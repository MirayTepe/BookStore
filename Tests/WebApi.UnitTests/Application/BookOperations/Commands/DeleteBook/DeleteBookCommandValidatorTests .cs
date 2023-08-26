using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(-999)]
        [InlineData(0)]
        [InlineData(-1)]
        //[InlineData(1)]
        public void WhenLowerThanAndEqualToZeroIdIsGiven_Validator_ShouldBeReturnError(int bookId)
        {
            //Arrange
            DeleteBookCommand command=new DeleteBookCommand(null);
            command.BookId=bookId;

            //Act
            DeleteBookCommandValidator validator=new DeleteBookCommandValidator();
            var result=validator.Validate(command);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);

        } 
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturn()
        {
           //Arrange
            DeleteBookCommand command=new DeleteBookCommand(null);
            command.BookId=1;

            //Act
            DeleteBookCommandValidator validator=new DeleteBookCommandValidator();
            var result=validator.Validate(command);

            //Assert
            result.Errors.Count.Should().Be(0);


        }

    }
} 