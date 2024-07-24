
using LibraryManagementBackend.Objects;
using LibraryManagementBackend.Testing;

namespace LibraryTest
{
    internal class BookRepositoryTests
    {
        IRepository<Book> _bookRepository;
        [SetUp]
        public void Setup()
        {
            _bookRepository = LibraryManagementServiceTest.BookRepository();
        }

        [TestCaseSource(typeof(LibraryTestCases), nameof(LibraryTestCases.ExpectedExistingBookCount))]
        public void GetAll_Returns_Correct_Amount_Of_Books(int expected)
        {
            var books = _bookRepository.GetAll().Result;
            Assert.AreEqual(expected, books.Count());
        }
        #region GetByID
        [TestCaseSource(typeof(LibraryTestCases), nameof(LibraryTestCases.ExistingBooks))]
        public void GetByID_Gets_Existing_Books(Book testBook)
        {
            var foundBook = _bookRepository.GetById(testBook.Id).Result.FirstOrDefault();

            Assert.IsNotNull(foundBook);            
        }
        [TestCaseSource(typeof(LibraryTestCases), nameof(LibraryTestCases.BooksExpected))]
        public void GetByID_Returns_Null_If_Book_Doesnt_Exist(Book testBook)
        {
            var foundBook = _bookRepository.GetById(testBook.Id).Result.FirstOrDefault();
            Assert.IsNull(foundBook);
        }
        #endregion
        #region Update
        [TestCaseSource(typeof(LibraryTestCases), nameof(LibraryTestCases.BooksExpected)) ]
        public void Update_Throws_If_Null_ID_Is_Passed (Book testBook)
        {
            var ex = Assert.Throws<AggregateException>(() => _bookRepository.Update(testBook).Wait());
            Assert.That(ex.InnerException, Is.TypeOf<ArgumentException>());
        }

        [TestCaseSource(typeof(LibraryTestCases), nameof(LibraryTestCases.BookUpdateTitle))]
        public void Update_Changes_Only_Title_When_Only_Title_Changed(Book testBook)
        {
            Book originalBook = _bookRepository.GetById(testBook.Id).Result.FirstOrDefault();
            _bookRepository.Update(testBook);
            Book updatedBook = _bookRepository.GetById(testBook.Id).Result.FirstOrDefault();
            Assert.IsNotNull(updatedBook);

            Assert.That(updatedBook.ISBN, Is.EqualTo(originalBook.ISBN));
            Assert.That(updatedBook.Author, Is.EqualTo(originalBook.Author));
            Assert.That(updatedBook.Title, Is.EqualTo(testBook.Title));
            Assert.That(updatedBook.Availibility, Is.EqualTo(originalBook.Availibility));
            Assert.That(updatedBook.Id, Is.EqualTo(originalBook.Id));


            _bookRepository.Update(originalBook);
        }

        [TestCaseSource(typeof(LibraryTestCases), nameof(LibraryTestCases.BookUpdateAuthor))]
        public void Update_Changes_Only_Author_When_Only_Author_Changed(Book testBook)
        {
            Book originalBook = _bookRepository.GetById(testBook.Id).Result.FirstOrDefault();
            _bookRepository.Update(testBook);
            Book updatedBook = _bookRepository.GetById(testBook.Id).Result.FirstOrDefault();
            Assert.IsNotNull(updatedBook);

            Assert.That(updatedBook.ISBN, Is.EqualTo(originalBook.ISBN));
            Assert.That(updatedBook.Author, Is.EqualTo(testBook.Author));
            Assert.That(updatedBook.Title, Is.EqualTo(originalBook.Title));
            Assert.That(updatedBook.Availibility, Is.EqualTo(originalBook.Availibility));
            Assert.That(updatedBook.Id, Is.EqualTo(originalBook.Id));

            _bookRepository.Update(originalBook);
        }

        [TestCaseSource(typeof(LibraryTestCases), nameof(LibraryTestCases.BookUpdateAvalibility))]
        public void Update_Changes_Only_Avalibility_When_Only_Avalibility_Changed(Book testBook)
        {
            Book originalBook = _bookRepository.GetById(testBook.Id).Result.FirstOrDefault();
            _bookRepository.Update(testBook);
            Book updatedBook = _bookRepository.GetById(testBook.Id).Result.FirstOrDefault();
            Assert.IsNotNull(updatedBook);

            Assert.That(updatedBook.ISBN, Is.EqualTo(originalBook.ISBN));
            Assert.That(updatedBook.Author, Is.EqualTo(originalBook.Author));
            Assert.That(updatedBook.Title, Is.EqualTo(originalBook.Title));
            Assert.That(updatedBook.Availibility, Is.EqualTo(testBook.Availibility));
            Assert.That(updatedBook.Id, Is.EqualTo(originalBook.Id));

            _bookRepository.Update(originalBook);
        }

        [TestCaseSource(typeof(LibraryTestCases), nameof(LibraryTestCases.BookUpdateISBN))]
        public void Update_Changes_Only_ISBN_When_Only_ISBN_Changed(Book testBook)
        {
            Book originalBook = _bookRepository.GetById(testBook.Id).Result.FirstOrDefault();
            _bookRepository.Update(testBook);
            Book updatedBook = _bookRepository.GetById(testBook.Id).Result.FirstOrDefault();
            Assert.IsNotNull(updatedBook);

            Assert.That(updatedBook.ISBN, Is.EqualTo(testBook.ISBN));
            Assert.That(updatedBook.Author, Is.EqualTo(originalBook.Author));
            Assert.That(updatedBook.Title, Is.EqualTo(originalBook.Title));
            Assert.That(updatedBook.Availibility, Is.EqualTo(originalBook.Availibility));
            Assert.That(updatedBook.Id, Is.EqualTo(originalBook.Id));

            _bookRepository.Update(originalBook);
        }

        [TestCaseSource(typeof(LibraryTestCases), nameof(LibraryTestCases.BookUpdateFullChange))]
        public void Update_Changes_Book_When_All_Changed(Book testBook)
        {
            Book originalBook = _bookRepository.GetById(testBook.Id).Result.FirstOrDefault();
            _bookRepository.Update(testBook);
            Book updatedBook = _bookRepository.GetById(testBook.Id).Result.FirstOrDefault();
            Assert.IsNotNull(updatedBook);

            Assert.That(updatedBook.ISBN, Is.EqualTo(testBook.ISBN));
            Assert.That(updatedBook.Author, Is.EqualTo(testBook.Author));
            Assert.That(updatedBook.Title, Is.EqualTo(testBook.Title));
            Assert.That(updatedBook.Availibility, Is.EqualTo(testBook.Availibility));
            Assert.That(updatedBook.Id, Is.EqualTo(testBook.Id));

            _bookRepository.Update(originalBook);
        }
        #endregion
        #region Search

        [TestCaseSource(typeof(LibraryTestCases), nameof(LibraryTestCases.BookEmpty))]
        public void Search_Returns_Nothing_On_Empty_Search(Book testBook)
        {
            IEnumerable<Book> books = _bookRepository.Search(testBook.Title).Result;
            Assert.That(books.Count(), Is.EqualTo(LibraryTestCases.ExpectedExistingBookCount().First()));
        }
        [TestCaseSource(typeof(LibraryTestCases), nameof(LibraryTestCases.ExistingBooks))]
        public void Search_Returns_Expected_Book_Searching_By_Name(Book testBook)
        {
            IEnumerable<Book> books = _bookRepository.Search(testBook.Title).Result;
            Assert.That(books.Count(), Is.EqualTo(1));
            Assert.That(books.FirstOrDefault().Title, Is.EqualTo(testBook.Title));
        }
        [TestCaseSource(typeof(LibraryTestCases), nameof(LibraryTestCases.ExistingBooks))]
        public void Search_Returns_Expected_Book_Searching_By_ID(Book testBook)
        {
            IEnumerable<Book> books = _bookRepository.Search(testBook.Id.ToString()).Result;
            Assert.That(books.Count(), Is.GreaterThan(0));
        }
        [TestCaseSource(typeof(LibraryTestCases), nameof(LibraryTestCases.ExistingBooks))]
        public void Search_Returns_Expected_Book_Searching_By_Author(Book testBook)
        {
            IEnumerable<Book> books = _bookRepository.Search(testBook.Author).Result;
            Assert.That(books.Count(), Is.EqualTo(1));
            Assert.That(books.FirstOrDefault().Author, Is.EqualTo(testBook.Author));
        }
        [TestCaseSource(typeof(LibraryTestCases), nameof(LibraryTestCases.ExistingBooks))]
        public void Search_Returns_Expected_Book_Searching_By_Availability(Book testBook)
        {
            IEnumerable<Book> books = _bookRepository.Search(testBook.Availibility.ToString()).Result;
            Assert.That(books.Count(), Is.EqualTo(LibraryTestCases.ExpectedExistingBookCount().First()));
            Assert.That(books.FirstOrDefault().Availibility, Is.EqualTo(testBook.Availibility));
        }
        [TestCaseSource(typeof(LibraryTestCases), nameof(LibraryTestCases.ExistingBooks))]
        public void Search_Returns_Expected_Book_Searching_By_ISBN(Book testBook)
        {
            IEnumerable<Book> books = _bookRepository.Search(testBook.ISBN).Result;
            Assert.That(books.Count(), Is.EqualTo(1));
            Assert.That(books.FirstOrDefault().ISBN, Is.EqualTo(testBook.ISBN));
        }
        #endregion
        #region Add
        [TestCaseSource(typeof(LibraryTestCases), nameof(LibraryTestCases.BooksExpected))]
        public async Task Add_Adds_A_New_BookAsync(Book testBook)
        {
            var startingcount = _bookRepository.GetAll().Result.Count();
            var expected = startingcount + 1;
             await _bookRepository.Add(testBook);

            var newCount = _bookRepository.GetAll().Result.Count();
            Assert.That(startingcount < newCount);
            Assert.That(expected, Is.EqualTo(newCount));
        }
        #endregion
        #region Delete
        [TestCaseSource(typeof(LibraryTestCases), nameof(LibraryTestCases.BooksExpected))]
        public  async Task Delete_Deletes_A_BookAsync(Book testBook)
        {
            var startingcount = _bookRepository.GetAll().Result.Count();
            var expected = startingcount - 1;

             await _bookRepository.Delete(testBook);

            var newCount = _bookRepository.GetAll().Result.Count();

            Assert.That(startingcount > newCount);
            Assert.That(expected, Is.EqualTo(newCount));
        }
        #endregion
       

    }
}
