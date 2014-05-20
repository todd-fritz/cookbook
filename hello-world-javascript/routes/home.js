/**
 * routes/home.js
 * Handler for home page requests.
 *
 * @author  Lindsay Parker
 * @version 1.0.0, 17 May 2013
 */
 
 // GET home page
 exports.start = function(req, res){
 	var tokenExists = req['session']['accessToken'] ? true : false;
 	
 	if (tokenExists) {
 		res.render('logout', { tab_name: 'Home - Logout',
 								title: 'InBloom JavaScript Hello World'}
 						
 		);
 	} else {
 		res.render('home', { tab_name: 'Home - Login',
 								title: 'InBloom JavaScript Hello World' }
 						
 		);
 	}
 };