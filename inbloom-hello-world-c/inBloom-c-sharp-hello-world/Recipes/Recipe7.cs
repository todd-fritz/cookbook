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
    public class Recipe7 : Recipes.Recipe
    {
        override public string RunRecipe(string token) { return ""; }

        public string RunRecipe(string token, string id)
        {
            return deleteAssessmentById(token, id);
        }

        private string deleteAssessmentById(string token, string id)
        {
            var client = new RestClient("https://api.sandbox.inbloom.org/api/rest/v1.2/");

            var endpoint = "assessments/" + id;

            var request = inBloomRestRequest(token, endpoint, Method.DELETE);

            RestResponse response = (RestResponse)client.Execute(request);

            // RestSharp recommended error code
            if (response.ErrorException != null)
            {
                const string message = "Error rectrieving response. Check details in exception for more info.";
                var inBLoomException = new ApplicationException(message, response.ErrorException);
                throw inBLoomException;
            }

            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                return response.ResponseStatus.ToString();
            }
            else
            {
                return "Delete was not successful: " + response.ResponseStatus.ToString();
            }
        }
    }
}
