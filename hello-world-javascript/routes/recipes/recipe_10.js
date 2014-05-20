/**
 * routes/recipes/recipe_10.js
 * Handler for Recipe #10 requests.
 *
 * @author  Lindsay Parker
 * @version 1.0.0, 06 JUN 2013
 */
 
 var config = require('../../config');
  
/**
 * Update a student's profile.
 * Recipe #10
 * @param client
 * @return function
 */
 exports.updateStudent = function (client){
 	return function(req, res){
 	
 	    var recipeDescription = 'Update a student\'s profile';
 	
 		// Assign existing student ID for test purposes. This ID will most likely
 		// be passed into the function in a functional application.
 		var studentId = STUDENT_ID_HERE;
 	
 		// Create path to 'students/{id}' endpoint
		var path = config.api.base_url + config.api.api_call_append + config.api.api_version + 
					'/students/' + studentId;
		
		// An student's key fields (studentUniqueStateId) cannot be modified once created.
		var updatedValuesForStudent = {
			'otherName': [{
				'firstName': 'Mark',
				'lastSurname': 'Twain',
				'otherNameType': 'Alias'
			}]
		}
		
		// GET request to api using Restify module .get function.
		// 'client' object is passed into function (Created in 'Hello World')
 		client.get(path, function (err, req2, res2, student) {
 		
			var studentToUpdate = {};
			
 			// Traverses 'student' JSON, removing links and metadata, copies fields/values being updated
 			// from 'updatedValuesForStudent' JSON and copies fields/values not being updated from 'student' JSON
 			// and places these fields/values in 'studentToUpdate' JSON.
			for (var child in student) {
				if (child === 'id' || child === 'links' || child === 'entityType') { 
					continue;
				} else if (updatedValuesForStudent[child]) {
					studentToUpdate[child] = updatedValuesForStudent[child];
					delete updatedValuesForStudent[child];
				} else {
					studentToUpdate[child] = student[child];
				}
 			}
 			for (var child in updatedValuesForStudent) {
 				if (child === 'id' || child === 'links' || child === 'entityType') {
					continue;
				} else {
					studentToUpdate[child] = updatedValuesForStudent[child];
				}
 			}
				
			// PUT request to api using Restify module .put function.
			client.put(path, studentToUpdate, function (err, req3, res3, obj) {

				var updatedStudent = req3['body'];
					
				// Prints new Assessment endpoint to webpage for verification
				res.render('recipes', { tab_name: 'Recipes', 
										title: 'Recipe 10',
										description: recipeDescription,
										data: updatedStudent }
				);
			});
		});
 	};
 };
 