using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests.TestSetup;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public CreateAuthorCommandTests(CommonTestFixture testFixture)
    {
        this.context = testFixture.Context;
        this.mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExitsAuthorFullNameIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        // arrange
        var author = new Author()
        {
            FirstName = "Jose",
            LastName = "Saramago",
            DateOfBirth = new DateTime(1948, 08, 07)
        };
        context.Authors.Add(author);
        context.SaveChanges();

        CreateAuthorCommand command = new CreateAuthorCommand(context, mapper);
        command.Model = new CreateAuthorModel { FirstName = author.FirstName, LastName = author.LastName, DateOfBirth = author.DateOfBirth };

        // act & assert
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("The author is already exists!");

    }

    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
    {
        // arrange
        CreateAuthorCommand command = new(context, mapper);
        CreateAuthorModel model = new CreateAuthorModel()
        {
            FirstName = "Jose",
            LastName = "Saramago",
            DateOfBirth = new DateTime(1948, 08, 07)
        };

        command.Model = model;

        // act
        FluentActions.Invoking(() => command.Handle()).Invoke();
        // assert
        var author = context.Authors.SingleOrDefault(g => g.FirstName == model.FirstName);
        author.Should().NotBeNull();
        author.LastName.Should().Be(model.LastName);
        author.DateOfBirth.Should().Be(model.DateOfBirth);
    }
}