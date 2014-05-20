/**
 * routes/student.js
 * Handler for home page requests.
 *
 * @author  Lindsay Parker
 * @version 1.0.0, 17 May 2013
 */
 
 var config = require('../config');
 
 
/**
 * Retrieving all student information.
 * @param client
 * @return function
 */
 exports.getStudents = function (client){
 	return function(req, res, next){
 	
 		// Create path to 'students' endpoint
		var path = config.api.base_url + config.api.api_call_append + config.api.api_version + '/students';
		 		
 		client.get(path, function (err, req2, res2, obj) {
 		
			var json_string = JSON.stringify(obj, 'false', 2);
			
			res.render('students', { tab_name: 'Students', 
										title: 'All Students',
										data: json_string }
			);
 		});
 	};
 };
 
 
/**
 * Retrieving student information by student ID.
 * @param client
 * @return function
 */
 exports.getStudentById = function (client){
 	return function(req, res){
 		
 		var studentId = req.params.studentId;
 		
 		// Create path to users 'home' endpoint
		var path = config.api.base_url + config.api.api_call_append + config.api.api_version +
					'/students/' + studentId;
		
		// GET request to api using Restify module .get function.
		// 'client' object is passed into function (Created in 'Hello World')
 		client.get(path, function (err, req2, res2, studentJSON) {
 			
 			var studentJSON_string = JSON.stringify(studentJSON, 'false', 2);
 			
 			// Prints userUniqeID to webpage for verification
			res.render('students', { tab_name: 'Student', 
										title: 'Single Student',
										data: studentJSON_string }
			);
 		});
 	};
 };
 