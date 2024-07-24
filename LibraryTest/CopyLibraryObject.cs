using LibraryManagementBackend.Objects;

namespace LibraryTest
{
    internal static class CopyLibraryObject 
    {
        internal static Book Copy(this Book book)
        {
            return new Book()
            {
                Id = book.Id,
                Author = book.Author,
                Availibility = book.Availibility,
                ISBN = book.ISBN,
                Title = book.Title,
            };
        }
        internal static Reader Copy(this Reader reader)
        {
            var books = new List<Book>();
            foreach (var b in reader.BorrowedBooks)
                books.Add(b.Copy());
            return new Reader()
            {
                Id = reader.Id,
                Name = reader.Name,
                BorrowedBooks = books,
            };
        }
    }
}
