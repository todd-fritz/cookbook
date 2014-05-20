using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using InBloomClient.Entities;

namespace inBloom_c_sharp_hello_world.Recipes
{

    // Determine user's educational organization ids
    public class Recipe2 : Recipe
    {
        /// <summary>
        /// Run the current recipe
        /// </summary>
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <returns>
        /// A concatenated string of all of the IDs of the Education Organizations the user is associated with
        /// </returns>
        override public string RunRecipe(string token)
        {
            string ret = "";
            
            IEnumerable<EducationOrganization> eduOrgs = getEducationalOrgs(token);

            // pull out the id fields and return it
            foreach (EducationOrganization eduOrg in eduOrgs)
            {
                ret += eduOrg.id + "\t";
            }
            return ret;
        }

        public IEnumerable<EducationOrganization> getEducationalOrgs(string token)
        {
            var client = new RestClient(WebConfigurationManager.AppSettings["inBloomRestURL"]);
            string homeEndpoint = "home/";

            // retrieve the home endpoint
            RestRequest homeRequest = inBloomRestRequest(token, homeEndpoint, Method.GET);
            var homeResponse = client.Execute(homeRequest);
            dynamic home = JsonConvert.DeserializeObject(homeResponse.Content);

            // find the self endpoint's url
            JArray links = home["links"];
            string eduOrgsEndpoint = findLink("getEducationOrganizations", links).Replace(WebConfigurationManager.AppSettings["inBloomRestURL"], "");

            // retrieve the self endpoint
            RestRequest eduOrgsRequest = inBloomRestRequest(token, eduOrgsEndpoint, Method.GET);
            var eduOrgsResponse = client.Execute(eduOrgsRequest);
            return JsonConvert.DeserializeObject<List<EducationOrganization>>(eduOrgsResponse.Content);
        }       
    }
}