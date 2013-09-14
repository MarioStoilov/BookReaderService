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
    public class CommentsController : BaseApiController
    {
        // GET api/comments/5
        [ActionName("getbookcomments")]
        public HttpResponseMessage Get(
            [ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey,
            int bookId)
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

                         var allComments =
                             from comment in context.Comments
                             where comment.BookID == bookId
                             select new CommentsByBookDTO()
                             {
                                 Id = comment.Id,
                                 Info = comment.Body,
                                 Username = comment.User.Username,
                             };

                         var response =
                             this.Request.CreateResponse(HttpStatusCode.OK);
                         return response;
                     }
                 });

            return responseMsg;
        }

        //// POST api/comments
        //public HttpResponseMessage Post([ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey,
        //    [FromBody]CommentsAddDTO value)
        //{
        //    var responseMsg = this.PerformOperationAndHandleExceptions(
        //         () =>
        //         {
        //             var context = new BookReaderEntities();
        //             using (context)
        //             {
        //                 var user = context.Users.FirstOrDefault(
        //                       usr => usr.sessionKey == sessionKey);

        //                 if (user == null)
        //                 {
        //                     throw new InvalidOperationException("Users does not exist");
        //                 }


        //                 Comment comment = new Comment()
        //                 {
        //                     Body = value.Info,
        //                     BookID = value.BookId,
        //                     Title = value.Title,
        //                     User = user,
        //                 };

        //                 context.Comments.Add(comment);
        //                 context.SaveChanges();

        //                 var response =
        //                     this.Request.CreateResponse(HttpStatusCode.Created);
        //                 return response;
        //             }
        //         });

        //    return responseMsg;
        
        //}
    }
}
