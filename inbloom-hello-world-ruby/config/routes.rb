RubyHello::Application.routes.draw do

	root :to => 'hello#index'

	match "/auth/inbloom/callback" => "sessions#create"
	match "/signout" => "sessions#destroy", :as => :signout

end
