using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Net.Http.Headers;

namespace UnitTest
{
    class User : TokenUser
    {
        public string confirmPassword { get; set; }
    }

    class TokenUser
    {
        public string userName { get; set; }
        public string password { get; set; }
    }

    [TestClass]
    public class APITest
    {

        [TestMethod]
        public void CreateUser()
        {

            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            client.BaseAddress = new Uri("http://localhost:63060/");

            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            var user = new User();

            user.userName = "jay";
            user.password = "1234Test!";
            user.confirmPassword = "1234Test!";

            var response = client.PostAsJsonAsync("api/account/register", user).Result;

            if (response.IsSuccessStatusCode)
            {

            }
            else
            {

            }
        }

        //http://bitoftech.net/2014/06/01/token-based-authentication-asp-net-web-api-2-owin-asp-net-identity/

         [TestMethod]
        public void GetToken()
        {
            var user = new TokenUser();

            user.userName = "jay";
            user.password = "1234Test!";

            string url = @"http://localhost:63060/token";
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.Accept] = "application/json";
            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            //client.Headers["grant_type"] = "authorization_code";

            string data = string.Format(
                "grant_type=password&username={0}&password={1}",
                user.userName, user.password);
            var result = client.UploadString(url, data);

        }
    }
}
