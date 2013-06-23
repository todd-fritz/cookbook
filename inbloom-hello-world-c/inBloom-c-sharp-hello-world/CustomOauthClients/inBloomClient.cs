using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Web;
using DotNetOpenAuth.Messaging;
using Validation;
using Newtonsoft.Json;
using DotNetOpenAuth.AspNet.Clients;
using Newtonsoft.Json.Linq;

namespace inBloom_c_sharp_hello_world.CustomOauthClients
{
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "inBloom", Justification = "Brand name")]
    public class inBloomClient : OAuth2Client
    {
        #region Constants and Fields
      
        /// <summary>
        /// The authorization endpoint.
        /// </summary>
        public const string RestEndpoint = "https://api.sandbox.inbloom.org/api/rest/v1.2/";

        /// <summary>
        /// The authorization endpoint.
        /// </summary>
        private const string AuthorizationEndpoint = "https://api.sandbox.inbloom.org/api/oauth/authorize";

        /// <summary>
        /// The token endpoint.
        /// </summary>
        private const string TokenEndpoint = "https://api.sandbox.inbloom.org/api/oauth/token";

        /// <summary>
        /// The _app id.
        /// </summary>
        private readonly string appId;

        /// <summary>
        /// The _app secret.
        /// </summary>
        private readonly string appSecret;

        #endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FacebookClient"/> class.
		/// </summary>
		/// <param name="appId">
		/// The app id.
		/// </param>
		/// <param name="appSecret">
		/// The app secret.
		/// </param>
		public inBloomClient(string appId, string appSecret)
			: base("inBloom") {
			Requires.NotNullOrEmpty(appId, "appId");
			Requires.NotNullOrEmpty(appSecret, "appSecret");

            this.appId = appId;
			this.appSecret = appSecret;
		}

		#endregion

		#region Methods

		/// <summary>
		/// The get service login url.
		/// </summary>
		/// <param name="returnUrl">
		/// The return url.
		/// </param>
		/// <returns>An absolute URI.</returns>
		protected override Uri GetServiceLoginUrl(Uri returnUrl) {
			var builder = new UriBuilder(AuthorizationEndpoint);

            // Per the recomendation of http://stackoverflow.com/questions/829080
            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

            queryString["client_id"] = this.appId;
            queryString["redirect_uri"] = returnUrl.AbsoluteUri;

            builder.Query = queryString.ToString();
    
			return builder.Uri;
		}

        /// <summary>
        /// The get user data.
        /// </summary>
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <returns>A dictionary of profile data.</returns>
        protected override IDictionary<string, string> GetUserData(string accessToken)
        {
            try
            {
                // Setup the webclient so that we can retrieve inBloom REST api endpoints
                var webClient = new WebClient();
                webClient.Headers.Add("Authorization", "bearer " + accessToken);
                webClient.Headers.Add("ContentType", "application/vnd.slc+json");
                webClient.Headers.Add("Accept", "application/vnd.slc+json");

                // Get the home enpoint, which will lead us to "self"
                string homeEndpoint = RestEndpoint + "home";
                var home_json = webClient.DownloadString(homeEndpoint);
                
                dynamic home_values = JsonConvert.DeserializeObject(home_json);
                JArray links = home_values["links"];

                string selfEndpoint = retrieveSelfURL(links);

                // Retrieve all of the data at the "self" endpoint
                var self_json = webClient.DownloadString(selfEndpoint);
                dynamic self_data = JsonConvert.DeserializeObject(self_json);

                // Pull out a subset of the data at "self" as application user data.
                IDictionary<string, string> user_dict = new Dictionary<string, string>();

                // An "id" field is required by OAuthWebSecurity.VerifyAuthentication
                // Using staffUniqueStateID as it is human readable and unique-ish
                user_dict.Add("id", self_data["staffUniqueStateId"].ToString());

                // Any other fields are just for our own use
                // Included inBloom's id as it is truely unique to a user
                user_dict.Add("inBloomId", self_data["id"].ToString());

                return user_dict;
            }
            catch (Exception ex)
            {
                // Fail loudly.
                throw ex;
            }
        }

        /// <summary>
        /// Returns the URL to the self API endpoint
        /// </summary>
        /// <param name="links">
        /// An array of links
        /// </param>
        /// <returns>
        /// A URL in string format that points to the self
        /// </returns>
        private string retrieveSelfURL(JArray links)
        {
            foreach (IDictionary<string,JToken> link in links)
            {
                if (link["rel"].ToString() == "self")
                {
                    return link["href"].ToString();
                }
            }
            // Wrong, but what to return in a fail condition?
            return null;
        }
		
        /// <summary>
		/// Obtains an access token given an authorization code and callback URL.
		/// </summary>
		/// <param name="returnUrl">
		/// The return url.
		/// </param>
		/// <param name="authorizationCode">
		/// The authorization code.
		/// </param>
		/// <returns>
		/// The access token.
		/// </returns>
		protected override string QueryAccessToken(Uri returnUrl, string authorizationCode) {
			var builder = new UriBuilder(TokenEndpoint);

            // Per the recomendation of http://stackoverflow.com/questions/829080
            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

            queryString["client_id"] = this.appId;
            queryString["client_secret"] = this.appSecret;
            queryString["redirect_uri"] = returnUrl.AbsoluteUri;
            queryString["code"] = authorizationCode;

            builder.Query = queryString.ToString();

			using (WebClient client = new WebClient()) {
                //client.Headers.Add("Authorization", authorizationCode);
                client.Headers.Add("Content-Type", "application/vnd.slc+json");
                client.Headers.Add("Accept", "application/vnd.slc+json");

				string data = client.DownloadString(builder.Uri);
				if (string.IsNullOrEmpty(data)) {
					return null;
				}

                Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);

                return values["access_token"];
			}
		}

		/// <summary>
		/// Converts any % encoded values in the URL to uppercase.
		/// </summary>
		/// <param name="url">The URL string to normalize</param>
		/// <returns>The normalized url</returns>
		/// <example>NormalizeHexEncoding("Login.aspx?ReturnUrl=%2fAccount%2fManage.aspx") returns "Login.aspx?ReturnUrl=%2FAccount%2FManage.aspx"</example>
		/// <remarks>
		/// There is an issue in Facebook whereby it will rejects the redirect_uri value if
		/// the url contains lowercase % encoded values.
		/// </remarks>
		private static string NormalizeHexEncoding(string url) {
			var chars = url.ToCharArray();
			for (int i = 0; i < chars.Length - 2; i++) {
				if (chars[i] == '%') {
					chars[i + 1] = char.ToUpperInvariant(chars[i + 1]);
					chars[i + 2] = char.ToUpperInvariant(chars[i + 2]);
					i += 2;
				}
			}
			return new string(chars);
		}

		#endregion
	}
}