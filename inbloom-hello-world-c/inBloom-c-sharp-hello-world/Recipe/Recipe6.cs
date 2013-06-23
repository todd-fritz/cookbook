using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Newtonsoft.Json;
using RestSharp;
using InBloomClient.Entities;



namespace inBloom_c_sharp_hello_world.Recipes
{
    public class Recipe6 : Recipe
    {
        override public string RunRecipe(string token) { return ""; }

        public string RunRecipe(string token, string id)
        {
            Assessment assessment = getAssessmentById(token, id);
              
            assessment.minRawScore = 42;
         
            RestResponse putResponse = putAssessement(token, id, assessment);

            // RestSharp recommended error code
            if (putResponse.ErrorException != null)
            {
                const string message = "Error rectrieving response. Check details in exception for more info.";
                var inBLoomException = new ApplicationException(message, putResponse.ErrorException);
                throw inBLoomException;
            }
            return assessment.id;
        }

        private Assessment getAssessmentById(string token, string id)
        {
            var client = new RestClient(WebConfigurationManager.AppSettings["inBloomRestURL"]);
            var endpoint = "assessments/" + id;

            var request = inBloomRestRequest(token, endpoint, Method.GET);
            
            RestResponse response = (RestResponse)client.Execute(request);
            Assessment assessment = JsonConvert.DeserializeObject<Assessment>(response.Content);
            return assessment;
        }

        private RestResponse putAssessement(string token, string id, Assessment assessment)
        {
            var client = new RestClient(WebConfigurationManager.AppSettings["inBloomRestURL"]);
            var endpoint = "assessments/" + id;

            var request = inBloomRestRequest(token, endpoint, Method.PUT);
            request.AddBody(assessment);

            RestResponse response = (RestResponse)client.Execute(request);

            return response;
         }
    }
}