using BookReaderServices.Attributes;
using BookReaderServices.Models;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ValueProviders;

namespace BookReaderServices.Controllers
{
    public class CategoriesController : BaseApiController
    {
        // GET api/categories/getallcategories
        [ActionName("getallcategories")]
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

                        var allCategories =
                            from cat in context.Categories
                            select new CategoriesDTO()
                            {
                                Title = cat.Title,
                            };
                        var response =
                            this.Request.CreateResponse(HttpStatusCode.OK, allCategories.ToList()); //very important to call .ToList() or else it fails to serialize
                        return response;
                    }
                });

            return responseMsg;
        }
    }
}
