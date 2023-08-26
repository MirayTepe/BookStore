using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange( 
                new Book
                {
                    //Id = 1,
                    Title = "Mona Lisa Overdrive",
                    AuthorId=1,
                    GenreId = 1, // Science Fiction
                    PageCount = 360,
                    PublishDate = new DateTime(1988, 06, 12)
                },
                new Book
                {
                    //Id = 2,
                    Title = "Count Zero",
                    AuthorId=2,
                    GenreId = 2, // Science Fiction
                    PageCount = 256,
                    PublishDate = new DateTime(1986, 01, 14)
                },
                new Book
                {
                    //Id = 3,
                    Title = "Neuromancer",
                    AuthorId=3,
                    GenreId = 2, // Science Fiction
                    PageCount = 271,
                    PublishDate = new DateTime(1984, 07, 01)
                }
            );
        }
    }
}