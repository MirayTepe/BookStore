using FluentValidation;
using WebApi.Application.AuthorOperation.Commands.CreateAuthor;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperation.Commands.DeleteAuthor
{
	public class DeleteAuthorCommand
	{
		public int AuthorId { get; set; }
		private readonly BookStoreDbContext _dbContext;

		public DeleteAuthorCommand(BookStoreDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public void Handle()
		{
			var author = _dbContext.Authors.SingleOrDefault(a => a.Id == AuthorId);
			var authorBooks = _dbContext.Books.SingleOrDefault(a => a.AuthorId == AuthorId);
			
			if (author is null)
				throw new InvalidOperationException("ID bulunamadı.");
				
			if (authorBooks is not null)
				throw new InvalidOperationException(author.FirstName + " " +  author.LastName + " bu yazarın yayınlanmış bir kitabı var.Lütfen silmeden önce kitabı siliniz.");
				
			_dbContext.Authors.Remove(author);
			_dbContext.SaveChanges();
		}
	}

}