using FluentValidation;
using WebApi.Application.AuthorOperation.Commands.CreateAuthor;

namespace WebApi.Application.AuthorOperation.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command=>command.Model.FirstName).MinimumLength(2).NotEmpty();
            RuleFor(command=>command.Model.LastName).MinimumLength(2).NotEmpty();
            RuleFor(command=>command.Model.DateOfBirth.Date).LessThan(DateTime.Now.Date);	
        }
    }
        
}
        