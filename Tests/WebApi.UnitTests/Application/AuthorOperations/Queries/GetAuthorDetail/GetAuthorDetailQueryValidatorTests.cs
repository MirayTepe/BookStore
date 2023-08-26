using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.AuthorOperation.Queries.GetAuthorDetail;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(-999)]
        [InlineData(0)]
        [InlineData(-1)]
        //[InlineData(1)]
        public void WhenLowerThanAndEqualToZeroIdIsGiven_Validator_ShouldBeReturnError(int authorId)
        {
            // Arrange
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null, null);
            query.AuthorId = authorId;

            // Act
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(query);


            // Assert
            result.Errors.Count.Should().NotBe(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null, null);
            query.AuthorId = 4;

            // Act
            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(query);

            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}