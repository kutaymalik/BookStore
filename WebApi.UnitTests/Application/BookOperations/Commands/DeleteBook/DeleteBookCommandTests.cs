using FluentAssertions;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook;

public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;

    public DeleteBookCommandTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
    }

    [Theory]
    [InlineData(10)]
    public void WhenBookDontExists_InvalidOperationException_ShouldBeReturn(int id)
    {
        // Arrange
        DeleteBookCommand command = new DeleteBookCommand(context);

        var book = context.Books.SingleOrDefault(x => x.Id == id);
        command.BookId = id;

        context.Books.Remove(book);
        context.SaveChanges();

        // Act & Assert
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Record not found!");
    }

    [Theory]
    [InlineData(1)]
    public void WhenValidIdIsGiven_Book_ShouldBeDeleted(int id)
    {
        // Arrange
        DeleteBookCommand command = new DeleteBookCommand(context);

        var book = context.Books.SingleOrDefault(x => x.Id == id);
        command.BookId = id;


        // Act & Assert
        FluentActions.Invoking(() => command.Handle()).Invoke();


        book = context.Books.SingleOrDefault(x => x.Id == id);
        book.Should().BeNull();
    }
}
