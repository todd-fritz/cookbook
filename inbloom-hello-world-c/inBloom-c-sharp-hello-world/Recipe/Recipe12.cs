using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBloomClient.Entities;
using InBloomClient.Services;
using InBloomClient.Enum;
using InBloomClient.Entities.Associations;
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
    public class Recipe12 : Recipe
    {
        public override string RunRecipe(string token)
        {
            // Get section to work with
            List<Section> sections = getSections(token);
            Section section = sections.First();
            string sectionId = section.id;

            List<StudentParentAssociation> assocs = getStudentParentAssociationsBySection(token, sectionId);
            Dictionary<string, string> contactRestrictions = findContactRestrictions(assocs);

            List<List<object>> stuff = new List<List<object>>();
            foreach(StudentParentAssociation assoc in assocs)
            {
                List<object> temp = new List<object>();
                temp.Add(assoc.parentId);
                temp.Add(getParentById(token, assoc.parentId));
                foreach(KeyValuePair<string, string> kvp in contactRestrictions) 
                {
                    if (kvp.Key == assoc.parentId) {
                        temp.Add(kvp.Value);
                    }
                }
                stuff.Add(temp);
            }

            return stuff.ToString();
        }

        private Parent getParentById(string token, string id)
        {
            var client = new RestClient(WebConfigurationManager.AppSettings["inBloomRestURL"]);
            string endpoint = "parents/" + id;

            RestRequest request = inBloomRestRequest(token, endpoint, Method.GET);
            var response = client.Execute(request);
            Parent parent = JsonConvert.DeserializeObject<Parent>(response.Content);

            return parent;
        }

        private Dictionary<string, string> findContactRestrictions(List<StudentParentAssociation> assocs)
        {
            // Find the contact restrictions
            Dictionary<string, string> contactRestrictions = new Dictionary<string, string>();
            foreach (StudentParentAssociation assoc in assocs)
            {
                contactRestrictions.Add(assoc.parentId, assoc.contactRestrictions);
            }
            return contactRestrictions;
        }

        private List<StudentParentAssociation> getStudentParentAssociationsBySection(string token, string sectionId)
        {
            var client = new RestClient(WebConfigurationManager.AppSettings["inBloomRestURL"]);
            string endpoint = "sections/" + sectionId + 
                "/studentSectionAssociations/students/studentParentAssociations";

            RestRequest request = inBloomRestRequest(token, endpoint, Method.GET);
            var response = client.Execute(request);
            List<StudentParentAssociation> assocs = JsonConvert.DeserializeObject<List<StudentParentAssociation>>(response.Content);

            return assocs;
        }

        private List<Section> getSections(string token)
        {
            var client = new RestClient(WebConfigurationManager.AppSettings["inBloomRestURL"]);
            string endpoint = "sections";

            RestRequest request = inBloomRestRequest(token, endpoint, Method.GET);
            var response = client.Execute(request);
            List<Section> sections = JsonConvert.DeserializeObject<List<Section>>(response.Content);

            return sections;
        }
    }
}