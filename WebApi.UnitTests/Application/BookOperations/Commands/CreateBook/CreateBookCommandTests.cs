using AutoMapper;
using FluentAssertions;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook;

public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public CreateBookCommandTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
        mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistsBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        // Arrange

        var book = new Book()
        {
            Title = "WhenAlreadyExistsBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",
            PageCount = 100,
            PublishDate = new DateTime(1990, 01, 10),
            GenreId = 1,
            AuthorId = 1,
        };

        context.Books.Add(book);
        context.SaveChanges();

        CreateBookCommand command = new CreateBookCommand(context, mapper);
        command.Model = new CreatebBookModel() { Title = book.Title };

        // Act & Assert

        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The book is already exists!");

        // Act

        // Assert
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
    {
        //Arrange
        CreateBookCommand command = new CreateBookCommand(context, mapper);

        CreatebBookModel model = new CreatebBookModel()
        {
            Title = "Hobbit",
            PageCount = 1000,
            PublishDate = DateTime.Now.Date.AddYears(-0),
            GenreId = 1,
            AuthorId = 1,
        };

        command.Model = model;

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var book = context.Books.SingleOrDefault(book => book.Title == model.Title);
        book.Should().NotBeNull();
        book.PageCount.Should().Be(model.PageCount);
        book.PublishDate.Should().Be(model.PublishDate);
        book.GenreId.Should().Be(model.GenreId);
        book.AuthorId.Should().Be(model.AuthorId);
    }
}
