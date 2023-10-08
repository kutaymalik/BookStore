using FluentAssertions;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;

    public CreateGenreCommandTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
    }

    [Fact]
    public void WhenAlreadyExitsGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        // arrange (Hazırlık)
        var genre = new Genre()
        {
            Name = "Deneme",
            IsActive = true
        };
        context.Genres.Add(genre);
        context.SaveChanges();

        CreateGenreCommand command = new CreateGenreCommand(context);
        command.Model = new CreateGenreModel { Name = genre.Name };

        // act & assert (Çalıştırma - Doğrulama)
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Record already exists");

    }

    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
    {
        // arrange
        CreateGenreCommand command = new(context);
        CreateGenreModel model = new CreateGenreModel()
        {
            Name = "Test test",
        };

        command.Model = model;

        // act
        FluentActions.Invoking(() => command.Handle()).Invoke();
        // assert
        var genre = context.Genres.SingleOrDefault(g => g.Name == model.Name);
        genre.Should().NotBeNull();
        genre.IsActive.Should().BeTrue();
    }
}
