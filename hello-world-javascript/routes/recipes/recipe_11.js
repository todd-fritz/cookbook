/**
 * routes/recipes/recipe_11.js
 * Handler for Recipe #11 requests.
 *
 * @author  Lindsay Parker
 * @version 1.0.0, 06 JUN 2013
 */
 
 var config = require('../../config');
  
  
/**
 * Assign a grade to a student.
 * Recipe #11
 * @param client
 * @return function
 */
 exports.createGradeForStudent = function (client){
 	return function(req, res){

 	    var recipeDescription = 'Assign a grade to a student';

 		// Assign existing section ID, student ID, and student/section association ID for test purposes.
 		// These IDs will most likely be passed into the function in a functional application.
 		var sectionId = SECTION_ID_HERE;
 		var studentId = STUDENT_ID_HERE;
 		var studentSectionAssociationId = STUDENT_SECTION_ASSOC_ID_HERE;
 		
 		// Create path to 'grades' endpoint
		var path = config.api.base_url + config.api.api_call_append + config.api.api_version +
					'/grades';
		
		var grade = {
			'gradeType': 'Exam',
			'letterGradeEarned': 'A+',
			'numericGradeEarned': 99,
			'schoolYear': '2011-2012',
			'sectionId': sectionId,
			'studentId': studentId,
			'studentSectionAssociationId': studentSectionAssociationId
		};

		// POST request to api using Restify module .post function.
		// 'client' object is passed into function (Created in 'Hello World')
		client.post(path, grade, function (err, req2, res2, obj) {

			var newGradeEndpoint = res2.headers['location'];
				
			// Prints new Assessment endpoint to webpage for verification
			res.render('recipes', { tab_name: 'Recipes', 
									title: 'Recipe 11',
									description: recipeDescription,
									data: newGradeEndpoint }
			);
		});
 	};
 };
 