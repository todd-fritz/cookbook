# Determine user’s educational organization id
def recipe2()
  return getEduOrgs()
end

def getEduOrgs()
  # get the home endpoint
	data = JSON.parse RestClient.get REST_URL + "home", headers
	links = data['links']
	

  # find the link to the Edu Orgs	
	eduOrgs_url = links.each do |link| 
	      if link['rel'] == 'getEducationOrganizations'
 					break link['href'].to_s
 			  end
  end

  # get the edu orgs	
	eduOrgs = JSON.parse RestClient.get eduOrgs_url, headers

  # pull out the ids and return then	
	return eduOrgs_url, eduOrgs.map {|eduOrg| eduOrg['id'] }
  
end
