/**
 * routes/recipes/recipe_02.js
 * Handler for Recipe #2 requests.
 *
 * @author  Lindsay Parker
 * @version 1.0.0, 06 JUN 2013
 */
 
 var config = require('../../config');
  
/**
 * Determining user's educational organizations ids.
 * Recipe #2
 * @param client
 * @return function
 */
 exports.getUserEdOrgIDs = function (client){
 	return function(req, res){
 	
 	    var recipeDescription = 'Determine users\'s educational organizations ids';
 	    
 		// Create path to users 'home' endpoint
		var path = config.api.base_url + config.api.api_call_append + config.api.api_version + '/home';
		
		// GET request to api using Restify module .get function.
		// 'client' object is passed into function (Created in 'Hello World')
 		client.get(path, function (err, req2, res2, userJSON) {

 			// Searches 'userJSON' for object containing 'getEducationOrganizations'
 			// and assigns api endpoint to 'edOrgsPath'
			for (var i = 0; i < userJSON['links'].length; i++) {
 				if (userJSON['links'][i]['rel'] === 'getEducationOrganizations'){
 					var edOrgsPath = userJSON['links'][i]['href'];
 					break;
 				}
 			}
			
 			client.get(edOrgsPath, function (err, req3, res3, userEdOrgsJSON) {
 				var edOrgsIDs = [];
 				
 				// Searches all objects in 'userEdOrgsJSON' for 'id' field
 				// and pushes 'id' values to an array
				for (var i = 0; i < userEdOrgsJSON.length; i++) {
 					edOrgsIDs.push(userEdOrgsJSON[i]['id']);
 				}
 					
 				// Prints userEdOrgsIDs to webpage for verification
				res.render('recipes', { tab_name: 'Recipes', 
										title: 'Recipe 2',
										description: recipeDescription,
										data: edOrgsIDs }
				);
 			});
 		});
 	};
 };
 