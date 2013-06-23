using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBloomClient.Entities;
using InBloomClient.Services;
using InBloomClient.Enum;
using InBloomClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using RestSharp;
using inBloom_c_sharp_hello_world.Recipes;

namespace inBloom_c_sharp_hello_world.Controllers
{
    class Recipe8 : Recipe
    {
        override public string RunRecipe(string token) 
        {
            List<Student> students = getStudents(token);
            return ""; 
        }

        private IEnumerable<Student> getStudents(string token)
        {
            throw new NotImplementedException();
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
