using BookReaderServices.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ValueProviders;
using BookReaderServices.Models;
using BookStore.Models;

namespace BookReaderServices.Controllers
{
    public class ShelvesController : BaseApiController
    {
        // GET api/shelves/getshelfById?id=5
        [ActionName("getshelfById")]
        public HttpResponseMessage Get([ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey, int id)
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

                        var foundShelf =
                            (from shelf in context.Shelves
                            where shelf.Id == id
                            select new ShelvesGetAllDTO()
                            {
                                Title = shelf.Title,
                                Id = shelf.Id,
                                UserId = shelf.UserId,
                            }).FirstOrDefault();
                        var response =
                            this.Request.CreateResponse(HttpStatusCode.OK, foundShelf);
                        return response;
                    }
                });

            return responseMsg;
        }

        // GET api/shelves/5/books
        [ActionName("books")]
        public HttpResponseMessage GetBooks([ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey, int id)
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

                        var foundBooks =
                            (from shelf in context.Shelves
                             where shelf.Id == id
                             select shelf.Books).ToList();

                        ICollection<BooksDTO> allBooks = new List<BooksDTO>();

                        foreach (Book book in foundBooks)
                        {
                            allBooks.Add(new BooksDTO()
                            {
                                Title = book.TItle,
                                AuthorInfo = new AuthorByBookDTO()
                                {
                                    FirstName = book.Author.Name,
                                    LastName = book.Author.Surname,
                                    Id = book.Author.Id
                                },
                                Description = book.Description,
                                Id = book.id,
                                Rating = book.Rating,
                                CategoryDetails = book.Category.Title,
                            });
                        }

                        var response =
                            this.Request.CreateResponse(HttpStatusCode.OK, allBooks.ToList());
                        return response;
                    }
                });

            return responseMsg;
        }

        // PUT api/shelves/5/update
        [ActionName("update")]
        public HttpResponseMessage PutUpdate([ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey, int id, [FromBody]ShelvesDTO shelf)
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

                        var shelfToUpdate = context.Shelves.FirstOrDefault(s => s.Id == id);

                        if (shelfToUpdate == null)
                        {
                            throw new InvalidOperationException("Shelf does not exist");
                        }

                        if (shelfToUpdate.UserId!=user.Id)
                        {
                            throw new InvalidOperationException("This shelf does not belong to you");
                        }

                        shelfToUpdate.Title = shelf.Title;
                        context.SaveChanges();


                        var response =
                            this.Request.CreateResponse(HttpStatusCode.OK);
                        return response;
                    }
                });

            return responseMsg;
        }

        // PUT api/shelves/5/remove
        [ActionName("remove")]
        public HttpResponseMessage DeleteShelf([ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey, int id)
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

                        var shelfToRemove = context.Shelves.FirstOrDefault(s => s.Id == id);

                        if (shelfToRemove == null)
                        {
                            throw new InvalidOperationException("Shelf does not exist");
                        }

                        if (shelfToRemove.UserId != user.Id)
                        {
                            throw new InvalidOperationException("This shelf does not belong to you");
                        }

                        shelfToRemove.Books.Clear();

                        context.Shelves.Remove(shelfToRemove);
                        context.SaveChanges();


                        var response =
                            this.Request.CreateResponse(HttpStatusCode.OK);
                        return response;
                    }
                });

            return responseMsg;
        }

        // GET api/shelves/getallshelves
        [ActionName("getallshelves")]
        public HttpResponseMessage GetAll([ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey)
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

                        var allShelves =
                            from shelf in context.Shelves
                            select new ShelvesGetAllDTO()
                            {
                                Title = shelf.Title,
                                Id = shelf.Id,
                                UserId = shelf.UserId,
                            };
                        var response =
                            this.Request.CreateResponse(HttpStatusCode.OK, allShelves.ToList());
                        return response;
                    }
                });

            return responseMsg;
        }

        [ActionName("getusershelves")]
        public HttpResponseMessage Get([ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey)
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

                        var allShelves =
                            from shelf in context.Shelves
                            where shelf.UserId==user.Id
                            select new ShelvesGetAllDTO()
                            {
                                Title = shelf.Title,
                                Id = shelf.Id,
                                UserId = shelf.UserId,
                            };
                        var response =
                            this.Request.CreateResponse(HttpStatusCode.OK, allShelves.ToList());
                        return response;
                    }
                });

            return responseMsg;
        }

        // POST api/shelves/createShelf
        [ActionName("createShelf")]
        public HttpResponseMessage Post(
            [ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey,
            [FromBody]ShelvesDTO value)
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

                        Shelf shelf = new Shelf()
                        {
                            Title = value.Title,
                            User = user,
                        };

                        

                        context.Shelves.Add(shelf);
                        context.SaveChanges();

                        ShelvesGetAllDTO current = new ShelvesGetAllDTO()
                        {
                            Id = shelf.Id,
                            Title = shelf.Title,
                            UserId = shelf.UserId
                        };

                        var response =
                            this.Request.CreateResponse(HttpStatusCode.Created, current);
                        return response;
                    }
                });

            return responseMsg;
        }
    }
}
