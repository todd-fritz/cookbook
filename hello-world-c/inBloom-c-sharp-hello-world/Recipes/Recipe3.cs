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
using InBloomClient.Enum;


namespace inBloom_c_sharp_hello_world.Recipes
{
    // Create a staff member
    public class Recipe3 : Recipe
    {
        /// <summary>
        /// Run the current recipe
        /// </summary>
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <returns>
        /// Create a staff and store a staff member
        /// </returns>
        override public string RunRecipe(string token)
        {
            Name name = createName( firstName: "John", 
                                    lastSurname: "Doe"
                                  );

            Staff staff = createStaff(  staffUniqueStateId: "jDoe", 
                                        name: name,
                                        sex: SexType.Male,
                                        highestLevelOfEducationCompleted: LevelOfEducationType.NoDegree
                                     );

            
            RestResponse response = postStaff(token, staff);

            // RestSharp recommended error code
            if (response.ErrorException != null)
            {
                const string message = "Error rectrieving response. Check details in exception for more info.";
                var inBLoomException = new ApplicationException(message, response.ErrorException);
                throw inBLoomException;
            }

            // Find location in headers
            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                foreach (Parameter header in response.Headers)
                {
                    if (header.Name == "Location")
                    {
                        return header.Value.ToString();
                    }
                }
                return "No location value in completed response. Did the post succeed work?";
            }
            else
            {
                return "Post was not successful: " + response.ResponseStatus.ToString();
            }
        }

        private RestResponse postStaff(string token, Staff staff)
        {
            var client = new RestClient(WebConfigurationManager.AppSettings["inBloomRestURL"]);
            var endpoint = "staff/";

            var request = inBloomRestRequest(token, endpoint, Method.POST);
            request.AddBody(staff);
            RestResponse response = (RestResponse)client.Execute(request);

            return response;
        }

        // TODO: add optional parameter
        private Name createName(string firstName, string lastSurname)
        {
            Name newName = new Name();
            newName.firstName = firstName;
            newName.lastSurname = lastSurname;
            return newName;
        }

        // TODO: add optional parameter
        public Staff createStaff(string staffUniqueStateId, Name name, SexType sex, LevelOfEducationType highestLevelOfEducationCompleted)
        {
            Staff newStaff = new Staff();
            newStaff.staffUniqueStateId = staffUniqueStateId;
            newStaff.name = name;
            newStaff.sex = sex;
            newStaff.highestLevelOfEducationCompleted = highestLevelOfEducationCompleted;

            /*// Add null check
            foreach (KeyValuePair<string, Object> entry in optional)
            {
                // Handle optional parameter's via refelction
            }*/
            return newStaff;
        }

    } 
}