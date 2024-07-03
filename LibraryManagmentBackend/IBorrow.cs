using LibraryManagementBackend.Objects;

namespace LibraryManagementBackend
{
    internal interface IBorrow
    {
        void UpdateBorrowState(Book b, BookEvent e, object sender); 
    }
}
