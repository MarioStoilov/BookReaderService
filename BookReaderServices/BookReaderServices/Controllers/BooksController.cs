using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookReaderServices.Models;

namespace BookReaderServices.Controllers
{
    public class BooksController : BaseApiController
    {
        // GET api/books/getallbooks
        [ActionName("getallbooks")]
        public HttpResponseMessage Get()
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(
                () =>
                {
                    var context = new BookReaderEntities();
                    using (context)
                    {
                        var allBooks =
                            from book in context.Books
                            select new BooksDTO()
                            {
                                Id = book.id,
                                Rating = book.Rating,
                                Title = book.TItle,
                                Description = book.Description,
                                CategoryDetails = book.Category.Title,
                                AuthorInfo = new AuthorByBookDTO()
                                {
                                    FirstName = book.Author.Name,
                                    LastName = book.Author.Surname,
                                }
                            };
                        var response =
                            this.Request.CreateResponse(HttpStatusCode.Created, allBooks);
                        return response;
                    }
                });

            return responseMsg;
        }

        //// GET api/books/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/books
        public void Post([FromBody]string value)
        {
        }

        // PUT api/books/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/books/5
        public void Delete(int id)
        {
        }
    }
}
