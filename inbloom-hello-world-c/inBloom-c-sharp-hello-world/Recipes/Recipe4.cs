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
using InBloomClient.Entities.Associations;
using InBloomClient.Enum;

namespace inBloom_c_sharp_hello_world.Recipes
{
    public class Recipe4 : Recipe
    {
        /// <summary>
        /// Run the current recipe
        /// </summary>
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <returns>
        /// The URL where the staff association was created.
        /// </returns>
        override public string RunRecipe(string token) { return "This actual won't work without two other parameters."; }
        
        public string RunRecipe(string token, string staffId, string eduOrgId)
        {
            StaffClassificationType staffClass = StaffClassificationType.Other;
            DateTime beginDate = new DateTime(year: 2013, month: 03, day: 15);

            StaffEducationOrganizationAssociation assoc = createStaffEducationOrganizationAssociation(
                staffId: staffId,
                educationOrganizationReference: eduOrgId,
                staffClassification: staffClass,
                beginDate: beginDate
            );

            RestResponse response = postStaffEducationOrganizationAssociation(token, assoc);

            // RestSharp recommended error handling
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
                        string location = header.Value.ToString();
                        string assocId = location.Replace(response.ResponseUri.ToString(), "");
                        return assocId;
                    }
                }
                return "No location value in completed response. Does this resource already exist?";
            }
            else
            {
                return "Post was not successful: " + response.ResponseStatus.ToString();
            }
        }

        public RestResponse postStaffEducationOrganizationAssociation(string token, StaffEducationOrganizationAssociation assoc)
        {
            var client = new RestClient(WebConfigurationManager.AppSettings["inBloomRestURL"]);
            var endpoint = "staffEducationOrgAssignmentAssociations/";

            var request = inBloomRestRequest(token, endpoint, Method.POST);
            request.AddBody(assoc);
            RestResponse response = (RestResponse)client.Execute(request);

            return response;
        }

        public StaffEducationOrganizationAssociation createStaffEducationOrganizationAssociation(string staffId, string educationOrganizationReference, StaffClassificationType staffClassification, DateTime beginDate)
        {
            StaffEducationOrganizationAssociation eduOrgAssoc = new StaffEducationOrganizationAssociation();
            eduOrgAssoc.staffReference = staffId;
            eduOrgAssoc.educationOrganizationReference = educationOrganizationReference;
            eduOrgAssoc.staffClassification = staffClassification;
            eduOrgAssoc.beginDate = beginDate;

            return eduOrgAssoc;
        }
    }
}