/**
 * routes/recipes/recipe_07.js
 * Handler for Recipe #7 requests.
 *
 * @author  Lindsay Parker
 * @version 1.0.0, 06 JUN 2013
 */
 
 var config = require('../../config');
  
  
/**
 * Delete an Assessment.
 * Recipe #7
 * @param client
 * @return function
 */
 exports.deleteAssessment = function (client){
 	return function(req, res){
 	
 	    var recipeDescription = 'Delete an assessment';
 	
 		// Create path to 'assessments/{id}' endpoint
		var path = config.api.base_url + config.api.api_call_append + config.api.api_version +
					'/assessments/{id}';
		
		// DELETE request to api using Restify module .del function.
		// 'client' object is passed into function (Created in 'Hello World')
		client.del(path, function(err, req2, res2) {
  			
  			// Prints previous endpoint of deleted assessment to webpage for verification
			res.render('recipes', { tab_name: 'Recipes', 
									title: 'Recipe 7',
									description: recipeDescription,
									data: path }
			);
		});
 	};
 };
 