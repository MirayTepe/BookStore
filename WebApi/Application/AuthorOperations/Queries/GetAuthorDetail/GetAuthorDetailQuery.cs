using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperation.Queries.GetAuthorDetail
{
	public class GetAuthorDetailQuery
	{
		public int AuthorId { get; set; }
		private readonly BookStoreDbContext _dbContext;
		private readonly IMapper _mapper;

		public GetAuthorDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}
		
		public AuthorDetailViewModel Handle()
		{
			var author = _dbContext.Authors.Where(a => a.Id == AuthorId).SingleOrDefault();
			
			if (author is null)
				throw new InvalidOperationException("Yazar bulunamıyor!");
			
			
			
			
			AuthorDetailViewModel vm = _mapper.Map<AuthorDetailViewModel>(author);

            return vm;
		}
		
		public class AuthorDetailViewModel
		{
			public string FirstName { get; set; }
			public string LastName { get; set; }
			public string DateOfBirth { get; set; }
		}
	}

}
