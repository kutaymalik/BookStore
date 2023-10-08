using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;

    public UpdateAuthorCommandTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenAuthorIsNotFound_InvalidOperationException_ShouldBeReturn()
    {
        // arrange

        UpdateAuthorCommand command = new(context);
        command.AuthorId = 999;

        // act & assert
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Record not found!");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeUpdated()
    {
        // arrange
        UpdateAuthorCommand command = new(context);
        var author = new Author { FirstName = "Paula", LastName = "Hawkins", DateOfBirth = new DateTime(2000, 11, 22) };

        context.Authors.Add(author);
        context.SaveChanges();

        command.AuthorId = author.Id;
        UpdateAuthorModel model = new UpdateAuthorModel { FirstName = "Paula", LastName = "Hawkins" };
        command.Model = model;

        // act
        FluentActions.Invoking(() => command.Handle()).Invoke();
        // assert
        var updatedAuthor = context.Authors.SingleOrDefault(a => a.Id == author.Id);
        updatedAuthor.Should().NotBeNull();
        updatedAuthor.FirstName.Should().Be(model.FirstName);
        updatedAuthor.LastName.Should().Be(model.LastName);
    }
}