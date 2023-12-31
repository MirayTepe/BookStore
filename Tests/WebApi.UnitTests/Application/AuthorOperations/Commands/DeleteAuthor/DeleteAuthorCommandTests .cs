using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.AuthorOperation.Commands.DeleteAuthor;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;

        public DeleteAuthorCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(99999)]
        public void WhenGivenAuthorIdIsNotExist_InvalidOperationException_ShouldBeReturn(int authorId)
        {
            // Arrange (preparation)
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = authorId;

            // Act & Assert (run and confirmation)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar bulunamadı.");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        public void
        WhenGivenAuthorHasAnyBook_InvalidOperationException_ShouldBeReturn(int authorId)
        {
            // Arrange (preparation)
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = authorId;


            // Act & Assert (run and confirmation)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazarın kayıtlı kitabı bulunduğu için işlem gerçekleştirilemedi.");
        }

        [Theory]
        //[InlineData(3)]
        [InlineData(4)]
        //[InlineData(1)]
        public void WhenValidInputsAreGiven_Author_ShouldBeDeleted(int authorId)
        {
            // Arrange (preparation)
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = authorId;

            // Act
            FluentActions
               .Invoking(() => command.Handle()).Invoke();

            // Assert 
            var author = _context.Authors.SingleOrDefault(x => x.Id == authorId);
            author.Should().BeNull();
        }
    }
}