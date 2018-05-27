using System;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using Web.ApplicationCore.Services;

namespace Web.Security
{
    public class BasicAuthHttpModule : IHttpModule
    {
        private const string Realm = "My Realm";

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += OnApplicationAuthenticateRequest;
            context.EndRequest += OnApplicationEndRequest;
        }

        private static void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }

        private static bool CheckPassword(string username, string password) =>
            AuthorizationService.IsCredentialsValid(username, password).GetAwaiter().GetResult();

        private static void AuthenticateUser(string credentials)
        {
            try
            {
                var encoding = Encoding.GetEncoding("iso-8859-1");
                credentials = encoding.GetString(Convert.FromBase64String(credentials));

                int separator = credentials.IndexOf(':');
                string name = credentials.Substring(0, separator);
                string password = credentials.Substring(separator + 1);

                if (CheckPassword(name, password))
                {
                    var identity = new GenericIdentity(name);
                    SetPrincipal(new GenericPrincipal(identity, null));
                }
                else
                {
                    // Invalid username or password.
                    HttpContext.Current.Response.StatusCode = 401;
                }
            }
            catch (FormatException)
            {
                // Credentials were not formatted correctly.
                HttpContext.Current.Response.StatusCode = 401;
            }
        }

        private static void OnApplicationAuthenticateRequest(object sender, EventArgs e)
        {
            var request = HttpContext.Current.Request;
            var authHeader = request.Headers["Authorization"];
            if (authHeader == null)
            {
                SetUnauthorized();
                return;
            }

            var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);
            
            // RFC 2617 sec 1.2, "scheme" name is case-insensitive
            if (authHeaderVal.Scheme.Equals("basic",
                    StringComparison.OrdinalIgnoreCase) &&
                authHeaderVal.Parameter != null)
            {
                AuthenticateUser(authHeaderVal.Parameter);
            }

        }

        private static void SetUnauthorized()
        {
            //HttpContext.Current.Response.StatusCode = 401;
            //HttpContext.Current.Response.Flush();
            //HttpContext.Current.Response.SuppressContent = true;
          //  HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        private static void OnApplicationEndRequest(object sender, EventArgs e)
        {
            //var response = HttpContext.Current.Response;
            //if (response.StatusCode == 401)
            //{
            //    response.Headers.Add("WWW-Authenticate",
            //        $"Basic realm=\"{Realm}\"");
            //}
        }

        public void Dispose()
        {
        }
    }
}