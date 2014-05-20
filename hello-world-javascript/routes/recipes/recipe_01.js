/**
 * routes/recipes/recipe_01.js
 * Handler for Recipe #1 requests.
 *
 * @author  Lindsay Parker
 * @version 1.0.0, 06 JUN 2013
 */
 
 var config = require('../../config');
  
  
/**
 * Retrieving a user's unique id.
 * Recipe #1
 * @param client
 * @return function
 */
 exports.getUserUID = function (client){
 	return function(req, res){
 		
 		var recipeDescription = 'Retrieving a user\'s unique id';
 		
 		// Create path to users 'home' endpoint
		var path = config.api.base_url + config.api.api_call_append + config.api.api_version + '/home';
		
		// GET request to api using Restify module .get function.
		// 'client' object is passed into function (Created in 'Hello World')
 		client.get(path, function (err, req2, res2, userJSON) {
 			
 			// Use string manipulation to parse 'rel' field of associative array 'userJSON'
 			// and assign value to userUniqueID
 			for (var i = 0; i < userJSON['links'].length; i++) {
 				if (userJSON['links'][i]['rel'] === 'self'){
 					var userUniqueID = userJSON['links'][i]['href'].substring(userJSON['links'][i]['href'].lastIndexOf('/') + 1);
 					break;
 				}
 			}
 			
 			// Prints userUniqeID to webpage for verification
			res.render('recipes', { tab_name: 'Recipes', 
										title: 'Recipe 1',
										description: recipeDescription,
										data: userUniqueID }
			);
 		});
 	};
 };
 