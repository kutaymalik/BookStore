using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommand
{
    public int AuthorId { get; set; }
    public readonly IBookStoreDbContext context;

    public DeleteAuthorCommand(IBookStoreDbContext context)
    {
        this.context = context;
    }

    public void Handle()
    {
        var author = context.Authors
            .Include(x => x.Books)
            .SingleOrDefault(x => x.Id == AuthorId);

        if (author == null)
        {
            throw new InvalidOperationException("Record not found!");
        }

        if(author.Books.Any())
        {
            throw new InvalidOperationException("You must first delete the author's books!");
        }

        context.Authors.Remove(author);

        context.SaveChanges();
    }
}
