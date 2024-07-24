using LibraryManagementBackend.Objects;
using LibraryManagementBackend.Testing;

namespace LibraryTest
{
    internal class LibraryTestCases
    {
        internal static IEnumerable<int> ExpectedExistingBookCount()
        {
            yield return LibraryTestEnviormentData.LibraryBooks().Count();
        }
        #region Books
        internal static IEnumerable<Book> ExistingBooks()
        {
            var books = LibraryTestEnviormentData.LibraryBooks();
            foreach (var book in books)
            {
                yield return book;
            }
        }
        internal static IEnumerable<Book> BooksExpected()
        {
            yield return
             new Book()
             {
                 Id = 1000001,
                 Title = "The Very Hungry Caterpillar",
                 Author = "Eric Carle",
                 Availibility = Availability.Available,
                 ISBN = "0-399-22690-7",

             };
            yield return
                new Book()
                {
                    Id = 1000002,
                    Title = "Charlotte's Web",
                    Author = "E. B. White",
                    Availibility = Availability.Available,
                    ISBN = "9780062658753",

                };
            yield return
                new Book()
                {
                    Id = 1000003,
                    Title = "Matilda",
                    Author = "Roald Dahl",
                    Availibility = Availability.Available,
                    ISBN = "9780140327595",

                };
            yield return
                new Book()
                {
                    Id = 1000004,
                    Title = "The Lion, the Witch and the Wardrobe",
                    Author = "Pauline Baynes",
                    Availibility = Availability.Available,
                    ISBN = "9780001831803",
                };
        }
        internal static IEnumerable<Book> BookEmpty()
        {
            yield return
             new Book();
        }
        internal static IEnumerable<Book> BooksNoId()
        {
            yield return
             new Book()
             {
                 Title = "The Very Hungry Caterpillar",
                 Author = "Eric Carle",
                 Availibility = Availability.Available,
                 ISBN = "0-399-22690-7",

             };
            yield return
                new Book()
                {
                    Title = "Charlotte's Web",
                    Author = "E. B. White",
                    Availibility = Availability.Available,
                    ISBN = "9780062658753",

                };
            yield return
                new Book()
                {
                    Title = "Matilda",
                    Author = "Roald Dahl",
                    Availibility = Availability.Available,
                    ISBN = "9780140327595",

                };
            yield return
                new Book()
                {
                    Title = "The Lion, the Witch and the Wardrobe",
                    Author = "Pauline Baynes",
                    Availibility = Availability.Available,
                    ISBN = "9780001831803",
                };
        }
        internal static IEnumerable<Book> NoTitleBook()
        {
            yield return
             new Book()
             {
                 Id = 1200001,
                 Author = "Eric Carle",
                 Availibility = Availability.Available,
                 ISBN = "0-399-22690-7",

             };
            yield return
                new Book()
                {
                    Id = 1200002,
                    Author = "E. B. White",
                    Availibility = Availability.Available,
                    ISBN = "9780062658753",

                };
            yield return
                new Book()
                {
                    Id = 1200003,
                    Author = "Roald Dahl",
                    Availibility = Availability.Available,
                    ISBN = "9780140327595",

                };
            yield return
                new Book()
                {
                    Id = 1200004,
                    Author = "Pauline Baynes",
                    Availibility = Availability.Available,
                    ISBN = "9780001831803",
                };
        }
        internal static IEnumerable<Book> BookNoAuther()
        {
            yield return
             new Book()
             {
                 Id = 1300001,
                 Title = "The Very Hungry Caterpillar",
                 Availibility = Availability.Available,
                 ISBN = "0-399-22690-7",

             };
            yield return
                new Book()
                {
                    Id = 1300002,
                    Title = "Charlotte's Web",
                    Availibility = Availability.Available,
                    ISBN = "9780062658753",

                };
            yield return
                new Book()
                {
                    Id = 1300003,
                    Title = "Matilda",
                    Availibility = Availability.Available,
                    ISBN = "9780140327595",

                };
            yield return
                new Book()
                {
                    Id = 1300004,
                    Title = "The Lion, the Witch and the Wardrobe",
                    Availibility = Availability.Available,
                    ISBN = "9780001831803",
                };
        }
        internal static IEnumerable<Book> BookNoAvalability()
        {
            yield return
             new Book()
             {
                 Id = 1000001,
                 Title = "The Very Hungry Caterpillar",
                 Author = "Eric Carle",
                 ISBN = "0-399-22690-7",

             };
            yield return
                new Book()
                {
                    Id = 1000002,
                    Title = "Charlotte's Web",
                    Author = "E. B. White",
                    ISBN = "9780062658753",

                };
            yield return
                new Book()
                {
                    Id = 1000003,
                    Title = "Matilda",
                    Author = "Roald Dahl",
                    ISBN = "9780140327595",

                };
            yield return
                new Book()
                {
                    Id = 1000004,
                    Title = "The Lion, the Witch and the Wardrobe",
                    Author = "Pauline Baynes",
                    ISBN = "9780001831803",
                };
        }
        internal static IEnumerable<Book> BookNoISBN()
        {
            yield return
             new Book()
             {
                 Id = 1000001,
                 Title = "The Very Hungry Caterpillar",
                 Author = "Eric Carle",
                 ISBN = "0-399-22690-7",

             };
            yield return
                new Book()
                {
                    Id = 1000002,
                    Title = "Charlotte's Web",
                    Author = "E. B. White",
                    ISBN = "9780062658753",

                };
            yield return
                new Book()
                {
                    Id = 1000003,
                    Title = "Matilda",
                    Author = "Roald Dahl",
                    ISBN = "9780140327595",

                };
            yield return
                new Book()
                {
                    Id = 1000004,
                    Title = "The Lion, the Witch and the Wardrobe",
                    Author = "Pauline Baynes",
                    ISBN = "9780001831803",
                };
        }
        internal static IEnumerable<Book> BookUpdateTitle()
        {
            var books = new List<Book>();
            var bookForUpdate = LibraryTestEnviormentData.LibraryBooks().FirstOrDefault();
            yield return
                new Book
                {
                    Id = bookForUpdate.Id,
                    Title = "Book Title",
                    Author = bookForUpdate.Author,
                    Availibility = bookForUpdate.Availibility,
                    ISBN = bookForUpdate.ISBN,
                };

        }
        internal static IEnumerable<Book> BookUpdateAuthor()
        {
            var books = new List<Book>();
            var bookForUpdate = LibraryTestEnviormentData.LibraryBooks().FirstOrDefault();
            yield return
                new Book
                {
                    Id = bookForUpdate.Id,
                    Title = bookForUpdate.Title,
                    Author = bookForUpdate.Author,
                    Availibility = bookForUpdate.Availibility,
                    ISBN = bookForUpdate.ISBN,
                };
        }
        internal static IEnumerable<Book> BookUpdateAvalibility()
        {
            var books = new List<Book>();
            var bookForUpdate = LibraryTestEnviormentData.LibraryBooks().FirstOrDefault();
            yield return
                new Book
                {
                    Id = bookForUpdate.Id,
                    Title = bookForUpdate.Title,
                    Author = bookForUpdate.Author,
                    Availibility = bookForUpdate.Availibility,
                    ISBN = bookForUpdate.ISBN,
                };
        }
        internal static IEnumerable<Book> BookUpdateISBN()
        {
            var books = new List<Book>();
            var bookForUpdate = LibraryTestEnviormentData.LibraryBooks().FirstOrDefault();
             yield return new Book
                {
                    Id = bookForUpdate.Id,
                    Title = bookForUpdate.Title,
                    Author = bookForUpdate.Author,
                    Availibility = bookForUpdate.Availibility,
                    ISBN = bookForUpdate.ISBN,
                };
        }
        internal static IEnumerable<Book> BookUpdateFullChange()
        {
            var books = new List<Book>();
            var bookForUpdate = LibraryTestEnviormentData.LibraryBooks().FirstOrDefault();
            yield return
                new Book
                {
                    Id = bookForUpdate.Id,
                    Title = bookForUpdate.Title+" Test",
                    Author = bookForUpdate.Author+ " Test",
                    Availibility = bookForUpdate.Availibility,
                    ISBN = "12345678910",
                };
        }
        #endregion
        #region Readers
        internal static int ExpectedStartingReaderCount()
        {
            return LibraryTestEnviormentData.LibraryReaders().Count();
        }
        internal static IEnumerable<Reader> ExisingReaders()
        {
            var readers = LibraryTestEnviormentData.LibraryReaders();
            foreach (var reader in readers)
            {
                yield return reader;
            }
        }
        internal static IEnumerable<Reader> ReadersExpected()
        {
            yield return
             new Reader()
             {
                 Id = 2000001,
                 Name = "Winnie-the-Pooh",

             };
            yield return new Reader()
            {
                Id = 2000002,
                Name = "Harry Potter",
            };
            yield return new Reader()
            {
                Id = 2000003,
                Name = "Paddington",
            };
            yield return new Reader()
            {
                Id = 2000004,
                Name = "Peter Pan",
            };
        }
        internal static IEnumerable<Reader> ReadersNoId()
        {
            yield return
            new Reader()
            {
                Name = "Winnie-the-Pooh",

            };
            yield return new Reader()
            {
                Name = "Harry Potter",
            };
            yield return new Reader()
            {
                Name = "Paddington",
            };
            yield return new Reader()
            {
                Name = "Peter Pan",
            };
        }
        internal static IEnumerable<Reader>  ReaderEmpty()
        {
            yield return new Reader();
        }
        internal static IEnumerable<Reader> ReadersNoName()
        {
            yield return
            new Reader()
            {
                Id = 2100001,

            };
            yield return new Reader()
            {
                Id = 2100002,
            };
            yield return new Reader()
            {
                Id = 2100003,
            };
            yield return new Reader()
            {
                Id = 2100004,
            };
        }
        #endregion
        #region BookAndReader
        internal static IEnumerable<object[]> ExistingBookAndReader()
        {
            var bookAndReader = new List<object[]>();
            var books = LibraryTestEnviormentData.LibraryBooks();
            var readers = LibraryTestEnviormentData.LibraryBooks();

            foreach (var book in books)
            {
                foreach (var reader in readers)
                {
                    bookAndReader.Add(
                    [
                        book.Copy(),
                        reader.Copy()
                    ]);
                }
            }
            return bookAndReader;
        }
        internal static IEnumerable<object[]> BookEmptyAndReaderEmpty()
        {
            yield return [new Book(), new Reader()];
        }
        #endregion
    }
}
