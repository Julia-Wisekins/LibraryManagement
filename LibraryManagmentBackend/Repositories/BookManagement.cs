using LibraryManagementBackend.Objects;
using System.Text;
using LibraryManagementBackend.Testing;

namespace LibraryManagementBackend.Repositories
{
    /// <summary>
    /// Managements all functionaility for the books 
    /// </summary>
    internal class BookManagement : IManage<Book>
    {
        private List<Book> _books;
        private IMediator _managementMediator;
        #region Singleton
        private static BookManagement _instance;

        public static BookManagement Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BookManagement();
                    _instance.Init();
                }
                return _instance;
            }
        }
        private void Init()
        {
            #region dummyData
            // Some sample data so everything is not empty
            _books = LibraryManagementServiceTest.Environment switch
            {
                Environment.Test => LibraryTestEnviormentData.LibraryBooks().ToList(),
                Environment.Live => new List<Book>(),
                _ => throw new ArgumentException($"invalid Enviorment: {LibraryManagementServiceTest.Environment}"),
            };
            #endregion
            _managementMediator = LibraryManagementServiceTest.GetManagementMediator();
        }
        #endregion


        /// <inheritdoc/>
        public async Task Add(Book entity)
        {
            await Validate(entity, true);
            _books.Add(entity);
        }

        /// <inheritdoc/>
        public async Task Delete(Book entity)
        {
            if (!_books.Remove(_books.FirstOrDefault(x => x.Id == entity.Id)))
            {
                throw new KeyNotFoundException(entity.Id.ToString());
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Book>> GetAll()
        {
            return _books.ToList();
        }

        /// <summary>
        /// Shallow copies the specific Book so that the UI can edit it without 
        /// changes to the backend until <see cref="Update(Book)"/> is called
        /// </summary>
        /// <param name="id">The Id of the book to get a copy of</param>
        /// <returns>A shallow copy of the book</returns>
        public async Task<IEnumerable<Book>> GetById(int id)
        {
            return _books.Where(x => x.Id == id).Select(ShallowCopy);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Book>> Search(string search)
        {
            IEnumerable<Book> idSearch = _books.Where(x => x.Id.ToString().Contains(search, StringComparison.CurrentCultureIgnoreCase));
            IEnumerable<Book> titleSearch = _books.Where(x => x.Title.Contains(search, StringComparison.CurrentCultureIgnoreCase));
            IEnumerable<Book> authorSearch = _books.Where(x => x.Author.Contains(search, StringComparison.CurrentCultureIgnoreCase));
            IEnumerable<Book> isbnSearch = _books.Where(x => x.ISBN.Contains(search, StringComparison.CurrentCultureIgnoreCase));
            IEnumerable<Book> availSearch = _books.Where(x => x.Availibility.ToString().Contains(search, StringComparison.CurrentCultureIgnoreCase));

            return idSearch.Union(titleSearch).Union(authorSearch).Union(availSearch).Union(isbnSearch);
        }

        /// <summary>
        /// Updates the books
        /// If books are set to anything but available it may need to use the Mediator to notify other systems
        /// </summary>
        /// <param name="entity">The book object that has the updates done</param>
        /// <returns>True if the book is updated otherwise false</returns>
        public async Task Update(Book entity)
        {

            Book bookToUpdate = _books.FirstOrDefault(x => x.Id == entity.Id);
            if(bookToUpdate == null)
            {
                throw new ArgumentException("Failed to find book inside of repository");
            }
            await Validate(entity, false);

            bookToUpdate.Title = entity.Title;
            bookToUpdate.Author = entity.Author;
            bookToUpdate.ISBN = entity.ISBN;
            if (bookToUpdate.Availibility != entity.Availibility
                && entity.Availibility == Availability.Available)
            {
                bookToUpdate.Availibility = entity.Availibility;
                _managementMediator.Notify(entity, this, BookEvent.Returned);
            }
            else if (entity.Availibility == Availability.LostOrDamaged
                || entity.Availibility == Availability.Removed)
            {
                bookToUpdate.Availibility = entity.Availibility;
                _managementMediator.Notify(entity, this, BookEvent.Disposed);
            }
            else
            {
                bookToUpdate.Availibility = entity.Availibility;
            }
        }

        /// <summary>
        /// Copies the book to a new object to be returned to the UI so it isn't editing the backend directly
        /// </summary>
        /// <param name="book">The book that will be copied</param>
        /// <returns>The shallow copy of the book</returns>
        private Book ShallowCopy(Book book)
        {
            Book copy = new Book()
            {
                Id = book.Id,
            };
            copy.Title = book.Title;
            copy.Author = book.Author;
            copy.ISBN = book.ISBN;
            copy.Availibility = book.Availibility;
            return copy;
        }

        /// <summary>
        /// Updates the availibility of the book depending on <paramref name="e"/>
        /// </summary>
        /// <param name="b">The book that the notification is about</param>
        /// <param name="e">The type of event that is happening <see cref="BookEvent"/></param>
        /// <param name="sender">The sender of the event</param>
        /// <exception cref="ArgumentException">Throws if the book attempting to be borrowed is not available</exception>
        public void UpdateBorrowState(Book b, BookEvent e, object sender)
        {
            // Need to get the local version not the copy
            b = _books.FirstOrDefault(x => x.Id == b.Id);
            if (b != null)
            {
                switch (e)
                {
                    case BookEvent.Borrowed:
                        if (b.Availibility != Availability.Available)
                        {
                            throw new ArgumentException($"Unable to assign a book ({b.Id}: {b.Title} by {b.Author}) to a reader. Please assign an availible book");
                        }
                        b.Availibility = Availability.Unavailable;
                        break;
                    case BookEvent.Returned:
                        b.Availibility = Availability.Available;
                        break;
                    case BookEvent.Disposed:
                        b.Availibility = Availability.LostOrDamaged;
                        break;
                }
            }
        }


        private async Task Validate(Book b, bool OnCreate)
        {
            var error = false;
            var sb = new StringBuilder();
            sb.AppendLine($"Error Creating {nameof(Book)}:");
            if (b == null)
            {
                sb.AppendLine($"\n{nameof(Book)} must not be null.");
                throw new ArgumentNullException(sb.ToString());
            }

            var id = $"Invalid {nameof(Book)} {nameof(Book.Id)}";
            if (b.Id < 1)
            {
                sb.AppendLine($"{id}: must not be less than 1");
                error = true;
            }

            if (OnCreate && (await GetAll()).Any(x => x.Id == b.Id))
            {
                sb.AppendLine($"{id}: Already Exisits");
                error = true;
            }

            var title = $"Invalid {nameof(Book)} {nameof(Book.Title)}";
            if (string.IsNullOrWhiteSpace(b.Title))
            {
                sb.AppendLine($"{title}: must contain a value");
                error = true;
            }

            var author = $"Invalid {nameof(Book)} {nameof(Book.Author)}";
            if (string.IsNullOrWhiteSpace(b.Author))
            {
                sb.AppendLine($"{author}: must contain a value");
                error = true;
            }

            var isbn = $"Invalid {nameof(Book)} {nameof(Book.ISBN)}";
            if (b.ISBN.Length < 10 || b.ISBN.Length > 13)
            {
                sb.AppendLine($"{isbn}: invlid");
                error = true;
            }

            if (error)
            {
                throw new ArgumentException(sb.ToString());
            }

        }
    }
}
