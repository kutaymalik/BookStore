using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommand
{
    public UpdateAuthorModel Model { get; set; }
    private readonly IBookStoreDbContext context;
    public int AuthorId { get; set; }

    public UpdateAuthorCommand(IBookStoreDbContext context)
    { 
        this.context = context;
    }

    public void Handle()
    {
        var author = context.Authors.SingleOrDefault(x => x.Id == AuthorId);

        if (author == null)
        {
            throw new InvalidOperationException("Record not found!");
        }

        author.FirstName = string.IsNullOrEmpty(Model.FirstName.Trim()) ? author.FirstName : Model.FirstName;
        author.LastName = string.IsNullOrEmpty(Model.LastName.Trim()) ? author.LastName : Model.LastName;

        context.SaveChanges();
    }

}

public class UpdateAuthorModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
