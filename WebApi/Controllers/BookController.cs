using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookById;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.Controllers;

[ApiController]
[Route("bookstore/api/[controller]")]
public class BookController : ControllerBase
{
    private readonly BookStoreDbContext context;

    public BookController(BookStoreDbContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
        GetBooksQuery query = new GetBooksQuery(context);

        var result = query.Handle();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        GetBookByIdQuery query = new GetBookByIdQuery(context);

        var result = query.Handle(id);

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
        CreateBookCommand command = new CreateBookCommand(context);

        try
        {
            command.Model = model;

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
        UpdateBookCommand command = new UpdateBookCommand(context);

        try
        {
            command.Model = model;

            command.Handle(id);
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
        var book = context.Books.SingleOrDefault(x => x.Id == id);

        if (book == null)
        {
            return BadRequest();
        }

        context.Books.Remove(book);

        context.SaveChanges();

        return Ok();
    }

}
