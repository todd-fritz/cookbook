/**
 * routes/recipes/recipe_12.js
 * Handler for Recipe #12 requests.
 *
 * @author  Lindsay Parker
 * @version 1.0.0, 06 JUN 2013
 */
 
 var config = require('../../config');
  
  
/**
 * List parent contact information for an entire class.
 * Recipe #12
 * @param client
 * @return function
 */
 exports.getParentsForSection = function (client){
 	return function(req, res){
 	
 	    var recipeDescription = 'List parent contact information for an entire class';
 	
 		// Assign existing section ID for test purposes. This ID will most likely be passed into the function
 		// in a functional application.
 		sectionID = SECTION_ID_HERE;
 		
 		// Create path to 'sections/{id}/studentSectionAssociations/students
 		// /studentParentAssociations/parents' endpoint
		var path = config.api.base_url + config.api.api_call_append + config.api.api_version +
					'/sections/' + sectionID +
					'/studentSectionAssociations/students/studentParentAssociations';

		// GET request to api using Restify module .get function.
		// 'client' object is passed into function (Created in 'Hello World')
 		client.get(path, function (err, req2, res2, studentParentAssocJSON) {
			
			var parentContactRestrictions = [];
			
			// Traverses 'studentParentAssocJSON' object and stores contactRestrictions field in
			// parentContactRestriction array
			for (var i = 0; i < studentParentAssocJSON.length; i++) {
				parentContactRestrictions[i] = {};
				parentContactRestrictions[i]['parentId'] = studentParentAssocJSON[i]['parentId'];
				if (studentParentAssocJSON[i]['contactRestrictions']) {
					parentContactRestrictions[i]['contactRestrictions'] =
						studentParentAssocJSON[i]['contactRestrictions'];
				} else {
					parentContactRestrictions[i]['contactRestrictions'] = 'none';
				}
			}
			
			path = path + '/parents';
			
			// GET request to api using Restify module .get function.
	 		client.get(path, function (err, req3, res3, parentJSON) {
	 			
	 			// Traverses parentJSON and adds the contactRestriction field/value to the
	 			// associated parent.
	 			for (var i = 0; i < parentJSON.length; i++) {
	 				for (var j = 0; j < parentContactRestrictions.length; j++) {
	 					if (parentJSON[i]['id'] === parentContactRestrictions[j]['parentId']) {
	 						parentJSON[i]['contactRestrictions'] =
	 							parentContactRestrictions[j]['contactRestrictions'];
	 						break;
	 					}
	 				}
	 			}
	 			
	 			var parentJSON_string = JSON.stringify(parentJSON, 'false', 2);
 			
	 			// Prints userEdOrgsIDs to webpage for verification
				res.render('recipes', { tab_name: 'Recipes', 
										title: 'Recipe 12',
										description: recipeDescription,
										data: parentJSON_string }				
				);
			});
 		});
 	};
 };
 