using WebApi.DBOperations;

namespace WebApi.BookOperations.DeleteBook;

public class DeleteBookCommand
{
    private readonly BookStoreDbContext dbContext;
    public int BookId { get; set; }

    public DeleteBookCommand(BookStoreDbContext dbContext)
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

        dbContext.Books.Remove(book);

        dbContext.SaveChanges();
    }
}
