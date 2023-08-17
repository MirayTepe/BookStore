using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;


namespace WebApi.Application.BookOperations.Queries.GetById
{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueryValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
          
        }

    }
}