using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace WebApplication1.Infrastructure
{
    public class HttpAuthenticate : FilterAttribute, IAuthenticationFilter
    {
        private string _username { get; set; }
        private string _password { get; set; }
        public HttpAuthenticate(string username, string password)
        {
            _username = username;
            _password = password;
        }

        
        //public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        //{
        //    // filterContext.Result = new HttpUnauthorizedResult();

        //}

        //public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        //{
        //    // use this to customize the result that's sent back.
        //}

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            throw new NotImplementedException();
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            throw new NotImplementedException();
        }
    }
}