using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;

    public DeleteGenreCommandTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
    }


    [Fact]
    public void WhenGivenGenreIsNotFound_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange

        DeleteGenreCommand command = new(context);
        command.GenreId = 999;

        // act & assert
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Record not found");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeDeleted()
    {
        // arrange
        DeleteGenreCommand command = new(context);
        command.GenreId = 1;

        // act
        FluentActions.Invoking(() => command.Handle()).Invoke();
        // assert
        var genre = context.Genres.SingleOrDefault(g => g.Id == command.GenreId);
        genre.Should().BeNull();
    }

}
