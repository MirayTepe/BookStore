using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperation.Commands.DeleteAuthor;
using WebApi.Application.AuthorOperation.Commands.UpdateAuthor;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperation.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator:AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
           RuleFor(command => command.Model.FirstName).MinimumLength(2).NotEmpty();
		   RuleFor(command => command.Model.LastName).MinimumLength(2).NotEmpty();
		   RuleFor(command => command.Model.DateOfBirth.Date).LessThan(DateTime.Now.Date);

        }


    }
} 