using FluentValidation;
using WebApi.Application.AuthorOperation.Commands.CreateAuthor;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperation.Commands.DeleteAuthor
{
	public class DeleteAuthorCommand
	{
		public int AuthorId { get; set; }
		private readonly IBookStoreDbContext _dbContext;

		public DeleteAuthorCommand(IBookStoreDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public void Handle()
		{
			var author = _dbContext.Authors.SingleOrDefault(a => a.Id == AuthorId);
			var authorBooks = _dbContext.Books.SingleOrDefault(a => a.AuthorId == AuthorId);
			
			if (author is null)
				throw new InvalidOperationException("Yazar bulunamadı.");
			   var bookOfAuthor = _dbContext.Books.Where(x => x.AuthorId == AuthorId).Any();
            if (bookOfAuthor)
                throw new InvalidOperationException("Yazarın kayıtlı kitabı bulunduğu için işlem gerçekleştirilemedi.");
				
			_dbContext.Authors.Remove(author);
			_dbContext.SaveChanges();
		}
	}

}