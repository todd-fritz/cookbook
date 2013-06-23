/**
 * routes/recipes/recipe_04.js
 * Handler for Recipe #4 requests.
 *
 * @author  Lindsay Parker
 * @version 1.0.0, 06 JUN 2013
 */
 
 var config = require('../../config');
  

/**
 * Associating staff with an educational organization.
 * Recipe #4
 * @param client
 * @return function
 */
 exports.createStaffEdOrgAssoc = function (client){
 	return function(req, res){
 	
 	    var recipeDescription = 'Associate staff with an educational organization';
 	
 		// Assign existing education organization ID and staff ID for test purposes. This ID will most likely
 		// be passed into the function in a functional application.
 		var educationOrganizationId = ED_ORG_ID_HERE;
 		var staffId = STAFF_ID_HERE;
 	
 		// Create path to 'staffEducationOrgAssignmentAssociations' endpoint
		var path = config.api.base_url + config.api.api_call_append + config.api.api_version + 
					'/staffEducationOrgAssignmentAssociations';
		
		var staffEdOrgAssoc = {
			'beginDate': '1999-12-31',
			'staffClassification': 'Principal',
			'educationOrganizationReference': educationOrganizationId,
			'staffReference': staffId
		};

		// POST request to api using Restify module .post function.
		// 'client' object is passed into function (Created in 'Hello World')
		client.post(path, staffEdOrgAssoc, function (err, req2, res2, obj) {

			var newStaffEdOrgAssocEndpoint = res2.headers['location'];
				
			// Prints new Staff/EducationOrganization association endpoint to webpage for verification
			res.render('recipes', { tab_name: 'Recipes', 
									title: 'Recipe 4',
									description: recipeDescription,
									data: newStaffEdOrgAssocEndpoint }
			);
		});
 	};
 };
 