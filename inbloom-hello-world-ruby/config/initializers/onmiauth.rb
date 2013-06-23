INBLOOM_CONFIG = YAML.load_file("#{::Rails.root}/config/inbloom.yml")[::Rails.env]

REST_URL = INBLOOM_CONFIG['site'] + INBLOOM_CONFIG['rest_url']

Rails.application.config.middleware.use OmniAuth::Builder do
	# if we're in development or test, use the sandbox	
	if Rails.env.development? || Rails.env.test?
	  provider :inbloom, 
			INBLOOM_CONFIG['client_id'],  
			INBLOOM_CONFIG['client_secret'],
			:setup => lambda{|env|env['omniauth.strategy'].options[:client_options].site = INBLOOM_CONFIG['site']}
	else
	  provider :inbloom, 
			INBLOOM_CONFIG['client_id'],  
			INBLOOM_CONFIG['client_secret']
	end
			
end
