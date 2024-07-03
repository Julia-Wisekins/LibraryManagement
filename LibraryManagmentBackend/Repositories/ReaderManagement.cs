using LibraryManagementBackend.Objects;
using LibraryManagementBackend.Interface;
using System.Text;

namespace LibraryManagementBackend.Repositories
{
    internal class ReaderManagement : IManage<Reader>
    {
        List<Reader> _readers = new List<Reader>();
        IManagementMediator _managementMediator;
        #region Singleton
        private static ReaderManagement _instance;

        public static ReaderManagement Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ReaderManagement();
                    _instance.Init();
                }
                return _instance;
            }
        }
        private void Init()
        {
            var reader1 = new Reader
            {
                Id = 1,
                Name = "Aragorn Elessar",
            };
            var reader2 = new Reader
            {
                Id = 2,
                Name = "Lyra Silvertongue",
            };
            var reader3 = new Reader
            {
                Id = 3,
                Name = "Eragon Shadeslayer",
            };

            _readers.Add(reader1);
            _readers.Add(reader2);
            _readers.Add(reader3);
            _managementMediator = LibraryManagementService.GetManagementMediator();
        }
        #endregion

        /// <inheritdoc/>
        public async Task Add(Reader entity)
        {
            await Validate(entity, true);
            _readers.Add(entity);
        }

        /// <inheritdoc/>
        public async Task Delete(Reader entity)
        {
            if (!_readers.Remove(_readers.FirstOrDefault(x => x.Id == entity.Id)))
            {
                throw new KeyNotFoundException(entity.Id.ToString());
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Reader>> GetAll()
        {
            return ShallowCopy(_readers);
        }

        /// <summary>
        /// Shallow copies the specific Reader so that the UI can edit it without 
        /// changes to the backend until <see cref="Update(Reader)"/> is called
        /// </summary>
        /// <param name="id">The Id of the reader to get a copy of</param>
        /// <returns>A shallow copy of the reader</returns>
        public async Task<IEnumerable<Reader>> GetById(int id)
        {
            return _readers.Where(x => x.Id == id).Select(ShallowCopy);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Reader>> Search(string search)
        {
            IEnumerable<Reader> idSearch = _readers.Where(x => x.Id.ToString().Contains(search, StringComparison.CurrentCultureIgnoreCase));
            IEnumerable<Reader> nameSearch = _readers.Where(x => x.Name.Contains(search, StringComparison.CurrentCultureIgnoreCase));
            IEnumerable<Reader> bookSearch = _readers.Where(x => x.BorrowedBooks.Any(j => j.Title.Contains(search, StringComparison.CurrentCultureIgnoreCase)));

            return idSearch.Union(nameSearch).Union(bookSearch);
        }

        /// <summary>
        /// Updates the reader
        /// First we check which books we need to add or remove. If the book is not availible on add throws an exception
        /// After the <see cref="_managementMediator"/> is notified then we update the reader
        /// </summary>
        /// <param name="entity">Reader containing updated data</param>
        /// <returns>True if the update was successful otherwise false</returns>
        /// <exception cref="ArgumentException">Thrown if the book is not available when attempting to add it</exception>
        public async Task Update(Reader entity)
        {
            Reader readerToUpdate = _readers.FirstOrDefault(x => x.Id == entity.Id);
            await Validate(entity, false);

            IEnumerable<Book> booksToRemove = readerToUpdate.BorrowedBooks.Except(entity.BorrowedBooks);
            IEnumerable<Book> booksToAdd = entity.BorrowedBooks.Except(readerToUpdate.BorrowedBooks);

            if (booksToAdd.Any(x => x.Availibility != Availability.Available))
            {
                throw new ArgumentException("Unable to assign a book to a reader that is not available");
                
            }

            foreach (var item in booksToRemove)
            {
                _managementMediator.Notify(item, this, BookEvent.Returned);
            }
            foreach (var item in booksToAdd)
            {
                _managementMediator.Notify(item, this, BookEvent.Borrowed);
            }
            readerToUpdate.Name = entity.Name;
            readerToUpdate.BorrowedBooks = entity.BorrowedBooks;
        }

        /// <summary>
        /// Copies the reader to a new object to be returned to the UI so it isn't editing the backend directly
        /// </summary>
        /// <param name="source">The reader that will be copied</param>
        /// <returns>The shallow copy of the reader</returns>
        private Reader ShallowCopy(Reader source)
        {
            var copy = new Reader()
            {
                Id = source.Id,
                Name = source.Name,
                BorrowedBooks = source.BorrowedBooks,
            };
            return copy;
        }

        /// <summary>
        /// Copies the readers inside the Repository
        /// </summary>
        /// <param name="source">The reposiitory data to be shallow copied</param>
        /// <returns>A new list of copied data</returns>
        private IEnumerable<Reader> ShallowCopy(IEnumerable<Reader> source)
        {
            var copy = new List<Reader>();
            foreach (Reader reader in source)
                copy.Add(ShallowCopy(reader));

            return copy;
        }

        /// <summary>
        /// Removes all books from the readers if they are being set to returned or disposed in <paramref name="e"/>
        /// </summary>
        /// <param name="b">The book that the notification is about</param>
        /// <param name="e">The type of event that is happening <see cref="BookEvent"/></param>
        /// <param name="sender">The sender of the event</param>
        public void UpdateBorrowState(Book b, BookEvent e, object sender)
        {
            if (e == BookEvent.Returned || e == BookEvent.Disposed)
            {
                foreach (var item in _readers)
                {
                    item.BorrowedBooks = item.BorrowedBooks.Where(x => x.Id != b.Id);
                }
            }
        }


        private async Task Validate(Reader r, bool OnCreate)
        {
            var error = false;
            var sb = new StringBuilder();
            sb.AppendLine($"Error Creating {nameof(Reader)}:");
            if (r == null)
            {
                sb.AppendLine($"\n{nameof(Reader)} must not be null.");
                throw new ArgumentNullException(sb.ToString());
            }

            var id = $"Invalid {nameof(Reader)} {nameof(Reader.Id)}";
            if (r.Id < 1)
            {
                sb.AppendLine($"{id}: must not be less than 1");
                error = true;
            }

            if (OnCreate && (await GetAll()).Any(x => x.Id == r.Id))
            {
                sb.AppendLine($"{id}: Already Exisits");
                error = true;
            }

            var title = $"Invalid {nameof(Reader)} {nameof(Reader.Name)}";
            if (string.IsNullOrWhiteSpace(r.Name))
            {
                sb.AppendLine($"{title}: must contain a value");
                error = true;
            }

            if (error)
            {
                throw new ArgumentException(sb.ToString());
            }

        }
    }
}
