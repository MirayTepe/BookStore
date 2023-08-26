using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.GenreOperations.Queries;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Theory]
        //[InlineData(1)]
        [InlineData(55)]
        [InlineData(9999)]
        public void WhenGenreIdIsNotFound_InvalidOperationException_ShouldReturnError(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = id;
            FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı.");

        }
    }
}