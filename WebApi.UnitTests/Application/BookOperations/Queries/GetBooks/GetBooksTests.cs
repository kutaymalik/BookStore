using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;
using static WebApi.Application.BookOperations.Queries.GetBooks.GetBooksQuery;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBooks;

public class GetBooksTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public GetBooksTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
        this.mapper = testFixture.Mapper;
    }

    //[Fact]
    public void WhenGetBooksQueryIsHandled_BookListShouldBeReturned()
    {
        // Arrange
        var query = new GetBooksQuery(context, mapper);

        // Act
        var result = query.Handle();
        List<BooksViewModel> books = mapper.Map<List<BooksViewModel>>(context.Books.ToList());

        // Assert
        books.Should().HaveCount(3);
        result.Should().NotBeNull();
        result.Should().HaveCount(3);

        result[0].Title.Should().Be(books[0].Title);
        result[0].Genre.Should().Be(books[0].Genre);
        result[0].Author.Should().Be(books[0].Author);
        result[0].PageCount.Should().Be(books[0].PageCount);
        result[0].PublishDate.Should().Be(books[0].PublishDate);

        result[1].Title.Should().Be(books[1].Title);
        result[1].Genre.Should().Be(books[1].Genre);
        result[1].Author.Should().Be(books[1].Author);
        result[1].PageCount.Should().Be(books[1].PageCount);
        result[1].PublishDate.Should().Be(books[1].PublishDate);

        result[2].Title.Should().Be(books[2].Title);
        result[2].Genre.Should().Be(books[2].Genre);
        result[2].Author.Should().Be(books[2].Author);
        result[2].PageCount.Should().Be(books[2].PageCount);
        result[2].PublishDate.Should().Be(books[2].PublishDate);

    }
}
