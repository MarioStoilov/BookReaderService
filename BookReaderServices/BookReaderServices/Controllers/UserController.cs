using BookReaderServices.Attributes;
using BookStore.Models;
using BookStore.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.ValueProviders;

namespace BookReaderServices.Controllers
{
    public class UserController : BaseApiController
    {
        // GET api/user
        private const int MinUsernameLength = 6;
        private const int MaxUsernameLength = 30;
        private const string ValidUsernameCharacters =
            "qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM1234567890_.";

        private const string SessionKeyChars =
            "qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDSAZXCVBNM";
        private static readonly Random rand = new Random();

        private const int SessionKeyLength = 50;

        private const int Sha1Length = 40;

        // {  
        //"username": "DonchoMinkov",
        //"authCode":   "bfff2dd4f1b310eb0dbf593bd83f94dd8d34077e" 
        //}

        [ActionName("register")]
        public HttpResponseMessage PostRegisterUser(UserModel model)
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(
                () =>
                {
                    var context = new BookReaderEntities();
                    using (context)
                    {
                        this.ValidateUsername(model.username);
                        this.ValidateAuthCode(model.authCode);
                        var usernameToLower = model.username.ToLower();
                        var user = context.Users.FirstOrDefault(
                            usr => usr.Username == usernameToLower);

                        if (user != null)
                        {
                            throw new InvalidOperationException("User exists");
                        }

                        user = new User()
                        {
                            Username = usernameToLower,
                            AuthCode = model.authCode
                        };

                        context.Users.Add(user);
                        context.SaveChanges();

                        user.sessionKey = this.GenerateSessionKey(user.Id);
                        context.SaveChanges();
                        string sessionKey = user.sessionKey;

                        var response =
                            this.Request.CreateResponse(HttpStatusCode.Created,
                                            sessionKey);
                        return response;
                    }
                });

            return responseMsg;
        }

        [ActionName("login")]
        public HttpResponseMessage PostLoginUser(UserModel model)
        {
            var responseMsg = this.PerformOperationAndHandleExceptions(
              () =>
              {
                  var context = new BookReaderEntities();
                  using (context)
                  {
                      this.ValidateUsername(model.username);
                      this.ValidateAuthCode(model.authCode);
                      var usernameToLower = model.username.ToLower();
                      var user = context.Users.FirstOrDefault(
                          usr => usr.Username == usernameToLower
                          && usr.AuthCode == model.authCode);

                      if (user == null)
                      {
                          throw new InvalidOperationException("Invalid username or password");
                      }
                      if (user.sessionKey == null)
                      {
                          user.sessionKey = this.GenerateSessionKey(user.Id);
                          context.SaveChanges();
                      }
                          
                      string sessionKey = user.sessionKey;

                      var response =
                          this.Request.CreateResponse(HttpStatusCode.OK,
                                          sessionKey);
                      return response;
                  }
              });

            return responseMsg;
        }

        [ActionName("logout")]
        public HttpResponseMessage PutLogoutUser(
            [ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey)
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

                        user.sessionKey = null;
                        context.SaveChanges();


                        var response =
                            this.Request.CreateResponse(HttpStatusCode.OK);
                        return response;
                    }
                });

            return responseMsg;
        }

        private string GenerateSessionKey(int userId)
        {
            StringBuilder skeyBuilder = new StringBuilder(SessionKeyLength);
            skeyBuilder.Append(userId);
            while (skeyBuilder.Length < SessionKeyLength)
            {
                var index = rand.Next(SessionKeyChars.Length);
                skeyBuilder.Append(SessionKeyChars[index]);
            }
            return skeyBuilder.ToString();
        }

        private void ValidateAuthCode(string authCode)
        {
            if (authCode == null || authCode.Length != Sha1Length)
            {
                throw new ArgumentOutOfRangeException("Password should be encrypted");
            }
        }

        private void ValidateUsername(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException("Username cannot be null");
            }
            else if (username.Length < MinUsernameLength)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("Username must be at least {0} characters long",
                    MinUsernameLength));
            }
            else if (username.Length > MaxUsernameLength)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("Username must be less than {0} characters long",
                    MaxUsernameLength));
            }
            else if (username.Any(ch => !ValidUsernameCharacters.Contains(ch)))
            {
                throw new ArgumentOutOfRangeException(
                    "Username must contain only Latin letters, digits .,_");
            }

        }
    }
}
