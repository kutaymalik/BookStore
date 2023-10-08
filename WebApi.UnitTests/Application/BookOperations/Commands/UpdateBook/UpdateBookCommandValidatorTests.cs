using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using static WebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace WebApi.UnitTests.Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public UpdateBookCommandValidatorTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
        mapper = testFixture.Mapper;
    }

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
        UpdateBookCommand command = new UpdateBookCommand(context, mapper);

        UpdateBookModel model = new UpdateBookModel()
        {
            Title = title,
            PageCount = pageCount,
            PublishDate = DateTime.Now.Date.AddYears(-1),
            GenreId = genreId,
            AuthorId = authorId
        };

        var book = new Book() 
        { 
            Title = "Test Book", 
            AuthorId = 1, 
            GenreId = 1,
            PageCount = 100, 
            PublishDate = DateTime.Now.AddYears(-10) 
        };

        context.Books.Add(book);
        context.SaveChanges();

        command.BookId = 1;
        command.Model = model;

        // Act
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }


    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeREturnError()
    {
        // Arrange
        UpdateBookCommand command = new UpdateBookCommand(context, mapper);

        UpdateBookModel model = new UpdateBookModel()
        {
            Title = "Lord Of The Rings",
            PageCount = 100,
            PublishDate = DateTime.Now.Date.AddYears(-1),
            GenreId = 1,
            AuthorId = 1
        };

        var book = new Book()
        {
            Title = "Test Book",
            AuthorId = 1,
            GenreId = 1,
            PageCount = 100,
            PublishDate = DateTime.Now.AddYears(-10)
        };

        context.Books.Add(book);
        context.SaveChanges();

        command.BookId = 1;
        command.Model = model;

        // Act
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(0);
    }
}
