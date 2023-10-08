using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommand
{
    public int GenreId { get; set; }
    public UpdateGenreModel Model { get; set; }
    public readonly IBookStoreDbContext context;

    public UpdateGenreCommand(IBookStoreDbContext context)
    {
        this.context = context;
    }

    public void Handle()
    {
        var genre = context.Genres.SingleOrDefault(x => x.Id ==  GenreId);

        if(genre == null)
        {
            throw new InvalidOperationException("Record not found");
        }

        if(context.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
        {
            throw new InvalidOperationException("The book genre with the same name already exists");
        }

        genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
        genre.IsActive = Model.IsActive;

        context.Genres.Update(genre);

        context.SaveChanges();
    }
}

public class UpdateGenreModel
{
    public string Name { get; set; }
    public bool IsActive { get; set; } = true;
}
