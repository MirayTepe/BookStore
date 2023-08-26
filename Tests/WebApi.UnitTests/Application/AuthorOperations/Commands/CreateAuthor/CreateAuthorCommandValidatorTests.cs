using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.AuthorOperation.Commands.CreateAuthor;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.UnitTests.TestSetup;
using static WebApi.Application.AuthorOperation.Commands.CreateAuthor.CreateAuthorCommand;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
       // [InlineData("Jack", "Sparrow")]
        [InlineData("", "Saparrow")]
        //[InlineData("Jac", "Sp")]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string authorName, string authorSurname)
        {
            // Arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorViewModel()
            {
                FirstName = authorName,
                LastName = authorSurname
                
            };

            // Act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            // Arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.Model = new CreateAuthorViewModel()
            {
                FirstName = "Jack",
                LastName = "Sparrow",
                DateOfBirth = DateTime.Now.Date,
            };

            // Act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}