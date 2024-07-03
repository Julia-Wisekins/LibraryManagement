using LibraryManagementBackend.Objects;
using LibraryManagementBackend.Repositories;

namespace LibraryManagementBackend.Interface
{
    /// <summary>
    /// Static system used to get singletons for injection in the front end
    /// </summary>
    public static class LibraryManagementService
    {
        /// <summary>
        /// Gets the <see cref="BookManagement"/> as a <see cref="IRepository{T}"/> for the front end
        /// </summary>
        /// <returns>The singleton instance of <see cref="BookManagement"/></returns>
        public static IRepository<Book> BookRepository()
        {
            return (IRepository<Book>)BookManagement.Instance;
        }

        /// <summary>
        /// Gets the <see cref="ReaderManagement"/> as a <see cref="IRepository{T}"/> for the front end
        /// </summary>
        /// <returns>The singleton instance of <see cref="ReaderManagement"/></returns>
        public static IRepository<Reader> ReaderResository()
        {
            return (IRepository<Reader>)ReaderManagement.Instance;
        }

        /// <summary>
        /// Gets the <see cref="ManagementMediator"/> as a <see cref="IManagementMediator"/> for the back end
        /// </summary>
        /// <returns>The singleton instance of <see cref="ManagementMediator"/></returns>
        internal static IManagementMediator GetManagementMediator()
        {
            return ManagementMediator.Instance;
        }

        /// <summary>
        /// Gets the <see cref="BookManagement"/> as a <see cref="IManage{T}"/> for the back end
        /// </summary>
        /// <returns>The singleton instance of <see cref="BookManagement"/></returns>
        internal static IManage<Book> GetBookManager()
        {
            return (IManage<Book>)BookManagement.Instance;
        }

        /// <summary>
        /// Gets the <see cref="BookManagement"/> as a <see cref="IManage{T}"/> for the back end
        /// </summary>
        /// <returns>The singleton instance of <see cref="BookManagement"/></returns>
        internal static IManage<Reader> GetReaderManager()
        {
            return (IManage<Reader>)ReaderManagement.Instance;
        }

    }
}
