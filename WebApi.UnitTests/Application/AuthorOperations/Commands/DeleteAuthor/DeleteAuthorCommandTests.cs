using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;

    public DeleteAuthorCommandTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenAuthorIsNotFound_InvalidOperationException_ShouldBeReturn()
    {
        // arrange

        DeleteAuthorCommand command = new(context);
        command.AuthorId = 120;

        // act & assert
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Record not found!");
    }

    [Fact]
    public void WhenGivenAuthorHaveBook_InvalidOperationException_ShouldBeReturn()
    {
        // arrange

        DeleteAuthorCommand command = new(context);
        command.AuthorId = 1;

        // act & assert
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("You must first delete the author's books!");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeDeleted()
    {
        // arrange
        var newAuthor = new Author()
        {
            FirstName = "Jose",
            LastName = "Saramago",
            DateOfBirth = new DateTime(1994, 08, 07)
        };
        context.Authors.Add(newAuthor);
        context.SaveChanges();

        DeleteAuthorCommand command = new(context);
        command.AuthorId = newAuthor.Id;

        // act
        FluentActions.Invoking(() => command.Handle()).Invoke();
        // assert
        var author = context.Authors.SingleOrDefault(a => a.Id == command.AuthorId);
        author.Should().BeNull();
    }
}