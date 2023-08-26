using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.AuthorOperation.Queries.GetAuthorDetail;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Theory]
        //[InlineData(1)]
        [InlineData(55)]
        [InlineData(9999)]
        public void WhenAuthorIdIsNotFound_InvalidOperationException_ShouldReturnError(int id)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = id;
            FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar kaydı bulunamadı.");

        }
    }
}