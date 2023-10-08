using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using static WebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public UpdateBookCommandTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
        mapper = testFixture.Mapper;
    }

    [Theory]
    [InlineData(999)]
    public void WhenBookDontExists_InvalidOperationException_ShouldBeReturn(int id)
    {
        // Arrange
        UpdateBookCommand command = new UpdateBookCommand(context, mapper);
        command.BookId = id;

        // Act & Assert
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Record not found!");
    }

    [Fact]
    public void WhenBookExists_Book_ShouldBeUpdated()
    {
        // Arrange
        UpdateBookCommand command = new UpdateBookCommand(context, mapper);
        UpdateBookModel model = new UpdateBookModel(){ Title = "Update Book", AuthorId = 2, GenreId = 2 , PageCount = 200, PublishDate = new DateTime(2022, 1, 1)};

        var book = new Book() { Title = "Test Book", AuthorId = 1, GenreId = 1, PageCount = 100, PublishDate = DateTime.Now.AddYears(-10) };

        context.Books.Add(book);
        context.SaveChanges();

        command.BookId = 1;
        command.Model = model;

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var updatedBook = context.Books.SingleOrDefault(b => b.Id == book.Id);
        book.Should().NotBeNull();
    }
}
