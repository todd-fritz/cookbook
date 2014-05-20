class SessionsController < ApplicationController
	
	def create
		auth = request.env["omniauth.auth"]
		session[:token] = auth[:credentials][:token]
		logger.info auth.inspect

		redirect_to root_url
	end

	def destroy
		RestClient.get  REST_URL + 'system/session/logout'
	
		session.delete(:token)	


		redirect_to root_url
	end

end
