/**
 * routes/recipes/recipe_08.js
 * Handler for Recipe #8 requests.
 *
 * @author  Lindsay Parker
 * @version 1.0.0, 06 JUN 2013
 */
 
 var config = require('../../config');
  
  
/**
 * Enroll a student in a section.
 * Recipe #8
 * @param client
 * @return function
 */
 exports.createStudentSectionAssoc = function (client){
 	return function(req, res){
 	
 	    var recipeDescription = 'Enroll a student in a section';
 

 		// Assign existing section ID and student ID for test purposes. This ID will most likely
 		// be passed into the function in a functional application.
 		var sectionId = SECTION_ID_HERE;
 		var studentId = STUDENT_ID_HERE;
 		
 		// Create path to 'studentSectionAssociations' endpoint
		var path = config.api.base_url + config.api.api_call_append + config.api.api_version +
					'/studentSectionAssociations';
		
		var studentSectionAssoc = {
			'beginDate': '2012-12-31',
			'sectionId': sectionId,
			'studentId': studentId
		};

		// POST request to api using Restify module .post function.
		// 'client' object is passed into function (Created in 'Hello World')
		client.post(path, studentSectionAssoc, function (err, req2, res2, obj) {
		
			var newStudentSectionAssocEndpoint = res2.headers['location'];
				
			// Prints new Staff/EducationOrganization association endpoint to webpage for verification
			res.render('recipes', { tab_name: 'Recipes', 
									title: 'Recipe 8',
									description: recipeDescription,
									data: newStudentSectionAssocEndpoint }
			);
		});
 	};
 };
 