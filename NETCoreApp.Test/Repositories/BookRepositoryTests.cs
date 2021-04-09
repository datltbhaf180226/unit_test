using NUnit.Framework;
using NETCoreApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NETCoreApp.Data;

namespace NETCoreApp.Repositories.Tests
{
    [TestFixture()]
    public class BookRepositoryTests
    {
        private static readonly DbContextOptions<MyDbContext> _options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        private static readonly MyDbContext _context = new(_options);

        public BookRepositoryTests()
        {

            var repository = new BookRepository(_context);
            repository.Insert(new Models.Book { Id = 1, Title = "Book 1", AuthorId = 1 });
            repository.Insert(new Models.Book { Id = 2, Title = "Book 2", AuthorId = 1 });
            repository.Insert(new Models.Book { Id = 3, Title = "Book 3", AuthorId = 1 });
            repository.Insert(new Models.Book { Id = 4, Title = "Book 4", AuthorId = 1 });
            repository.Insert(new Models.Book { Id = 5, Title = "Book 5", AuthorId = 1 });
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test()]
        public void GetAllTest()
        {
            var repository = new BookRepository(_context);
            var books = repository.GetAll();

            Assert.IsNotNull(books);
            Assert.AreEqual(5, books.Count());

            bool invalid = books.Any(b => b.AuthorId != 1);
            Assert.IsFalse(invalid);
        }

        [Test()]
        public void GetTest()
        {
            var repository = new BookRepository(_context);

            // Arrange
            const long id = 4;
            const string expected = "Book 4";

            // Act
            var book = repository.Get(id);

            // Assert
            Assert.IsNotNull(book);
            Assert.AreEqual(expected, book.Title);
        }

        //[Test()]
        //public void BookRepositoryTest()
        //{
        //    throw new NotImplementedException();
        //}
    }
}