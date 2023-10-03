using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.GetBookDetail.GetBookDetailQuery;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.Controllers;

[ApiController]
[Route("bookstore/api/[controller]")]
public class BookController : ControllerBase
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public BookController(BookStoreDbContext context, IMapper mapper)
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
        try
        {
            GetBookDetailQuery query = new GetBookDetailQuery(context, mapper);

            query.BookId = id;

            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            validator.ValidateAndThrow(query);

            result  = query.Handle();
        } 
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        

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

        try
        {
            command.Model = model;

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel model)
    {
        try
        {
            UpdateBookCommand command = new UpdateBookCommand(context);

            command.BookId = id;

            command.Model = model;

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        try
        {
            DeleteBookCommand command = new(context);

            command.BookId = id;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
        }
        catch (Exception ex)
        {
            BadRequest(ex.Message);
        }

        return Ok();
    }

}
