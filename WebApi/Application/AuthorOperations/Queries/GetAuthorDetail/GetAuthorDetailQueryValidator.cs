using FluentValidation;
namespace WebApi.Application.AuthorOperation.Queries.GetAuthorDetail
{
	public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
    {
		public GetAuthorDetailQueryValidator()
		{
			RuleFor(q => q.AuthorId).GreaterThan(0);
		}
    }
}
