using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebApi.Application.AuthorOperation.Commands.UpdateAuthor;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.UnitTests.TestSetup;
using static WebApi.Application.AuthorOperation.Commands.UpdateAuthor.UpdateAuthorCommand;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        //[InlineData(1,"Jack", "Sparrow")]
        [InlineData(2,"", "Saparrow")]
        //[InlineData(3,"Jac", "Sp")]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int authorId, string name, string surname)
        {
            // Arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null,null);
            command.AuthorId = authorId;
            command.Model = new UpdateAuthorViewModel()
            {
                FirstName = name,
                LastName=surname,
                DateOfBirth = DateTime.Now.Date.AddYears(-1),
            };

            // Act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            // Arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(null,null);
            command.AuthorId = 1;
            command.Model = new UpdateAuthorViewModel()
            {
                FirstName = "Jack",
                LastName = "London",
                DateOfBirth = DateTime.Now.Date.AddYears(-120),
            };

            // Act
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().Be(0);
        }
    }
}