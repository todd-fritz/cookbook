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
using Newtonsoft.Json;
using inBloom_c_sharp_hello_world.Recipes;
using System.Web.Configuration;

namespace inBloom_c_sharp_hello_world.Controllers
{
    class Recipe8 : Recipe
    {
        override public string RunRecipe(string token) 
        {
            // get a student
            List<Student> students = (List<Student>)getStudents(token);
            Student student = students.First();

            // get a section
            List<Section> sections = (List<Section>)getSections(token);
            Section section = sections.First();

            // create a date
            DateTime begindate = DateTime.Now; 

            // create a Student section assocation
            StudentSectionAssociation assoc = createStudentSectionAssociation(
                studentId: student.id,
                sectionId: section.id,
                beginDate: begindate
            );

            // Post the assocation to the database
            RestResponse response = postStudentSectionAssociation(token, assoc);
                        
            
            // RestSharp recommended error handling
            if (response.ErrorException != null)
            {
                const string message = "Error rectrieving response. Check details in exception for more info.";
                var inBLoomException = new ApplicationException(message, response.ErrorException);
                throw inBLoomException;
            }

            // return the location of the new assocation
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

        private RestResponse postStudentSectionAssociation(string token, StudentSectionAssociation assoc)
        {
            var client = new RestClient(WebConfigurationManager.AppSettings["inBloomRestURL"]);
            var endpoint = "studentSectionAssociations/";

            var request = inBloomRestRequest(token, endpoint, Method.POST);
            request.AddBody(assoc);
            RestResponse response = (RestResponse)client.Execute(request);

            return response;
        }

        private StudentSectionAssociation createStudentSectionAssociation(string studentId, string sectionId, DateTime beginDate)
        {
            StudentSectionAssociation assoc = new StudentSectionAssociation();

            assoc.studentId = studentId;
            assoc.sectionId = sectionId;
            assoc.beginDate = beginDate;

            return assoc;
        }

        private List<Section> getSections(string token)
        {
            var client = new RestClient(WebConfigurationManager.AppSettings["inBloomRestURL"]);
            string endpoint = "sections/";

            RestRequest request = inBloomRestRequest(token, endpoint, Method.GET);
            var response = client.Execute(request);
            List<Section> sections = JsonConvert.DeserializeObject<List<Section>>(response.Content);

            return sections;
        }

        private IEnumerable<Student> getStudents(string token)
        {
            var client = new RestClient(WebConfigurationManager.AppSettings["inBloomRestURL"]);
            string endpoint = "students/";

            RestRequest request = inBloomRestRequest(token, endpoint, Method.GET);
            var response = client.Execute(request);
            List<Student> students = JsonConvert.DeserializeObject<List<Student>>(response.Content);

            return students;
        }
    }
}
