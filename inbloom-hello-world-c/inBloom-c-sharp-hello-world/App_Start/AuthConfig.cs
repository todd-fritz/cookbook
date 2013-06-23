using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using inBloom_c_sharp_hello_world.Models;
using inBloom_c_sharp_hello_world.CustomOauthClients;
using System.Web.Configuration;

namespace inBloom_c_sharp_hello_world
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "",
            //    appSecret: "");

            //OAuthWebSecurity.RegisterGoogleClient();

             var _inBloomClient = new inBloomClient(
                appId: WebConfigurationManager.AppSettings["InBloomClientId"],
                appSecret: WebConfigurationManager.AppSettings["InBloomSharedSecret"]);
                
             OAuthWebSecurity.RegisterClient(_inBloomClient, "inBloom", null);
        }
    }
}
