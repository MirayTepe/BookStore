using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateGenreCommandTest(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Theory]
        //[InlineData(1)]
       // [InlineData(4)]
        [InlineData(-2)]
        [InlineData(99)]
        public void WhenGivenGenreIdIsNotExist_InvalidOperationException_ShouldBeReturnErrors(int id)
        {
            // Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = id;
            command.Model = new UpdateGenreViewModel();

            // Act and Assert
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı.");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldNotBeReturnError()
        {
            // Arrange
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = 2;
            command.Model = new UpdateGenreViewModel()
            {
                Name = "Underground Literature",
            };

            // Act
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            // Assert
            result.Errors.Count.Should().Be(0);
        }

    }
}