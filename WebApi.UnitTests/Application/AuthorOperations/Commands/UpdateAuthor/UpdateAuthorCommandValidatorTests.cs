using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.UnitTests.TestSetup;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
{

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void WhenAuthorIdIsInvalid_Validator_ShouldHaveError(int authorId)
    {
        // arrange
        var model = new UpdateAuthorModel { FirstName = "Joseph", LastName = "Murphy"};
        UpdateAuthorCommand command = new(null);
        command.Model = model;
        command.AuthorId = authorId;

        // act
        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(command);

        // assert
        result.Errors.Should().ContainSingle();
    }

    [Theory]
    [InlineData("", "")]
    [InlineData("ab", "cd")]
    [InlineData("12", "456")]
    [InlineData("123", "")]
    public void WhenModelIsInvalid_Validator_ShouldHaveError(string firstName, string LastName)
    {
        // arrange
        var model = new UpdateAuthorModel { FirstName = firstName, LastName = LastName};
        UpdateAuthorCommand command = new(null);
        command.AuthorId = 3;
        command.Model = model;

        // act
        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(command);

        // assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenVirthDayEqualNowGiven_Validator_ShouldBeReturnError()
    {
        // arrange
        UpdateAuthorCommand command = new(null);
        command.Model = new UpdateAuthorModel { FirstName = "Jack", LastName = "London" };

        // act
        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(command);

        // assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenInputsAreValid_Validator_ShouldNotHaveError()
    {
        // arrange
        var model = new UpdateAuthorModel { FirstName = "Jack", LastName = "London" };
        UpdateAuthorCommand command = new(null);
        command.AuthorId = 2;
        command.Model = model;

        // act
        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(command);

        // assert
        result.Errors.Count.Should().Be(0);
    }
}