namespace LibraryManagementBackend.Objects
{
    /// <summary>
    /// Definitions for the reader
    /// Simple class with no functionality to emulate if it were a database
    /// </summary>
    public class Reader
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public IEnumerable<Book> BorrowedBooks { get; set; } = new List<Book>();    
    }
}
