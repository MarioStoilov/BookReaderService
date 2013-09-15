using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace BookReaderServices
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            
            config.Routes.MapHttpRoute(
                name: "ShelvesApi",
                routeTemplate: "api/shelves/{action}",
                defaults: new
                {
                    controller = "shelves",
                   
                }
            );
            config.Routes.MapHttpRoute(
                name: "ShelvesBookApi",
                routeTemplate: "api/shelves/{id}/{action}",
                defaults: new
                {
                    controller = "shelves",
                    id = UrlParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
                name: "AuthorsApi",
                routeTemplate: "api/authors/{action}",
                defaults: new
                {
                    controller = "authors"
                }
            );
            config.Routes.MapHttpRoute(
                name: "CommentsApi",
                routeTemplate: "api/comments/{action}",
                defaults: new
                {
                    controller = "comments"
                }
            );

            config.Routes.MapHttpRoute(
                name: "BooksApi",
                routeTemplate: "api/books/{action}",
                defaults: new
                {
                    controller = "books"
                }
            );

            config.Routes.MapHttpRoute(
                name: "UsersApi",
                routeTemplate: "api/user/{action}",
                defaults: new
                {
                    controller = "user"
                }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
        }
    }
}
