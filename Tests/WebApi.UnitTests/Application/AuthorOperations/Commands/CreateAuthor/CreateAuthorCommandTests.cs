using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.AuthorOperation.Commands.CreateAuthor;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using static WebApi.Application.AuthorOperation.Commands.CreateAuthor.CreateAuthorCommand;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;


        public CreateAuthorCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistAuthorIdentityIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Arrange (preparation)
            var author = new Author() { FirstName = "Test_WhenAlreadyExistAuthorIdentityIsGiven_InvalidOperationException_ShouldBeReturn", LastName = "Test_Surname", DateOfBirth = DateTime.Now.AddYears(-2) };
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorViewModel() { FirstName = author.FirstName, LastName = author.LastName, DateOfBirth = author.DateOfBirth };

            // Act & Assert (run and confirmation)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar zaten mevcut");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            // Arrange (preparation)
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            CreateAuthorViewModel model = new CreateAuthorViewModel() { FirstName = "Jack", LastName = "Sparrow", DateOfBirth = DateTime.Now.AddYears(-2) };
            command.Model = model;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var author = _context.Authors.SingleOrDefault(x => x.FirstName == model.FirstName && x.LastName == model.LastName);

            author.Should().NotBeNull();
            author.FirstName.Should().Be(model.FirstName);
            author.LastName.Should().Be(model.LastName);
            author.DateOfBirth.Should().Be(model.DateOfBirth);
        }
    }
}
