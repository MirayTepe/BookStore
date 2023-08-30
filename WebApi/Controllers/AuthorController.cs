using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

using Microsoft.AspNetCore.Authorization;
using WebApi.DBOperations;
using WebApi.Application.AuthorOperation.Queries.GetAuthorDetail;
using static WebApi.Application.AuthorOperation.Commands.CreateAuthor.CreateAuthorCommand;
using WebApi.Application.AuthorOperation.Commands.CreateAuthor;
using static WebApi.Application.AuthorOperation.Commands.UpdateAuthor.UpdateAuthorCommand;
using WebApi.Application.AuthorOperation.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperation.Commands.DeleteAuthor;
using System.Net.WebSockets;
using WebApi.Application.AuthorOperation.Queries.GetAuthors;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase
    {
        private readonly IBookStoreDbContext _context; 
        private readonly IMapper _mapper;
        public AuthorController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
            return Ok(query.Handle());
        }
        
        [HttpGet("{id}")]
        public IActionResult GetAuthorById(int id)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = id;          
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            validator.ValidateAndThrow(query);
            var result=query.Handle();
            return Ok(result);
        }
        
        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorViewModel newAuthor)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);

            command.Model = newAuthor;
            
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            
            command.Handle();
            
            return Ok();
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorViewModel updatedAuthor)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context, _mapper);
            
            command.AuthorId = id;
            command.Model = updatedAuthor;
            
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            validator.ValidateAndThrow(command);
            
            command.Handle();
            
            return Ok();
        }
        
        [HttpDelete("{id}")]
        public IActionResult RemoveAuthor(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            
            command.AuthorId = id;
            //VALIDATIONS
            command.Handle();
            
            return Ok();
        }
    }
}