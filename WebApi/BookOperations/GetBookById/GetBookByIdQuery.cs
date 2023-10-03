using WebApi.Common;
using WebApi.DBOperations;
using static WebApi.BookOperations.GetBooks.GetBooksQuery;

namespace WebApi.BookOperations.GetBookById;

public class GetBookByIdQuery
{
    private readonly BookStoreDbContext dbContext;

    public GetBookByIdQuery(BookStoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public BooksViewModel Handle(int id)
    {
        var book = dbContext.Books.FirstOrDefault(x => x.Id == id);

        BooksViewModel vm = new();

        vm.Title = book.Title;
        vm.Genre = ((GenreEnum)book.GenreId).ToString();
        vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
        vm.PageCount = book.PageCount;

        return vm;
    }
}

