using WebApi.DBOperations;

namespace WebApi.BookOperations.UpdateBook;

public class UpdateBookCommand
{
    public UpdateBookModel Model { get; set; }
    private readonly BookStoreDbContext dbContext;

    public UpdateBookCommand(BookStoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Handle(int id)
    {
        var book = dbContext.Books.SingleOrDefault(x => x.Id == id);

        if(book == null)
        {
            throw new InvalidOperationException("Record not found!");
        }

        book.Title = Model.Title != default ? Model.Title : book.Title;
        book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
        book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
        book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;

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
