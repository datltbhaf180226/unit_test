using Moq;
using NETCoreApp.Controllers;
using NETCoreApp.Models;
using NETCoreApp.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NETCoreApp.Test.Controllers
{
    [TestFixture()]
    public class BookControllerTests
    {
        private readonly List<Book> _books = new()
        {
            new Book { Id = 1, Title = "Book 1", AuthorId = 1 },
            new Book { Id = 2, Title = "Book 2", AuthorId = 2 },
            new Book { Id = 3, Title = "Book 3", AuthorId = 5 }
        };

        [Test()]
        public void GetAll_ShouldReturnAllBooks()
        {
            // Arrange
            var mockRepo = new Mock<IBookRepository>();
            mockRepo.Setup(r => r.GetAll(It.IsAny<Expression<Func<Book, object>>>())).Returns(_books);

            var controller = new BookController(mockRepo.Object);

            // Act
            var result = controller.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }

        [Test()]
        public void Get_ShouldReturnABook()
        {
            // Arrange
            const long id = 2;
            const string expected = "Book 2";

            var mockRepo = new Mock<IBookRepository>();
            mockRepo.Setup(r => r.Get(id)).Returns(_books.SingleOrDefault(o => o.Id == id));

            var controller = new BookController(mockRepo.Object);

            // Act
            var result = controller.Get(id);

            // Assert
            mockRepo.Verify(repository => repository.Get(It.IsAny<long>()), Times.Once);

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.Title);
        }

        [Test()]
        public void Get_ThrowException()
        {
            // Arrange
            const long id = 0;

            var mockRepo = new Mock<IBookRepository>();
            mockRepo.Setup(r => r.Get(id)).Returns(_books.SingleOrDefault(o => o.Id == id));

            var controller = new BookController(mockRepo.Object);

            // Act
            //var result = controller.Get(id);

            // Assert
            //Assert.That(() => controller.Get(id), Throws.TypeOf<ArgumentOutOfRangeException>());

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => controller.Get(id));
            Assert.That(ex.ParamName, Is.EqualTo("ID must be greater than zero!"));
        }
    }
}