/**
 * routes/recipes/recipe_09.js
 * Handler for Recipe #9 requests.
 *
 * @author  Lindsay Parker
 * @version 1.0.0, 06 JUN 2013
 */
 
 var config = require('../../config');
  
  
/**
 * Find student by query.
 * Recipe #9
 * @param client
 * @return function
 */
 exports.getStudentByQuery = function (client){
 	return function(req, res){
 	
 	    var recipeDescription = 'Find a student by criteria';
 		var query = 'name.firstName=Daniel';
 	
 		// Create path to 'students' endpoint including queries
		var path = config.api.base_url + config.api.api_call_append + config.api.api_version +
					'/students?' + query;
		
		// GET request to api using Restify module .get function.
		// 'client' object is passed into function (Created in 'Hello World')
 		client.get(path, function (err, req2, res2, studentsQueryJSON) {
 		
 			var studentsQueryJSON_string = JSON.stringify(studentsQueryJSON, 'false', 2);
 			
 			// Prints userUniqeID to webpage for verification
			res.render('recipes', { tab_name: 'Recipes', 
									title: 'Recipe 9',
									description: recipeDescription,
									data: studentsQueryJSON_string }
			);
 		});
 	};
 };
 