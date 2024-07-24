using LibraryManagementBackend.Objects;
using LibraryManagementBackend.Repositories;

namespace LibraryManagementBackend.Testing
{
    /// <summary>
    /// Static system used to get singletons for injection in the front end
    /// </summary>
    public static class LibraryManagementServiceTest
    {
        public static readonly Environment Environment = Environment.Test;
        private static readonly IManage<Book> _bookManager = BookManagement.Instance;
        private static readonly IManage<Reader> _readerManager = ReaderManagement.Instance;
        private static readonly IMediator _managementMediator = ManagementMediator.Instance;

        /// <summary>
        /// Gets the <see cref="BookManagement"/> as a <see cref="IRepository{T}"/> for the front end
        /// </summary>
        /// <returns>The singleton instance of <see cref="BookManagement"/></returns>
        public static IRepository<Book> BookRepository()
        {
            return _bookManager;
        }

        /// <summary>
        /// Gets the <see cref="ReaderManagement"/> as a <see cref="IRepository{T}"/> for the front end
        /// </summary>
        /// <returns>The singleton instance of <see cref="ReaderManagement"/></returns>
        public static IRepository<Reader> ReaderResository()
        {
            return _readerManager;
        }

        /// <summary>
        /// Gets the <see cref="ManagementMediator"/> as a <see cref="IMediator"/> for the back end
        /// </summary>
        /// <returns>The singleton instance of <see cref="ManagementMediator"/></returns>
        internal static IMediator GetManagementMediator()
        {
            return _managementMediator;
        }

        /// <summary>
        /// Gets the <see cref="BookManagement"/> as a <see cref="IManage{T}"/> for the back end
        /// </summary>
        /// <returns>The singleton instance of <see cref="BookManagement"/></returns>
        internal static IManage<Book> GetBookManager()
        {
            return _bookManager;
        }

        /// <summary>
        /// Gets the <see cref="BookManagement"/> as a <see cref="IManage{T}"/> for the back end
        /// </summary>
        /// <returns>The singleton instance of <see cref="BookManagement"/></returns>
        internal static IManage<Reader> GetReaderManager()
        {
            return _readerManager;
        }
    }
}
