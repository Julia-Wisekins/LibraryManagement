using LibraryManagementBackend.Interface;
using LibraryManagementBackend.Objects;

namespace LibraryTest
{
    public class Tests
    {
        IRepository<Book> _bookRepository;
        IRepository<Reader> _readerRepository;
        [SetUp]
        public void Setup()
        {
            _bookRepository = LibraryManagementService.BookRepository();
            _readerRepository = LibraryManagementService.ReaderResository();
        }

        [Test]
        public async Task SearchAUser()
        {
            int count = (await _readerRepository.GetAll()).Count();

            _readerRepository.Add(new Reader()
            {
                Id = 4,
                Name = "Steve",
            });

            List<Reader> readers = (await _readerRepository.GetAll()).ToList();

            Assert.That(readers.Count, Is.EqualTo(count + 1));

            Assert.That((await _readerRepository.Search("Steve")).Count(), Is.EqualTo(1));

            Assert.Pass();
        }

        [Test]
        public async Task UpdateUser()
        {
            int count = (await _readerRepository.GetAll()).Count();

            _readerRepository.Add(new Reader()
            {
                Id = 5,
                Name = "Bill",
            }).Wait();

            List<Reader> readers = (await _readerRepository.GetAll()).ToList();

            Assert.That(readers.Count, Is.EqualTo(count + 1));
            Assert.That((await _readerRepository.Search("Bill")).Count(), Is.EqualTo(1));
            Assert.That((await _readerRepository.Search("Orange")).Count(), Is.EqualTo(0));

            _readerRepository.Update(new Reader()
            {
                Id = 5,
                Name = "Orange",
            }).Wait();

            Assert.That((await _readerRepository.Search("Bill")).Count(), Is.EqualTo(0));
            Assert.That((await _readerRepository.Search("Orange")).Count(), Is.EqualTo(1));

            Assert.Pass();
        }

        [Test]
        public async Task UpdateBook()
        {
            int count = (await _bookRepository.GetAll()).Count();

            _bookRepository.Add(new Book()
            {
                Id = 7,
                Title = "Random Book Name",
                Author = "Test Book Author",
                Availibility = Availability.Available,
                ISBN = "217842604162",
            }).Wait();

            List<Book> books = (await _bookRepository.GetAll()).ToList();

            Assert.That(books.Count, Is.EqualTo(count + 1));
            Assert.That((await _bookRepository.Search("Random Book Name")).Count(), Is.EqualTo(1));
            Assert.That((await _bookRepository.Search("Different Book Name")).Count(), Is.EqualTo(0));

            _bookRepository.Update(new Book()
            {
                Id = 7,
                Title = "Different Book Name",
                Author = "Test Book Author",
                Availibility = Availability.Available,
                ISBN = "217174201947",
            }).Wait();

            Assert.That((await _bookRepository.Search("Random Book Name")).Count(), Is.EqualTo(0));
            Assert.That((await _bookRepository.Search("Different Book Name")).Count(), Is.EqualTo(1));

            Assert.Pass();
        }

        [Test]
        public async Task ReturnedBook()
        {
            int count = (await _bookRepository.GetAll()).Count();
            Book book = new Book()
            {
                Id = 8,
                Title = "Testing returning books",
                Author = "Testing returning author",
                Availibility = Availability.Unavailable,
                ISBN = "212418448209",
            };

            _bookRepository.Add(book).Wait();

            _readerRepository.Add(new Reader()
            {
                Id = 6,
                BorrowedBooks = new List<Book>()
                {
                    book,
                },
                Name = "Gentle Reader",
            }).Wait();

            Reader reader = (await _readerRepository.Search("Gentle Reader")).FirstOrDefault();
            Assert.That(reader.BorrowedBooks.Count(), Is.EqualTo(1));

            Book updateBook = new Book()
            {
                Id = 8,
                Title = "Testing returning books",
                Author = "Testing returning author",
                Availibility = Availability.Available,
                ISBN = "217190247124",
            };
            _bookRepository.Update(updateBook).Wait();
            reader = (await _readerRepository.Search("Gentle Reader")).FirstOrDefault();
            Assert.That(reader.BorrowedBooks.Count(), Is.EqualTo(0));

            Assert.Pass();
        }

        [Test]
        public async Task UserBorrows()
        {
            int count = (await _bookRepository.GetAll()).Count();
            Book book = new Book()
            {
                Id = 9,
                Title = "Testing user borrowing books",
                Author = "Testing user borrowing author",
                Availibility = Availability.Available,
                ISBN = "240912891844",
            };

            _bookRepository.Add(book).Wait();

            _readerRepository.Add(new Reader()
            {
                Id = 7,
                Name = "Goldilocks",
            }).Wait();

            _readerRepository.Update(new Reader()
            {
                Id = 7,
                Name = "Goldilocks",
                BorrowedBooks = new List<Book>() { book }
            }).Wait();

            Assert.That(book.Availibility, Is.EqualTo(Availability.Unavailable));
            Reader reader = (await _readerRepository.Search("Goldilocks")).FirstOrDefault();

            Assert.That(reader.BorrowedBooks.FirstOrDefault(), Is.EqualTo(book));

            Assert.Pass();
        }

        [Test]
        public async Task UserReturns()
        {
            int count = (await _bookRepository.GetAll()).Count();
            Book book = new Book()
            {
                Id = 10,
                Title = "Testing user returning books",
                Author = "Testing user returning author",
                Availibility = Availability.Unavailable,
                ISBN = "597012579185",
            };

            _bookRepository.Add(book).Wait();

            _readerRepository.Add(new Reader()
            {
                Id = 8,
                BorrowedBooks = new List<Book>()
                {
                    book,
                },
                Name = "Vladimir",
            }).Wait();

            Reader reader = (await _readerRepository.Search("Vladimir")).FirstOrDefault();
            Assert.That(reader.BorrowedBooks.Count(), Is.EqualTo(1));

            _readerRepository.Update(new Reader()
            {
                Id = 8,
                Name = "Vladimir",
            }).Wait();

            book = (await _bookRepository.GetById(10)).FirstOrDefault();

            Assert.That(book.Availibility, Is.EqualTo(Availability.Available));

            reader = (await _readerRepository.Search("Vladimir")).FirstOrDefault();
            Assert.That(reader.BorrowedBooks.Count(), Is.EqualTo(0));

            Assert.Pass();
        }
    }
}