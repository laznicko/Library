using Library.DataLayer;
using Library.DataLayer.Entities;

namespace Library.BusinessLayer
{
    public static class DbInitializer
    {
        /// <summary>
        /// Initialize db context, and set default data
        /// </summary>
        /// <param name="context"></param>
        public static void Initialize(DBContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Books.Any())
            {
                var books = new[]
                {
                    new BookEntity { BookPublicID = Guid.NewGuid(), ISBN = "123456779", BookTitle = "Sample Book 1", AuthorFirstName = "John", AuthorLastName = "Paul", IsBorrowed = false },
                    new BookEntity { BookPublicID = Guid.NewGuid(), ISBN = "987651321", BookTitle = "Sample Book 2", AuthorFirstName = "Anthon", AuthorLastName = "King", IsBorrowed = false }
                };

                foreach (var book in books)
                {
                    context.Books.Add(book);
                }
            }

            if (!context.Users.Any())
            {
                var users = new[]
                {
                    new UserEntity { UserPublicID = Guid.NewGuid(), Email = "user@email.com", FirstName = "Tom", LastName = "Jerry"}
                };

                foreach (var user in users)
                {
                    context.Users.Add(user);
                }
            }

            context.SaveChanges();
        }
    }
}
