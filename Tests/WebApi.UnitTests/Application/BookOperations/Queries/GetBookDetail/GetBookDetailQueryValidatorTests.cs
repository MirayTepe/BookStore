﻿using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.Queries.GetById;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(-999)]
        [InlineData(0)]
        [InlineData(-1)]
        //[InlineData(1)]
        public void WhenLowerThanAndEqualToZeroIdIsGiven_Validator_ShouldBeReturnError(int bookId)
        {
            // Arrange
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            query.BookId = bookId;

            // Act
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(query);


            // Assert
            result.Errors.Count.Should().NotBe(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            query.BookId = 2;

            // Act
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(query);

            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}