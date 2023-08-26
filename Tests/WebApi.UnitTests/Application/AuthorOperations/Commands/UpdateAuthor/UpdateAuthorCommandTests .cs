using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.AuthorOperation.Commands.UpdateAuthor;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;
using static WebApi.Application.AuthorOperation.Commands.UpdateAuthor.UpdateAuthorCommand;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateAuthorCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Theory]
        //[InlineData(1)]
        //[InlineData(4)]
        [InlineData(-2)]
        public void WhenGivenAuthorIdIsNotExist_InvalidOperationException_ShouldBeReturnErrors(int id)
        {
            // Arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context,_mapper);
            command.AuthorId = id;
            command.Model = new UpdateAuthorViewModel();

            // Act and Assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar bulunamadı.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeUpdated()
        {
            // Arrange (preparation)
            int authorId = 1;
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context,_mapper);
            UpdateAuthorViewModel model = new UpdateAuthorViewModel() { FirstName = "Test_WhenValidInputsAreGiven_Author_ShouldBeUpdated",LastName = "Test_Surname", DateOfBirth = DateTime.Now.Date.AddYears(-23) };
            command.Model = model;
            command.AuthorId = authorId;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var author = _context.Authors.SingleOrDefault(x => x.Id == authorId);

            author.Should().NotBeNull();
            author.FirstName.Should().Be(model.FirstName);
            author.LastName.Should().Be(model.LastName);
            author.DateOfBirth.Should().Be(model.DateOfBirth);
        }
    }
}