using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;
using static WebApi.Application.BookOperations.Queries.GetBookDetail.GetBookDetailQuery;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetails;

public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public GetBookDetailQueryTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
        this.mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeReturned()
    {
        // arrange
        GetBookDetailQuery query = new GetBookDetailQuery(context, mapper);
        query.BookId = 1;

        var book = context.Books.Include(x => x.Genre).Include(x => x.Author).Where(b => b.Id == 1).SingleOrDefault();

        // act
        BookDetailViewModel vm = query.Handle();

        // assert
        book.Should().NotBeNull();
        vm.Should().NotBeNull();
        vm.Title.Should().Be(book.Title);
        vm.PageCount.Should().Be(book.PageCount);
        vm.Genre.Should().Be(book.Genre.Name);
        vm.Author.Should().Be(book.Author.FirstName + " " + book.Author.LastName);
        vm.PublishDate.Should().Be(book.PublishDate.ToString("dd/MM/yyyy 00:00:00"));
    }

    [Fact]
    public void WhenNonExistingBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        // arrange
        int bookId = 11;

        GetBookDetailQuery query = new GetBookDetailQuery(context, mapper);
        query.BookId = bookId;

        // assert
        query.Invoking(x => x.Handle())
             .Should().Throw<InvalidOperationException>()
             .And.Message.Should().Be("Record not found!");
    }



}
