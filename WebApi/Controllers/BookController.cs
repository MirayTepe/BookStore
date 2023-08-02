using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
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
            GetByIdBookCommand command = new GetByIdBookCommand(_context);
            var book = command.Handle(id);

            if (book is null)
                return NotFound();

            return Ok(book);
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
            UpdateBookCommand command=new UpdateBookCommand(_context);

            try
            {
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
      var book=_context.Books.SingleOrDefault(x=>x.Id==id);
      if (book is null)
             return BadRequest();
    
       _context.Books.Remove(book);
       _context.SaveChanges();
       return Ok();


    }
  }
}