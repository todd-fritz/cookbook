/**
 * routes/recipes/recipe_06.js
 * Handler for Recipe #6 requests.
 *
 * @author  Lindsay Parker
 * @version 1.0.0, 06 JUN 2013
 */
 
 var config = require('../../config');
  
  
/**
 * Update an Assessment.
 * Recipe #6
 * @param client
 * @return function
 */
 exports.updateAssessment = function (client){
 	return function(req, res){
 	
 	    var recipeDescription = 'Update an assessment';
 		
 		// Assign existing assessment ID for test purposes. This ID will most likely
 		// be passed into the function in a functional application.
 		var assessmentId = ASSESSMENT_ID_HERE;
 		
 		// Create path to 'assessments/{id}' endpoint
		var path = config.api.base_url + config.api.api_call_append + config.api.api_version +
					'/assessments/' + assessmentId;
		
		// An assessment's key fields (academicSubject, assessmentIdentificationCode, 
		// assessmentTitle, gradeLevelAssessed, version) cannot be modified once created.
		var updatedValuesForAssessment = {
			'maxRawScore': 100,
			'minRawScore': 0
		}
		
		// GET request to api using Restify module .get function.
		// 'client' object is passed into function (Created in 'Hello World')
 		client.get(path, function (err, req2, res2, assessment) {

			var assessmentToUpdate = {};
			
 			// Tranverses 'assessment' object and copies field/values to 'assessmentToUpdate' object
 			// if the fields are not metadata.  
			for (var child in assessment) {
				if (child === 'id' || child === 'links' || child === 'entityType') { 
					continue;
				} else if (updatedValuesForAssessment[child]) {
					assessmentToUpdate[child] = updatedValuesForAssessment[child];
					delete updatedValuesForAssessment[child];
				} else {
					assessmentToUpdate[child] = assessment[child];
				}
 			}
 			// If fields to be updated are not set currently in 'assessment' object,
 			// enter fields in 'assessmentToUpdate' object now
 			for (var child in updatedValuesForAssessment) {
 				if (child === 'id' || child === 'links' || child === 'entityType') {
					continue;
				} else {
					assessmentToUpdate[child] = updatedValuesForAssessment[child];
				}
 			}
				
			// PUT request to api using Restify module .put function.
			client.put(path, assessmentToUpdate, function (err, req3, res3, obj) {

				var updatedAssessment = req3['body'];
					
				// Prints new Assessment endpoint to webpage for verification
				res.render('recipes', { tab_name: 'Recipes', 
										title: 'Recipe 6',
										description: recipeDescription,
										data: updatedAssessment }
				);
			});
		});
 	};
 };
 