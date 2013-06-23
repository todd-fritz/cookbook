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

namespace inBloom_c_sharp_hello_world.Recipes
{
    public class Recipe10 : Recipe
    {
        override public string RunRecipe(string token)
        {
            // get a student to work with
            List<Student> students = (List<Student>)getStudents(token);
            Student student = students.First();

            OtherName alias = createOtherName(
                firstName: "Mark",
                lastSurname: "Twain",
                otherNameType: OtherNameType.Alias
                );

            if (student.otherName == null)
            {
                student.otherName = new List<OtherName>();
            }
            student.otherName.Add(alias);
            
            RestResponse response = putStudent(token, student);

            return response.StatusCode.ToString();
        }

        public Student getStudentById(string token, string id)
        {
            var client = new RestClient(WebConfigurationManager.AppSettings["inBloomRestURL"]);
            var endpoint = "students/" + id;

            var request = inBloomRestRequest(token, endpoint, Method.GET);
            RestResponse response = (RestResponse)client.Execute(request);

            Student student = JsonConvert.DeserializeObject<Student>(response.Content);

            return student;
        }

        public RestResponse putStudent(string token, Student student)
        {
            var client = new RestClient(WebConfigurationManager.AppSettings["inBloomRestURL"]);
            var endpoint = "students/" + student.id;

            var request = inBloomRestRequest(token, endpoint, Method.PUT);
            request.AddBody(student);
            RestResponse response = (RestResponse)client.Execute(request);

            return response;
        }

        public OtherName createOtherName(string firstName, string lastSurname, OtherNameType otherNameType)
        {
            OtherName otherName = new OtherName();
            otherName.firstName = firstName;
            otherName.lastSurname = lastSurname;
            otherName.otherNameType = otherNameType;

            return otherName;
        }

        public IEnumerable<Student> getStudents(string token)
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