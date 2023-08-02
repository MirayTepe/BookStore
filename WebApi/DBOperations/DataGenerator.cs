using Microsoft.EntityFrameworkCore;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
      //initilize metodumuz olsun, verileri insert eden.
      //IServiceProvider, In-memory ile alakalı. Program.cs kendi içerisindeki ServiceProvider ile burayı çağırarak
      //uygulama ilk çalıştığında hep ayağa kalkan bir yapı kuruyoruz, bu da ServiceProvider ile oluyor.
       public static void Initialize(IServiceProvider serviceProvider)
        {
            //Az önce oluşturmuş olduğumuz context'in instance'ına ihtiyacımız var. Çünkü, birazdan database'e kaydeceğim o bilgileri
            //bunu da context aracılığıyla yapacağız.
            //alttaki getrequiredService, injection ile alakalı. Buna sonra geleceğiz.
            using(var context= new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>())){
                if(context.Books.Any())
                {
                  return;
                }
                //Controller'daki verilerin aynısını buraya ekledik, onları silicez.
                 context.Books.AddRange( 
                    new Book
                      {
                          //Id = 1,
                          Title = "Mona Lisa Overdrive",
                          GenreId = 1, // Science Fiction
                          PageCount = 360,
                          PublishDate = new DateTime(1988, 06, 12)
                      },
                      new Book
                      {
                          //Id = 2,
                          Title = "Count Zero",
                          GenreId = 1, // Science Fiction
                          PageCount = 256,
                          PublishDate = new DateTime(1986, 01, 14)
                      },
                      new Book
                      {
                          //Id = 3,
                          Title = "Neuromancer",
                          GenreId = 1, // Science Fiction
                          PageCount = 271,
                          PublishDate = new DateTime(1984, 07, 01)
                      }
                  );
               context.SaveChanges();
            }
        }
    }
}