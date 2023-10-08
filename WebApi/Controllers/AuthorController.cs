using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;

namespace WebApi.Controllers;

[ApiController]
[Route("bookstore/api/[controller]")]
public class AuthorController: ControllerBase
{
    private readonly IBookStoreDbContext context;
    private readonly IMapper mapper;

    public AuthorController(IBookStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAuthors()
    {
        GetAuthorQuery query = new GetAuthorQuery(context, mapper);

        var result = query.Handle();

        return Ok(result);
    }

    [HttpGet("id")]
    public IActionResult GetAuthorDetail(int id)
    {
        GetAuthorDetailQuery query = new GetAuthorDetailQuery(context, mapper);
        query.AuthorId = id;

        GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
        validator.ValidateAndThrow(query);

        var result = query.Handle();
        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor)
    {
        CreateAuthorCommand command = new CreateAuthorCommand(context, mapper);
        command.Model = newAuthor;

        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpPut("id")]
    public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updateAuthor)
    {
        UpdateAuthorCommand command = new UpdateAuthorCommand(context);
        command.AuthorId = id;
        command.Model = updateAuthor;

        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("id")]
    public IActionResult DeleteAuthor(int id)
    {
        DeleteAuthorCommand command = new DeleteAuthorCommand(context);
        command.AuthorId = id;

        DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }
}
