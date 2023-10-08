using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommand
{
    public CreateAuthorModel Model { get; set; }
    private readonly IBookStoreDbContext context;
    private readonly IMapper mapper;

    public CreateAuthorCommand(IBookStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public void Handle()
    {
        var author = context.Authors
            .SingleOrDefault(x => x.FirstName.ToLower() == Model.FirstName.ToLower() 
            && x.LastName.ToLower() == Model.LastName.ToLower() 
            && x.DateOfBirth == Model.DateOfBirth);

        if (author != null)
        {
            throw new InvalidOperationException("The author is already exists!");
        }

        author = mapper.Map<Author>(Model);

        context.Authors.Add(author);

        context.SaveChanges();
    }
}

public class CreateAuthorModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}