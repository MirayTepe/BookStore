using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidator:AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(command=> command.BookId).GreaterThan(0);

        }


    }
} 