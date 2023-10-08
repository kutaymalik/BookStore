using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenres;

public class GetGenresQuery
{
    public readonly IBookStoreDbContext context;
    public readonly IMapper mapper;

    public GetGenresQuery(IBookStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public List<GenresViewModel> Handle()
    {
        var genres = context.Genres.Where(x => x.IsActive).OrderBy(x => x.Id).ToList();

        List<GenresViewModel> list = mapper.Map<List<GenresViewModel>>(genres);

        return list;
    }
}

public class GenresViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}
