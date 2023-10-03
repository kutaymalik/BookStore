using WebApi.Common;
using WebApi.DBOperations;
using static WebApi.BookOperations.GetBooks.GetBooksQuery;

namespace WebApi.BookOperations.GetBookDetail;

public class GetBookDetailQuery
{
    private readonly BookStoreDbContext dbContext;
    public int BookId { get; set; }

    public GetBookDetailQuery(BookStoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public BookDetailViewModel Handle()
    {
        var book = dbContext.Books.Where(x => x.Id == BookId).SingleOrDefault();

        if(book == null)
        {
            throw new InvalidOperationException("Rcord not found!");
        }

        BookDetailViewModel vm = new();

        vm.Title = book.Title;
        vm.Genre = ((GenreEnum)book.GenreId).ToString();
        vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
        vm.PageCount = book.PageCount;

        return vm;
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string PublishDate { get; set; }
        public int PageCount { get; set; }
    }
}

