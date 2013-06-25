# Determine User’s id
def recipe1()
  return getUserUID(session[:token])
end


def getUserUID(token)
  # get the home endpoint
	data = JSON.parse RestClient.get REST_URL + "home", headers
	links = data['links']

	# find the self link
	self_url = links.each { |link| if link['rel'] = 'self'
 					break link['href']
	      end }
	
	# pull the id field out of the self data
	logger.info self_url
	data = JSON.parse RestClient.get self_url, headers
	uid = data['id']
	
	return uid
end
