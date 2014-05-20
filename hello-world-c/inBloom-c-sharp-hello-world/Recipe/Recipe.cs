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
    abstract public class Recipe
    {
        abstract public string RunRecipe(string token);
        
        public RestRequest inBloomRestRequest(string token, string endpoint, Method method)
        {
            RestRequest request = new RestRequest(endpoint, method);
            // These fields will be the same for every inBloom request
            request.JsonSerializer = new inBloomSerializer();
            request.AddHeader("Authorization", "bearer " + token);
            request.AddHeader("Accept", "application/json");
            request.RequestFormat = DataFormat.Json;

            return request;
        }

        protected string findLink(string target, JArray links)
        {
            foreach (IDictionary<string, JToken> link in links)
            {
                if (link["rel"].ToString() == target)
                {
                    return link["href"].ToString();
                }
            }
            // Wrong, but what to return in a fail condition?
            return null;
        }
    }
}
