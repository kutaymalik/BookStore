using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook;

public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
{

    [Theory]
    [InlineData("Lord Of The Rings", 0, 0, 0)]
    [InlineData("Lord Of The Rings", 1, 0, 0)]
    [InlineData("Lord Of The Rings", 0, 1, 0)]
    [InlineData("Lord Of The Rings", 0, 0, 1)]
    [InlineData("Lord Of The Rings", 100, 1, 0)]
    [InlineData("", 0, 0, 0)]
    [InlineData("", 100, 1, 1)]
    [InlineData("", 0, 1, 1)]
    [InlineData("Lor", 100, 1, 1)]
    [InlineData("Lor", 0, 0, 0)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors
        (string title, int pageCount, int genreId, int authorId)
    {
        // Arrange
        CreateBookCommand command = new CreateBookCommand(null, null);

        command.Model = new CreatebBookModel()
        {
            Title = title,
            PageCount = pageCount,
            PublishDate = DateTime.Now.Date.AddYears(-1),
            GenreId = genreId,
            AuthorId = authorId
        };

        // Act
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
    {
        // Arrange
        CreateBookCommand command = new CreateBookCommand(null, null);

        command.Model = new CreatebBookModel()
        {
            Title = "Lord Of The Rings",
            PageCount = 100,
            PublishDate = DateTime.Now.Date,
            GenreId = 1,
            AuthorId = 1
        };

        // Act
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeREturnError()
    {
        // Arrange
        CreateBookCommand command = new CreateBookCommand(null, null);

        command.Model = new CreatebBookModel()
        {
            Title = "Lord Of The Rings",
            PageCount = 100,
            PublishDate = DateTime.Now.Date.AddYears(-1),
            GenreId = 1,
            AuthorId = 1
        };

        // Act
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(0);
    }
}
