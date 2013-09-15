using BookReaderServices.Attributes;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ValueProviders;
using BookReaderServices.Models;

namespace BookReaderServices.Controllers
{
    public class AuthorsController : BaseApiController
    {
        // POST api/authors/createAuthor
        [ActionName("createAuthor")]
        public HttpResponseMessage Post(
            [ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey,
            [FromBody]AuthorsAddDTO value)
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

                        Author author = new Author()
                        {
                            Name = value.Name,
                            Nickname = value.Nickname,
                            Surname = value.Surname
                        };

                        context.Authors.Add(author);
                        context.SaveChanges();

                        var response =
                            this.Request.CreateResponse(HttpStatusCode.OK);
                        return response;
                    }
                });

            return responseMsg;
        }
    }
}
