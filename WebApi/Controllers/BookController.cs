using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
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

        public BookController(BookStoreDbContext context)
        {
           _context=context;
        }
        

       [HttpGet]
        public IActionResult GetBooks(){
          GetBooksQuery query= new GetBooksQuery(_context);
          var result=query.Handle();
          return Ok(result);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            try
            {
               GetByIdBookCommand command = new GetByIdBookCommand(_context);
               command.BookId=id;
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
            CreateBookCommand command=new CreateBookCommand(_context);
            try
            {
                command.Model=newBook;
                command.Handle();
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