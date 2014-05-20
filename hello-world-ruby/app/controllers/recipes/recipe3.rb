# Create a Staff Member
def recipe3()
  # create a new staff member in the data store
  # Recipe 3
  staff_loc = create_staff()
	logger.info "New Staff Location: " + staff_loc
	
  json = JSON.parse RestClient.get staff_loc, headers
  
  # Clean up the datastore
  RestClient.delete staff_loc, headers
  
  return staff_loc, json
end

# helper methods
################
def create_staff() 
  new_staff = staff("yyy997", name("John", "Doe"), "Male", "No Degree")
  
  url = REST_URL + 'staff'
  response = RestClient.post url, new_staff.to_json, headers
  logger.info "Staff create at location: " + response.headers[:location]          
  return response.headers[:location]
end
 
def name( first, last, optional ={} )
  name_hash = { 
    :firstName => first,
    :lastSurname => last
  }  
  name_hash.update optional
end

def staff( state_id, name, sex, edu_level, optional = {} )
  staff_hash = {
    :staffUniqueStateId => state_id,
    :name => name,
    :sex => sex,
    :highestLevelOfEducationCompleted => edu_level
  }
  staff_hash.update optional
end
