using Microsoft.AspNetCore.Mvc;
using NETCoreApp.Models;
using NETCoreApp.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NETCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _repository;

        public BookController(IBookRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<BookController>
        [HttpGet]
        public IEnumerable<BookModel> GetAll()
        {
            var books = _repository.GetAll(o => o.Author);
            return books.Select(book => new BookModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author != null ? new AuthorModel
                {
                    FirstName = book.Author.FirstName,
                    LastName = book.Author.LastName
                } : null
            }).AsEnumerable();
        }

        // GET: api/<BookController>
        [HttpGet("list")]
        public IEnumerable<BookModel> GetAllByAuthorId(long id)
        {
            var books = _repository.Filter(book => book.AuthorId == id && book.Title.Contains(".NET"), book => book.Author);
            return books.Select(book => new BookModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = new AuthorModel
                {
                    FirstName = book.Author.FirstName,
                    LastName = book.Author.LastName
                }
            }).AsEnumerable();
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public Book Get(long id)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException("ID must be greater than zero!");

            return _repository.Get(id);
        }

        // POST api/<BookController>
        [HttpPost]
        public Book Post(BookCreateModel model)
        {
            if (!ModelState.IsValid) return null;

            var entity = new Book
            {
                Title = model.Title,
                AuthorId = model.AuthorId,
                CreatedDate = DateTime.Now
            };

            _repository.Insert(entity);
            return entity;
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public Book Update(long id, BookCreateModel model)
        {
            if (!ModelState.IsValid) return null;

            var entity = _repository.Get(id);
            entity.Title = model.Title;
            entity.AuthorId = model.AuthorId;

            _repository.Update(entity);
            return entity;
        }

        [HttpPut]
        public Book Update2(BookUpdateModel model)
        {
            if (!ModelState.IsValid) return null;

            var entity = _repository.Get(model.Id);
            entity.Title = model.Title;
            entity.AuthorId = model.AuthorId;

            _repository.Update(entity);
            return entity;
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
