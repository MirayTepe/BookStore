using System.Linq;
using WebApi.DBOperations;


namespace WebApi.BookOperations.GetById
{
    public class GetByIdBookCommand
    {
        private readonly BookStoreDbContext _context;

        public GetByIdBookCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public BookViewModel Handle(int id)
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == id);

            if (book is null)
                return null;

            var bookViewModel = new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                GenreId = book.GenreId,
                PageCount = book.PageCount,
                PublishDate = book.PublishDate
            };

            return bookViewModel;
        }
    }
     public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
