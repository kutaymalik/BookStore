using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;
using WebApi.Entities;
using static WebApi.Application.BookOperations.Queries.GetBooks.GetBooksQuery;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail;

public class GetBookDetailQuery
{
    private readonly IBookStoreDbContext dbContext;
    public int BookId { get; set; }
    private readonly IMapper mapper;

    public GetBookDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public BookDetailViewModel Handle()
    {
        var book = dbContext.Books
            .Include(x => x.Genre)
            .Include(x=> x.Author)
            .Where(x => x.Id == BookId).SingleOrDefault();

        if (book == null)
        {
            throw new InvalidOperationException("Record not found!");
        }

        BookDetailViewModel vm = mapper.Map<BookDetailViewModel>(book);

        //BookDetailViewModel vm = new();

        //vm.Title = book.Title;
        //vm.Genre = ((GenreEnum)book.GenreId).ToString();
        //vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
        //vm.PageCount = book.PageCount;

        return vm;
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string PublishDate { get; set; }
        public int PageCount { get; set; }
        public string Author { get; set; }
    }
}

