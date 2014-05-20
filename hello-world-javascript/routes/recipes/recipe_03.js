/**
 * routes/recipes/recipe_03.js
 * Handler for Recipe #3 requests.
 *
 * @author  Lindsay Parker
 * @version 1.0.0, 06 JUN 2013
 */
 
 var config = require('../../config');
  

/**
 * Create a Staff Member.
 * Recipe #3
 * @param client
 * @return function
 */
 exports.createStaffMember = function (client){
 	return function(req, res){
 		
 	    var recipeDescription = 'Create a staff member'; 		
 		
 		// Create path to 'staff' endpoint
		var path = config.api.base_url + config.api.api_call_append + config.api.api_version + '/staff';
		
		var staff = { 
			sex: 'Male',
  			staffUniqueStateId: 'ksoze',
  			hispanicLatinoEthnicity: false,
  			name: { 
     			lastSurname: 'Soze',
     			firstName: 'Keyser' },
  			highestLevelOfEducationCompleted: 'No Degree'
		};
			
		// POST request to api using Restify module .post function.
		// 'client' object is passed into function (Created in 'Hello World')
 		client.post(path, staff, function (err, req2, res2, obj) {
 		
			var newStaffEndpoint = res2.headers['location'];
			
 			// Prints userEdOrgsIDs to webpage for verification
			res.render('recipes', { tab_name: 'Recipes', 
										title: 'Recipe 3',
										description: recipeDescription,
										data: newStaffEndpoint }
			);
 		});
 	};
 };
 