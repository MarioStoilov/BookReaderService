using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookReaderServices.Models;
using System.Web.Http.ValueProviders;
using BookReaderServices.Attributes;

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
                            this.Request.CreateResponse(HttpStatusCode.Created, allBooks.ToList()); //very important to call .ToList() or else it fails to serialize
                        return response;
                    }
                });

            return responseMsg;
        }

        // POST api/books/postbook
        [ActionName("postbook")]
        public HttpResponseMessage Post(
            [ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey,
            [FromBody]BooksAddDTO body)
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(
            () =>
            {
                var context = new BookReaderEntities();
                using (context)
                {
                    var user = context.Users.FirstOrDefault(
                                usr => usr.sessionKey == sessionKey);

                    if (user == null)
                    {
                        throw new InvalidOperationException("Users does not exist");
                    }
                    if (user.IsAuthor == true || user.IsEditor == true)
                    {
                        Book book = new Book()
                        {
                            AuthorID = body.AuthorId,
                            Description = body.Description,
                            CategoryID = body.CategoryId,
                            TItle = body.Title,
                        };

                        context.Books.Add(book);
                        context.SaveChanges();
                    }

                    var response =
                        this.Request.CreateResponse(HttpStatusCode.Created);
                    return response;
                }
            });

            return responseMsg;
        }

        // POST api/books/addbooktoshelf
        [ActionName("addbooktoshelf")]
        public HttpResponseMessage PostToShelf(
            [ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey,
            [FromBody]BookToShelfDTO body)
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(
            () =>
            {
                var context = new BookReaderEntities();
                using (context)
                {
                    var user = context.Users.FirstOrDefault(
                                usr => usr.sessionKey == sessionKey);

                    if (user == null)
                    {
                        throw new InvalidOperationException("Users does not exist");
                    }
                    
                        var shelf = context.Shelves.FirstOrDefault(
                               sh => sh.Id == body.ShelfId && sh.User == user);

                        var book = context.Books.FirstOrDefault(
                            b=>b.id == body.BookId);
                        if (book == null || shelf == null)
                        {
                            throw new ArgumentException("shelf or book not existing");
                        }


                        shelf.Books.Add(book);
                        context.SaveChanges();
                    

                    var response =
                        this.Request.CreateResponse(HttpStatusCode.Created);
                    return response;
                }
            });

            return responseMsg;
        }

        // PUT api/books/updatebook?bookId=10
        [ActionName("updatebook")]
        public HttpResponseMessage Put([ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey,
            [FromBody]BooksPutModelDTO body, int bookId)
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(
           () =>
           {
               var context = new BookReaderEntities();
               using (context)
               {
                   var user = context.Users.FirstOrDefault(
                               usr => usr.sessionKey == sessionKey);

                   if (user == null)
                   {
                       throw new InvalidOperationException("Users does not exist");
                   }
                   if (user.IsAuthor == true || user.IsEditor == true)
                   {
                       var bookForUpdate = context.Books.FirstOrDefault(b => b.id == bookId);
                       if (bookForUpdate == null)
                       {
                           throw new InvalidOperationException("Book does not exist");
                       }

                       if (body.AuthorId != null)
                       {
                           bookForUpdate.AuthorID = body.AuthorId;
                       }
                       if (body.Description != null)
                       {
                           bookForUpdate.Description = body.Description;
                       }
                       if (body.Title != null)
                       {
                           bookForUpdate.TItle = body.Title;
                       }

                       context.SaveChanges();
                   }

                   var response =
                       this.Request.CreateResponse(HttpStatusCode.OK);
                   return response;
               }
           });

            return responseMsg;
        }

        // DELETE api/books/5
        public HttpResponseMessage Delete(
            [ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey,
            [FromBody]BooksDeleteDTO body)
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(
           () =>
           {
               var context = new BookReaderEntities();
               using (context)
               {
                   var user = context.Users.FirstOrDefault(
                               usr => usr.sessionKey == sessionKey);

                   if (user == null)
                   {
                       throw new InvalidOperationException("Users does not exist");
                   }
                   if (user.IsAuthor == true)
                   {
                       var bookForDelete = context.Books.FirstOrDefault(b => b.id == body.Id);
                       if (bookForDelete == null)
                       {
                            throw new InvalidOperationException("Book does not exist");
                       }

                       context.Books.Remove(bookForDelete);
                       context.SaveChanges();
                   }

                   var response =
                       this.Request.CreateResponse(HttpStatusCode.OK);
                   return response;
               }
           });

            return responseMsg;
        }
    }
}
