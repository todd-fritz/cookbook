using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using inBloom_c_sharp_hello_world.Recipes;

namespace inBloom_c_sharp_hello_world.Controllers
{
    [Authorize]
    public class RecipeController : Controller
    {
        //
        // GET: /Recipe/

        public ActionResult Index()
        {
            ViewBag.Title = "Recipes";
            ViewBag.Data = "Recipe List:";
            return View();
        }


        public ActionResult Recipe1()
        {
            ViewBag.Title = "Recipe 1";
            ViewBag.Description = "User's unique inBloom id";

            string token = Session["inbloomtoken"].ToString();

            Recipe rec = new Recipe1();
            ViewBag.Data = rec.RunRecipe(token);
            return View("Index");
        }

        public ActionResult Recipe2()
        {
            ViewBag.Title = "Recipe 2";
            ViewBag.Description = "User's educational organization id's";

            string token = Session["inbloomtoken"].ToString();

            Recipe rec = new Recipe2();
            ViewBag.Data = rec.RunRecipe(token);
            return View("Index");
        }

        public ActionResult Recipe3()
        {
            ViewBag.Title = "Recipe 3";
            ViewBag.Description = "Created staff member's REST url";

            string token = Session["inbloomtoken"].ToString();

            Recipe rec = new Recipe3();
            ViewBag.Data = rec.RunRecipe(token);
            return View("Index");
        }

        public ActionResult Recipe4()
        {
            ViewBag.Title = "Recipe 4";
            ViewBag.Description = "Created staff association's id";

            string token = Session["inbloomtoken"].ToString();

            Recipe2 recGetEduOrg = new Recipe2();
            string eduOrgId = recGetEduOrg.getEducationalOrgs(token).First().id;

            Recipe3 recCreate = new Recipe3();
            string staffId = recCreate.RunRecipe(token);

            Recipe4 recAssoc = new Recipe4();
            ViewBag.Data = recAssoc.RunRecipe(token: token, staffId: staffId, eduOrgId: eduOrgId);

            return View("Index");
        }

        public ActionResult Recipe5()
        {
            ViewBag.Title = "Recipe 5";
            ViewBag.Description = "Created an assessment with id:";

            string token = Session["inbloomtoken"].ToString();

            Recipe recipe5 = new Recipe5();
            ViewBag.Data = recipe5.RunRecipe(token);

            return View("Index");
        }

        public ActionResult Recipe6()
        {
            ViewBag.Title = "Recipe 6";
            ViewBag.Description = "Updated assessment with id:";

            string token = Session["inbloomtoken"].ToString();

            // Create an assessment
            Recipe recipe5 = new Recipe5();
            string assessmentId = recipe5.RunRecipe(token);

            // Alter that assessment
            Recipe6 recipe6 = new Recipe6();
            ViewBag.Data = recipe6.RunRecipe(token, assessmentId);

            return View("Index");
        }

        public ActionResult Recipe7()
        {
            ViewBag.Title = "Recipe 7";
            ViewBag.Description = "Delete Assessment";

            string token = Session["inbloomtoken"].ToString();

            // Create an assessment
            Recipe recipe5 = new Recipe5();
            string assessmentId = recipe5.RunRecipe(token);

            // Alter that assessment
            Recipe6 recipe6 = new Recipe6();
            recipe6.RunRecipe(token, assessmentId);

            // Delete that assessment
            Recipe7 recipe7 = new Recipe7();
            ViewBag.Data = recipe7.RunRecipe(token, assessmentId);

            return View("Index");
        }

        public ActionResult Recipe8()
        {
            ViewBag.Title = "Recipe 8";
            ViewBag.Description = "Enroll a student to section. Displays the id of the StudentSectionAssociation";

            string token = Session["inbloomtoken"].ToString();

            Recipe recipe8 = new Recipe8();
            ViewBag.Data = recipe8.RunRecipe(token);

            return View("Index");
        }

        public ActionResult Recipe9()
        {
            ViewBag.Title = "Recipe 9";
            ViewBag.Description = "Name and Id of students matching query operators";

            string token = Session["inbloomtoken"].ToString();

            Recipe recipe9 = new Recipe9();
            ViewBag.Data = recipe9.RunRecipe(token);


            return View("Index");
        }

        public ActionResult Recipe10()
        {
            ViewBag.Title = "Recipe 10";
            ViewBag.Description = "Update student";

            string token = Session["inbloomtoken"].ToString();

            Recipe recipe10 = new Recipe10();
            ViewBag.Data = recipe10.RunRecipe(token);

            return View("Index");
        }

        public ActionResult Recipe11()
        {
            ViewBag.Title = "Recipe 11";
            ViewBag.Description = "Assign a Grade to a Student";

            string token = Session["inbloomtoken"].ToString();

            Recipe recipe11 = new Recipe11();
            ViewBag.Data = recipe11.RunRecipe(token);


            return View("Index");
        }

        public ActionResult Recipe12()
        {
            ViewBag.Title = "Recipe 12";
            ViewBag.Description = "Not implemented yet";

            string token = Session["inbloomtoken"].ToString();

            Recipe recipe12 = new Recipe12();
            ViewBag.Data = recipe12.RunRecipe(token);

            return View("Index");
        }
    }
}