using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;


namespace WebApi.BookOperations.GetById
{
    public class GetByIdBookCommand
    {
        private readonly BookStoreDbContext _context;
        public int BookId {get; set;}

        public GetByIdBookCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public BookDetailViewModel Handle()
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == BookId);

            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı.");

            BookDetailViewModel vm = new BookDetailViewModel();
          
             
                vm.Title = book.Title;
                vm.PageCount = book.PageCount;
                vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
                vm.Genre=((GenreEnum)book.GenreId).ToString();
            

            return vm;
        }
    }
     public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}
