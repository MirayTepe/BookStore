using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;


namespace WebApi.Application.BookOperations.Queries.GetById
{
    public class GetBookDetailQuery { 
        private readonly BookStoreDbContext _context;
        public int BookId {get; set;}

        private readonly IMapper _mapper;

        public GetBookDetailQuery(BookStoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper  = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var book = _context.Books.Include(x=>x.Genre).Where(b => b.Id == BookId).SingleOrDefault();

            if (book is null)
                throw new InvalidOperationException("Kitap bulunamadı.");

            BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(book);//new BookDetailViewModel();
          
             
                // vm.Title = book.Title;
                // vm.PageCount = book.PageCount;
                // vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
                // vm.Genre=((GenreEnum)book.GenreId).ToString();
            

            return vm;
        }
    }
     public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string FirstName { get; set; }
		public string LastName { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}
