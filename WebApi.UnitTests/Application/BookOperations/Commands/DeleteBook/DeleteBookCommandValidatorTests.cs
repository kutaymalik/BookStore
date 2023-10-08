using FluentAssertions;
using FluentValidation;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.BookOperations.Commands.DeleteBook;

public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;

    public DeleteBookCommandValidatorTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
    }

    [Theory]
    [InlineData(0)]
    public void WhenInvalidIdIsGiven_Validator_ShouldBeReturnError(int id)
    {
        // Arrange
        DeleteBookCommand command = new DeleteBookCommand(context);
        command.BookId = id;


        // Act
        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData(1)]
    public void WhenValidIdIsGiven_Validator_ShouldBeNotReturnError(int id)
    {
        // Arrange
        DeleteBookCommand command = new DeleteBookCommand(context);
        command.BookId = id;


        // Act
        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(0);
    }
}
