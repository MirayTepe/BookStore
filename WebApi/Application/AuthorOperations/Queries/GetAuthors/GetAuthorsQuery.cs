using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
namespace WebApi.Application.AuthorOperation.Queries.GetAuthors
{
	public class GetAuthorsQuery
	{
		private readonly  BookStoreDbContext _dbContext;
		private readonly IMapper _mapper;

		public GetAuthorsQuery(BookStoreDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}
		
		public List<AuthorsViewModel> Handle()
		{
			var authorList = _dbContext.Authors.OrderBy(a => a.Id).ToList();
			
			List<AuthorsViewModel> viewModel = _mapper.Map<List<AuthorsViewModel>>(authorList);
			
			return viewModel;
		}
		
		public class AuthorsViewModel
		{
			public int Id {get; set;}
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public string DateOfBirth { get; set; }
		}
	}
}