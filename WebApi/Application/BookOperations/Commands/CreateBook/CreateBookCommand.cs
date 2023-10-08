using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Commands.CreateBook;

public class CreateBookCommand
{
    public CreatebBookModel Model { get; set; }
    private readonly IBookStoreDbContext dbContext;
    private readonly IMapper mapper;

    public CreateBookCommand(IBookStoreDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var book = dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);

        if (book != null)
        {
            throw new InvalidOperationException("The book is already exists!");
        }

        book = mapper.Map<Book>(Model);

        //book = new Book();

        //book.Title = Model.Title;
        //book.PublishDate = Model.PublishDate;
        //book.PageCount = Model.PageCount;
        //book.GenreId = Model.GenreId;

        dbContext.Books.Add(book);

        dbContext.SaveChanges();
    }

    public class CreatebBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public int AuthorId { get; set; }
    }
}
