using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InBloomClient.Entities;
using InBloomClient.Enum;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using RestSharp;

namespace inBloom_c_sharp_hello_world.Recipes
{
    public class Recipe5 : Recipe
    {

        override public string RunRecipe(string token)
        {
            // Create the assessment it's required objects
            AssessmentIdentificationCode idCode = createAssesmentIdentifcationCode(
                identificationSystem: AssessmentIdentificationSystemType.Other,
                ID: "Unique Alphanumberic Identifier"
                );

            List<AssessmentIdentificationCode> idCodes = new List<AssessmentIdentificationCode>();
            idCodes.Add(idCode);

            Assessment assessment = createAssessment(
                assessmentTitle: "Title", 
                assessmentIdentificationCode: idCodes,
                version: 1);

            // return location or error
            return postAssessment(token, assessment);
        }

        private string postAssessment(string token, Assessment assessment)
        {
            var client = new RestClient("https://api.sandbox.inbloom.org/api/rest/v1.2/");
            var endpoint = "assessments/";

            var request = inBloomRestRequest(token, endpoint, Method.POST);
            request.AddBody(assessment);

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
                foreach (Parameter header in response.Headers)
                {
                    if (header.Name == "Location")
                    {
                        // Return the id value of the new assessment
                        string location = header.Value.ToString();
                        string assessmentId = location.Replace(response.ResponseUri.ToString(), "");
                        return assessmentId;
                    }
                }
                // Success but no location -> fail loudly
                const string message = "No location value in completed response. Did the post actually work?";
                var inBLoomException = new ApplicationException(message, response.ErrorException);
                throw inBLoomException;
            }
            else
            {
                return "Post was not successful: " + response.ResponseStatus.ToString();
            }
        }

        private AssessmentIdentificationCode createAssesmentIdentifcationCode(AssessmentIdentificationSystemType identificationSystem, string ID)
        {
            AssessmentIdentificationCode idCode = new AssessmentIdentificationCode();
            idCode.identificationSystem = identificationSystem;
            idCode.ID = ID;

            return idCode;
        }

        private Assessment createAssessment(string assessmentTitle, List<AssessmentIdentificationCode> assessmentIdentificationCode, int version)
        {
            Assessment assessment = new Assessment();
            assessment.assessmentTitle = assessmentTitle;
            assessment.assessmentIdentificationCode = assessmentIdentificationCode;
            //assessment.version = version;

            return assessment;
        }
    }
}