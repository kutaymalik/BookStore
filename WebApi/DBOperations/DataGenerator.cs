using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
        {
            if (context.Books.Any() || context.Authors.Any() ||context.Genres.Any())
            {
                return;
            }

            context.Authors.AddRange(
                new Author
                {
                    FirstName = "Stefan",
                    LastName = "Zweig",
                    DateOfBirth = new DateTime(1953, 3, 15)
                },
                new Author
                {
                    FirstName = "George",
                    LastName = "Orwell",
                    DateOfBirth = new DateTime(1942, 5, 22)
                },
                new Author
                {
                    FirstName = "Franz",
                    LastName = "Kafka",
                    DateOfBirth = new DateTime(1953, 6, 9)
                },
                new Author
                {
                    FirstName = "Cristiano",
                    LastName = "Ronaldo",
                    DateOfBirth = new DateTime(1953, 6, 9)
                }
            );

            context.Genres.AddRange(
                new Genre
                {
                    Name = "Personal Growth"
                },
                new Genre
                {
                    Name = "Science Fiction"
                },
                new Genre
                {
                    Name = "Novel"
                }
            );

            context.Books.AddRange(
                new Book
                {
                    Title = "Lean Startup",
                    GenreId = 1,
                    PageCount = 200,
                    PublishDate = new DateTime(2001, 06, 12),
                    AuthorId = 1
                },
                new Book
                {
                    Title = "Herland",
                    GenreId = 2,
                    PageCount = 250,
                    PublishDate = new DateTime(2010, 05, 23),
                    AuthorId = 2
                },
                new Book
                {
                    Title = "Dune",
                    GenreId = 2,
                    PageCount = 540,
                    PublishDate = new DateTime(2002, 12, 21),
                    AuthorId = 3
                }
            );

            context.SaveChanges();
        }
    }
}
