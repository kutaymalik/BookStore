using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup;

public static class Authors
{
    public static void AddAuthors(this BookStoreDbContext context)
    {
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
                });
    }
}
