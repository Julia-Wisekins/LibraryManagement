using LibraryManagementBackend.Objects;
using LibraryManagementBackend.Interface;
using LibraryManagementBackend.Repositories;

namespace LibraryManagementBackend
{
    internal class ManagementMediator : IManagementMediator
    {
        private IManage<Book> Books { get; set; }
        private IManage<Reader> Readers { get; set; }


        #region Singleton
        private static ManagementMediator _instance;

        public static ManagementMediator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ManagementMediator();
                    _instance.Init();
                }
                return _instance;
            }
        }
        #endregion

        /// <summary>
        /// Initialises the books and readers to the singleton instances
        /// </summary>
        private void Init()
        {
            Books = LibraryManagementService.GetBookManager();
            Readers = LibraryManagementService.GetReaderManager();
        }

        /// <inheritdoc/>
        public void Notify(Book book, object sender, BookEvent state)
        {
            if (sender == Books)
            {
                Readers.UpdateBorrowState(book, state, sender);
            } 
            else if (sender == Readers)
            {
                Books.UpdateBorrowState(book, state, sender);
            }
        }
    }
}
