using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using Xunit;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _context=testFixture.Context;
            _mapper=testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeRetur()
        {
            //arrange(Hazırlık)
            var book=new Book(){Title="Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",PageCount=100,PublishDate=new System.DateTime(1998,01,10),GenreId=1};
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command=new CreateBookCommand(_context,_mapper);
            command.Model=new CreateBookViewModel(){Title=book.Title};
            //act(Çalıştırma) & assert(Doğrulama)
            FluentActions.Invoking(()=> command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            //arrange
            CreateBookCommand command=new CreateBookCommand(_context,_mapper);
            CreateBookViewModel model=new CreateBookViewModel(){Title="Hobbit",PageCount=1000,PublishDate=DateTime.Now.Date.AddYears(-10),GenreId=1};
            command.Model=model;

            //act
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            //assert
            var book=_context.Books.SingleOrDefault(book=>book.Title==model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            //book.Title.Should().Be(model.Title);
            book.GenreId.Should().Be(model.GenreId);


        }
    }
}