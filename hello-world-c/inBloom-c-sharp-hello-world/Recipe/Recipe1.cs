using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace inBloom_c_sharp_hello_world.Recipes
{

    // Determine user's id
    public class Recipe1 : Recipe
    {

        /// <summary>
        /// Run the current recipe
        /// </summary>
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <returns>Whatever the recipe is supposed to</returns>
        override public string RunRecipe(string token)
        {
            return getUserId(token);
        }

        /// <summary>
        /// The get user's inBloom id.
        /// </summary>
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <returns>inBloom id for current user as a string</returns>
        public string getUserId(string token)
        {
            var client = new RestClient(WebConfigurationManager.AppSettings["inBloomRestURL"]);
            string homeEndpoint = "home/";
            
            // retrieve the home endpoint
            RestRequest homeRequest = inBloomRestRequest(token, homeEndpoint, Method.GET);
            var homeResponse = client.Execute(homeRequest);
            dynamic home = JsonConvert.DeserializeObject(homeResponse.Content);

            // find the self endpoint's url
            JArray links = home["links"];
            string selfEndpoint = findLink("self", links).Replace(WebConfigurationManager.AppSettings["inBloomRestURL"], "");

            // retrieve the self endpoint
            RestRequest selfRequest = inBloomRestRequest(token, selfEndpoint, Method.GET);
            var selfResponse = client.Execute(selfRequest);
            dynamic self = JsonConvert.DeserializeObject(selfResponse.Content);

            // pull out the id field and return it
            return self["id"].ToString();
        }
    }
}