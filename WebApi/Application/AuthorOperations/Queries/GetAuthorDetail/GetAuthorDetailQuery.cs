using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;

public class GetAuthorDetailQuery
{
    public int AuthorId { get; set; }
    public readonly IBookStoreDbContext context;
    public readonly IMapper mapper;

    public GetAuthorDetailQuery(IBookStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public AuthorDetailViewModel Handle()
    {
        var author = context.Authors.SingleOrDefault(x => x.Id == AuthorId);

        if(author == null)
        {
            throw new InvalidOperationException("Book author not found");
        }

        return mapper.Map<AuthorDetailViewModel>(author);
    }


}

public class AuthorDetailViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}