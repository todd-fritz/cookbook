/**
 * app.js
 * Main application setup.
 *
 * @author  Lindsay Parker
 * @version 1.0.0, 17 May 2013
 */

 // Modules required for application
 var express = require('express')
  , restify = require('restify')
  , stylus  = require('stylus')
  , http = require('http')
  , path = require('path')
  , url = require('url')
  , util = require('util');
  
 // Application files
 var config = require('./config')
  , SLC = require('./SLC')
  , home = require('./routes/home')
  , students = require('./routes/students');
 
 // Individual recipe files
 var recipe_01 = require('./routes/recipes/recipe_01')
  , recipe_02 = require('./routes/recipes/recipe_02')
  , recipe_03 = require('./routes/recipes/recipe_03')
  , recipe_04 = require('./routes/recipes/recipe_04')
  , recipe_05 = require('./routes/recipes/recipe_05')
  , recipe_06 = require('./routes/recipes/recipe_06')
  , recipe_07 = require('./routes/recipes/recipe_07')
  , recipe_08 = require('./routes/recipes/recipe_08')
  , recipe_09 = require('./routes/recipes/recipe_09')
  , recipe_10 = require('./routes/recipes/recipe_10')
  , recipe_11 = require('./routes/recipes/recipe_11')
  , recipe_12 = require('./routes/recipes/recipe_12');

 var app = express();
 
 // Creating a instance of SLC for authentication
 var inbloom = new SLC(config.api.base_url, 
                  config.api.client_id, 
                  config.api.client_secret, 
                  config.api.callback_uri,
                  config.api.api_version);

 // Create a JSON client using Restify module
 var client = restify.createJsonClient({
	url: 'https://api.sandbox.inbloom.org',
	headers: { accept: 'application/vnd.slc+json',
				authorization: 'bearer ',
				'content-type': 'application/vnd.slc+json'
				},
	version: '*'
 });
 
 var oauth_path = url.parse(config.api.callback_uri)['pathname'];
 
 // Configuration for Express, Passport, Jade, and Stylus middlewares
 app.configure(function(){
	app.set('port', process.env.PORT || 3000);
	app.set('views', __dirname + '/views');
	app.set('view engine', 'jade');
	app.use(express.favicon());
	app.use(express.logger('dev'));
	app.use(express.bodyParser());
	app.use(express.methodOverride());
	app.use(express.cookieParser('think about the kids'));
	app.use(express.session({ secret: 'osengage'}));
	app.use(app.router);
	app.use(stylus.middleware(__dirname + '/public'));
	app.use(express.static(path.join(__dirname, 'public')));
 });

 // Development only
 app.configure('development', function(){
	app.use(express.errorHandler({ dumpExceptions: true, showStack: true }));
 });
 // Production and staging only
 app.configure('production', 'staging', function(){
	app.use(express.errorHandler());
 });
 

 // If the access token is active, it will allow next route handler in line to handle 
 // the request, if the token is expired, then it will redirect to login page
 function requireToken() {
    return function(req, res, next) {
        if (req.session.accessToken) {
            next();
        }
        else {
            res.redirect('/home');
        }
    }
 }

//-----------Routing Table---------------//

/**
 * Redirect the user to the OAuth 2.0 provider (inBloom) for authentication.
 * When complete, inBloom will redirect the user back to the 
 * redirect url specified in the configuration file
 */
 app.get('/auth/inbloom', function(req, res) {
 	res.redirect(inbloom.getLoginURL());
 });                            

/**
 * The OAuth 2.0 provider (inBloom) has redirected the user back to the application.
 * Finish the authentication process by attempting to obtain an access
 * token.  If authorization was granted, the user will be logged in.
 * Otherwise, authentication has failed.
 */
 app.get(oauth_path, function(req, res) {
	inbloom.oauth({code: req['query']['code']}, function(accessToken) {
		req.session.accessToken = accessToken;
		client['headers']['authorization'] = 'bearer ' + accessToken;
		res.redirect('/students');
 	})
 });

 app.get('/', home.start);
 app.get('/home', home.start);
 app.get('/students', students.getStudents(client));
 app.get('/students/:studentId', students.getStudentById(client));
 
 // Logout and redirect to user login page.
 app.get('/logout', function (req, res) {
    req.session.destroy();
    inbloom.logout(function (response) {
        res.redirect('/');
    });
 });
 
 // Recipes
 app.get('/recipes/recipe1', recipe_01.getUserUID(client));
 app.get('/recipes/recipe2', recipe_02.getUserEdOrgIDs(client));
 app.get('/recipes/recipe3', recipe_03.createStaffMember(client));
 app.get('/recipes/recipe4', recipe_04.createStaffEdOrgAssoc(client));
 app.get('/recipes/recipe5', recipe_05.createAssessment(client));
 app.get('/recipes/recipe6', recipe_06.updateAssessment(client));
 app.get('/recipes/recipe7', recipe_07.deleteAssessment(client));
 app.get('/recipes/recipe8', recipe_08.createStudentSectionAssoc(client));
 app.get('/recipes/recipe9', recipe_09.getStudentByQuery(client));
 app.get('/recipes/recipe10', recipe_10.updateStudent(client));
 app.get('/recipes/recipe11', recipe_11.createGradeForStudent(client));
 app.get('/recipes/recipe12', recipe_12.getParentsForSection(client));

 //--------------End of Routing Table----------------//


 http.createServer(app).listen(app.get('port'), function(){
	console.log('Express server listening on port ' + app.get('port'));
 });
