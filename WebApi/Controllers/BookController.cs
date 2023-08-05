using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookById;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.GetById;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.AddControllers{
    [ApiController]
    [Route("[controller]s")]
    public class BookController:ControllerBase
    {
        private readonly BookStoreDbContext _context;

        private readonly IMapper _mapper;

        public BookController(BookStoreDbContext context,IMapper mapper)
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
            try
            {
               GetByIdBookCommand command = new GetByIdBookCommand(_context,_mapper);
               command.BookId=id;
               GetByIdBookCommandValidator validator=new GetByIdBookCommandValidator();
               validator.ValidateAndThrow(command);
               result = command.Handle();

            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
           return Ok(result);
            
        }
        
       
        //Post
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook){
            CreateBookCommand command=new CreateBookCommand(_context,_mapper);
            try
            {
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
            
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
           
            return Ok();
        }
        //Put
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody]UpdateBookModel updatedBook){
            
            try
            {
                UpdateBookCommand command=new UpdateBookCommand(_context);
                command.BookId=id;
                command.Model=updatedBook;
                UpdateBookCommandValidator validator=new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return Ok();

        }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id){
      try
      {
         DeleteBookCommand command=new DeleteBookCommand(_context);
         command.BookId=id;
         DeleteBookCommandValidator validator= new DeleteBookCommandValidator();
         validator.ValidateAndThrow(command);
         command.Handle();  
      }
       catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
     
      return Ok();

    }
  }
}