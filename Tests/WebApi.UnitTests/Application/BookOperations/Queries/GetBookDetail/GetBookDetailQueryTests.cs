using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.BookOperations.Queries.GetById;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Theory]
        //[InlineData(1)]
        [InlineData(55)]
        [InlineData(9999)]
        public void WhenBookIdIsNotFound_InvalidOperationException_ShouldReturnError(int id)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = id;
            FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı.");

        }
    }
}