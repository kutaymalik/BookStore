using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommand
{
    public UpdateBookModel Model { get; set; }
    private readonly IBookStoreDbContext dbContext;
    private readonly IMapper mapper;

    public int BookId { get; set; }

    public UpdateBookCommand(IBookStoreDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var book = dbContext.Books.SingleOrDefault(x => x.Id == BookId);

        if (book == null)
        {
            throw new InvalidOperationException("Record not found!");
        }

        book = mapper.Map<Book>(Model);

        dbContext.Books.Update(book);

        dbContext.SaveChanges();
    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
