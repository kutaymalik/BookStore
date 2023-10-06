using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommand
{
    public int GenreId { get; set; }
    public readonly BookStoreDbContext context;
    public DeleteGenreCommand(BookStoreDbContext context)
    {
        this.context = context;
    }

    public void Handle()
    {
        var genre = context.Genres.SingleOrDefault(x => x.Id == GenreId);

        if (genre == null)
        {
            throw new InvalidOperationException("Record not found");
        }

        context.Genres.Remove(genre);

        context.SaveChanges();
    }
}
