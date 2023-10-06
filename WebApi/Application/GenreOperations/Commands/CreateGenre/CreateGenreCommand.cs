using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommand
{
    public CreateGenreModel Model { get; set; }
    private readonly BookStoreDbContext context;
    public CreateGenreCommand(BookStoreDbContext context)
    {
        this.context = context;
    }

    public void Handle()
    {
        var genre = context.Genres.SingleOrDefault(x => x.Name == Model.Name);

        if (genre != null)
        {
            throw new InvalidOperationException("Record already exists");
        }

        genre = new Genre();

        genre.Name = Model.Name;

        context.Genres.Add(genre);

        context.SaveChanges();
    }
}

public class CreateGenreModel
{
    public string Name { get; set; }
}
