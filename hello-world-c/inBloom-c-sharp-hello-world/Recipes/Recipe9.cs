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
using System.Collections.Specialized;

namespace inBloom_c_sharp_hello_world.Recipes
{
    public class Recipe9 :Recipe
    {
        public override string RunRecipe(string token)
        {
            Dictionary<string, string> queries = new Dictionary<string,string>();
 
            queries.Add("name.firstName", "Mi-Ha");

            List<Student> students = (List<Student>)getStudents(token, queries);

            string ret = "";

            foreach (Student student in students)
            {
                ret += student.name.firstName + "\t" + student.id + "\n";
            }
            return ret;
        }

        private IEnumerable<Student> getStudents(string token, Dictionary<string, string> parameters = null)
        {
            string queryString = "?";
            if (parameters != null)
            {
                queryString += createQueryString(parameters);
            }

            var client = new RestClient(WebConfigurationManager.AppSettings["inBloomRestURL"]);
            string endpoint = "students" + queryString;

            RestRequest request = inBloomRestRequest(token, endpoint, Method.GET);
            var response = client.Execute(request);
            List<Student> students = JsonConvert.DeserializeObject<List<Student>>(response.Content);

            return students;
        }

        private string createQueryString(Dictionary<string,string> parameters)
        {
            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

            foreach (var keyValuePair in parameters)
            {
                queryString.Add(keyValuePair.Key.ToString(), keyValuePair.Value.ToString());
            }
            
            return queryString.ToString();
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