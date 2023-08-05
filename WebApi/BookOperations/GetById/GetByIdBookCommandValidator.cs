using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.GetById;
using WebApi.Common;


namespace WebApi.BookOperations.GetBookById
{
    public class GetByIdBookCommandValidator : AbstractValidator<GetByIdBookCommand>
    {
        public GetByIdBookCommandValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
          
        }

    }
}