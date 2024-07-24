using LibraryManagementBackend.Objects;

namespace LibraryManagementBackend
{
    /// <summary>
    /// Handles communication between the Book and Reader repositories
    /// </summary>
    internal interface IMediator
    {
        /// <summary>
        /// Notifies the repositories of any changes to books
        /// </summary>
        /// <param name="book">The book that is updated</param>
        /// <param name="sender">The <see cref="IRepository{T}"/> that sent the event</param>
        /// <param name="state">The new state of that book</param>
        void Notify(Book book, object sender, BookEvent state);
    }
}
