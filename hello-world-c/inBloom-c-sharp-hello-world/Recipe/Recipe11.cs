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
    public class Recipe11 : Recipe
    {
        public override string RunRecipe(string token)
        {
            List<StudentSectionAssociation> assocs = getStudentSectionAssociation(token);
            StudentSectionAssociation assoc = assocs.First();

            string studentSectionAssociationId = "264c2a4b468a418b3fe6005263192e2957b9497e_id8d843440104f871e2717f38759c10c89c3742092_id";
            string sectionId = "264c2a4b468a418b3fe6005263192e2957b9497e_id";
            string studentId = "ea99ef20bbb347149565f4eb8318f7f9d16265c4_id";

            /*string studentSectionAssociationId = assoc.id;
            string sectionId = assoc.sectionId;
            string studentId = assoc.studentId;*/

            Grade grade = createGrade(
                gradeType: GradeType.Exam,
                schoolYear: SchoolYearType.Y20122013,
                sectionId: sectionId,
                studentId: studentId,
                studentSectionAssocation: studentSectionAssociationId);

            RestResponse response = postGrade(token, grade);
            return response.StatusCode.ToString();
        }

        private RestResponse postGrade(string token, Grade grade)
        {
            var client = new RestClient(WebConfigurationManager.AppSettings["inBloomRestURL"]);
            var endpoint = "grades";

            var request = inBloomRestRequest(token, endpoint, Method.POST);
            request.AddBody(grade);
            RestResponse response = (RestResponse)client.Execute(request);

            return response;
        }

        private List<StudentSectionAssociation> getStudentSectionAssociation(string token)
        {
            var client = new RestClient(WebConfigurationManager.AppSettings["inBloomRestURL"]);
            string endpoint = "studentSectionAssociations";

            RestRequest request = inBloomRestRequest(token, endpoint, Method.GET);
            var response = client.Execute(request);
            List<StudentSectionAssociation> assoc = JsonConvert.DeserializeObject<List<StudentSectionAssociation>>(response.Content);

            return assoc;
        }

        private Grade createGrade(GradeType gradeType, SchoolYearType schoolYear, string sectionId, string studentId, string studentSectionAssocation)
        {
            Grade grade = new Grade();
            grade.gradeType = gradeType;
            grade.schoolYear = schoolYear;
            grade.sectionId = sectionId;
            grade.studentId = studentId;
            grade.studentSectionAssociationId = studentSectionAssocation;

            return grade;
        }
    }
}