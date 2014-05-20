/**
 * routes/recipes/recipe_05.js
 * Handler for Recipe #5 requests.
 *
 * @author  Lindsay Parker
 * @version 1.0.0, 06 JUN 2013
 */
 
 var config = require('../../config');
  
  
/**
 * Create an Assessment.
 * Recipe #5
 * @param client
 * @return function
 */
 exports.createAssessment = function (client){
 	return function(req, res){

 	    var recipeDescription = 'Create an assessment';


 		// Create path to 'assessments' endpoint
		var path = config.api.base_url + config.api.api_call_append + config.api.api_version +
					'/assessments';
		
		var assessment = {
			'academicSubject': 'Reading',
			'assessmentIdentificationCode': [{
				'assigningOrganizationCode': 'Enter assigning orgs code or ID here',
				'ID': 'Enter assessment ID here',
				'identificationSystem': 'Other'
				}],
			'assessmentTitle': '1984',
			'gradeLevelAssessed': 'Tenth grade',
			'version': 42
		};

		// POST request to api using Restify module .post function.
		// 'client' object is passed into function (Created in 'Hello World')
		client.post(path, assessment, function (err, req2, res2, obj) {

			var newAssessmentEndpoint = res2.headers['location'];
				
			// Prints new Assessment endpoint to webpage for verification
			res.render('recipes', { tab_name: 'Recipes', 
									title: 'Recipe 5',
									description: recipeDescription,
									data: newAssessmentEndpoint }
			);
		});
 	};
 };
 