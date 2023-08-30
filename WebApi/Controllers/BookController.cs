using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Queries.GetById;
using WebApi.DBOperations;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace WebApi.Controllers{
    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class BookController:ControllerBase
    {
        private readonly IBookStoreDbContext _context;

        private readonly IMapper _mapper;
       

        public BookController(IBookStoreDbContext context,IMapper mapper)
        {
           _context=context;
           _mapper=mapper;
        }
        

       [HttpGet]
        public IActionResult GetBooks(){
          GetBooksQuery query= new GetBooksQuery(_context,_mapper);
          var result=query.Handle();
          return Ok(result);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
          
               GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
               query.BookId=id;
               GetBookDetailQueryValidator validator=new GetBookDetailQueryValidator();
               validator.ValidateAndThrow(query);
               result = query.Handle();

         
           return Ok(result);
            
        }
        
       
        //Post
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookViewModel newBook){

            CreateBookCommand command=new CreateBookCommand(_context,_mapper);
           
                command.Model=newBook;
                CreateBookCommandValidator validator=new CreateBookCommandValidator();
                
                validator.ValidateAndThrow(command);
                 command.Handle();
                // if(!result.IsValid)
                //     foreach (var item in result.Errors)
                //     {
                //         Console.WriteLine("Özellik "+ item.PropertyName+" - Error Message: "+item.ErrorMessage);
                        
                //     }
                // else
                //     command.Handle();
            
            
         
                
            
           
            return Ok();
        }
        //Put
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody]UpdateBookViewModel updatedBook){
           
           
                UpdateBookCommand command=new UpdateBookCommand(_context);
                command.BookId=id;
                command.Model=updatedBook; 
                UpdateBookCommandValidator validator=new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command); 
                command.Handle();
              
           
        
         return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id){
    
           DeleteBookCommand command=new DeleteBookCommand(_context);
           command.BookId=id;
           DeleteBookCommandValidator validator= new DeleteBookCommandValidator();
           validator.ValidateAndThrow(command);
           command.Handle();  
      
     
     
         return Ok();

       }
  }
}