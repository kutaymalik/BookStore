using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthor;

public class GetAuthorQuery
{
    public readonly IBookStoreDbContext context;
    public readonly IMapper mapper;

    public GetAuthorQuery(IBookStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public List<AuthorViewModel> Handle()
    {
        var authors = context.Authors.OrderBy(x => x.Id).ToList();

        List<AuthorViewModel> list = mapper.Map<List<AuthorViewModel>>(authors);

        return list;
    }
}

public class AuthorViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}