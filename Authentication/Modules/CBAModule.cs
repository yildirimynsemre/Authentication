using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;

namespace Authentication.Modules
{
    public class CBAModule : IHttpModule
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += Context_AuthenticateRequest;
            context.EndRequest += Context_EndRequest;
            //throw new NotImplementedException();
        }

        private void Context_EndRequest(object sender, EventArgs e)
        {
            var responce = HttpContext.Current.Response;
            if (responce.StatusCode==401)
            {
                responce.Headers.Add("WWW-Authenticate", "Basic realm=\"insert for realm\"");
            }
            //throw new NotImplementedException();
        }

        private void Context_AuthenticateRequest(object sender, EventArgs e)
        {
            var request = HttpContext.Current.Request;
            var header = request.Headers["Authorization"];
            if (header != null)
            {
                var parsedValue = AuthenticationHeaderValue.Parse(header);
                if (parsedValue.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase)&&parsedValue.Parameter!=null)
                {
                    Authenticate(parsedValue.Parameter);

                }
            }
            //throw new NotImplementedException();
        }

        private bool Authenticate(string credentialValues)
        {
            bool isValid = false;

            try
            {
                var credentials = Encoding.GetEncoding("iso-8859-1").GetString(Convert.FromBase64String(credentialValues));
                var values = credentials.Split(':');
                isValid = CheckUser(userName: values[0], password: values[1]);
                if (isValid)
                {
                    SetPrincipal(new GenericPrincipal(new GenericIdentity(values[0]), null));
                }

            }
            catch
            {
                 
            }
            return isValid;
            //throw new NotImplementedException();
        }

        private bool CheckUser(string userName, string password)
        {
            return (userName == "test" && password == "1234");
            //throw new NotImplementedException();
        }

        private static void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
                HttpContext.Current.User = principal;
        }
    }
}