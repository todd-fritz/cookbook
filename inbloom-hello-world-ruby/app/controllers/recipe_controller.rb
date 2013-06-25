class RecipeController < ApplicationController

  def show
    Dir[File.dirname(__FILE__) + '/recipes/*.rb'].each {|file| require file }
    
    @recipe = params["recipe"]
    
    @endpoint, @data = self.send(@recipe)
  end

  def index
    
  end
  
  def headers
    raise ArgumentError, "This funciton assumes that session[:token] is defined" unless session[:token]
    headers = {
      :Content_Type => 'application/vnd.slc+json',
	    :Accept => 'application/vnd.slc+json',
	    :Authorization => 'bearer ' + session[:token]
    }		  
  end
end
