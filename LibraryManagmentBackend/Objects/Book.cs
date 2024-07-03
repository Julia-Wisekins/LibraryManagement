using System.ComponentModel;

namespace LibraryManagementBackend.Objects
{
    /// <summary>
    /// Definitions of the books
    /// Simple class with no functionality to emulate if it were a database
    /// </summary>
    public class Book
    {
        [Description("Book ID")]
        public int Id { get; init; }
        public string Title { get;  set; } = String.Empty;
        public string Author { get;  set; } = String.Empty;
        public string ISBN { get;  set; } = String.Empty;
        public Availability Availibility { get;  set; }
    }
}
