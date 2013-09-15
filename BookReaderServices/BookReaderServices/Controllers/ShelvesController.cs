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

        // GET api/shelves/getallshelves
        [ActionName("getallshelves")]
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
