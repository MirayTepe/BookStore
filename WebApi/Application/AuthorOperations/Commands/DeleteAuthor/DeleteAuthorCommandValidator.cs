using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperation.Commands.DeleteAuthor;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperation.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidator:AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(command=> command.AuthorId).GreaterThan(0);

        }


    }
} 