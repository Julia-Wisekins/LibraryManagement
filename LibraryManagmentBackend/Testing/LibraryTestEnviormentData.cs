using LibraryManagementBackend.Objects;
using System.Collections;

namespace LibraryManagementBackend.Testing
{
    public static class LibraryTestEnviormentData
    {
        public static IEnumerable<Reader> LibraryReaders()
        {
            return [
             new()
            {
                Id = 1,
                Name = "Aragorn Elessar",
            },
            new()
            {
                Id = 2,
                Name = "Lyra Silvertongue",
            },
            new()
            {
                Id = 3,
                Name = "Eragon Shadeslayer",
            }];
        }

        public static IEnumerable<Book> LibraryBooks()
        {
            return [
             new ()
            {
                Author = "George Orwell",
                Availibility = Availability.Available,
                Id = 1,
                ISBN = "9780451524935",
                Title = "1984",
            },
             new ()
            {
                Author = "J.K. Rowling",
                Availibility = Availability.Available,
                Id = 2,
                ISBN = "9780747532743",
                Title = "Harry Potter and the Philosopher's Stone",
            },
            new()
            {
                Author = "Harper Lee",
                Availibility = Availability.Available,
                Id = 3,
                ISBN = "9780061120084",
                Title = "To Kill a Mockingbird",
            },
           new()
           {
               Author = "F. Scott Fitzgerald",
               Availibility = Availability.Available,
               Id = 4,
               ISBN = "9780743273565",
               Title = "The Great Gatsby",
           },
           new()
           {
               Author = "J.R.R. Tolkien",
               Availibility = Availability.Available,
               Id = 5,
               ISBN = "9780261103573",
               Title = "The Lord of the Rings",
           },
             new()
             {
                 Author = "Jane Austen",
                 Availibility = Availability.Available,
                 Id = 6,
                 ISBN = "9780141439518",
                 Title = "Pride and Prejudice",
             }];
        }
    }
}
