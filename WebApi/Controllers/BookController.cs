using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using static WebApi.Application.BookOperations.Queries.GetBookDetail.GetBookDetailQuery;
using static WebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;

namespace WebApi.Controllers;

[ApiController]
[Route("bookstore/api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookStoreDbContext context;
    private readonly IMapper mapper;

    public BookController(IBookStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
        GetBooksQuery query = new GetBooksQuery(context, mapper);

        var result = query.Handle();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        BookDetailViewModel result;

        GetBookDetailQuery query = new GetBookDetailQuery(context, mapper);

        query.BookId = id;

        GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
        validator.ValidateAndThrow(query);

        result = query.Handle();

        return Ok(result);
    }

    //[HttpGet]
    //public Book Get([FromQuery] string id)
    //{
    //    var book = BookList.Where(x => x.Id == Convert.ToInt32(id)).SingleOrDefault();

    //    return book;
    //}

    [HttpPost]
    public IActionResult AddBook([FromBody] CreatebBookModel model)
    {
        CreateBookCommand command = new CreateBookCommand(context, mapper);

        command.Model = model;

        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel model)
    {
        UpdateBookCommand command = new UpdateBookCommand(context, mapper);

        command.BookId = id;

        command.Model = model;

        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        DeleteBookCommand command = new(context);

        command.BookId = id;

        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        validator.ValidateAndThrow(command);

        command.Handle();

        return Ok();
    }

}
