﻿using WebApi.DBOperations;

namespace WebApi.Application.BookOperations.Commands.DeleteBook;

public class DeleteBookCommand
{
    private readonly IBookStoreDbContext dbContext;
    public int BookId { get; set; }

    public DeleteBookCommand(IBookStoreDbContext dbContext)
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
