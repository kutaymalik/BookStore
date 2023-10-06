using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommand
{
    public UpdateBookModel Model { get; set; }
    private readonly BookStoreDbContext dbContext;

    public int BookId { get; set; }

    public UpdateBookCommand(BookStoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Handle()
    {
        var book = dbContext.Books.SingleOrDefault(x => x.Id == BookId);

        if (book == null)
        {
            throw new InvalidOperationException("Record not found!");
        }

        book.Title = Model.Title != default ? Model.Title : book.Title;

        dbContext.SaveChanges();
    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
