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
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        

        public UpdateBookCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            
        }
        [Theory]
        //[InlineData(1)]
        [InlineData(55)]
        [InlineData(-2)] 
        public void WhenGivenBookIdIsNotExist_InvalidOperationException_ShouldBeReturnErrors(int id){
            //Arrange
            UpdateBookCommand command=new UpdateBookCommand(_context);
            command.BookId=id;
            command.Model=new UpdateBookViewModel();
            
            //Act and Assert
            FluentActions
                .Invoking(()=>command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should()
                .Be("Kitap bulunamadı.");

        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            // Arrange (preparation)
            int bookId = 2;
            UpdateBookCommand command = new UpdateBookCommand(_context);
            UpdateBookViewModel model = new UpdateBookViewModel() { Title = "Romeo&Juliet", PageCount = 150, PublishDate = DateTime.Now.Date.AddYears(-2), GenreId = 1 };
            command.Model = model;
            command.BookId = bookId;

            // Act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // Assert
            var book = _context.Books.SingleOrDefault(x => x.Id == bookId);

            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
        }
    }
}