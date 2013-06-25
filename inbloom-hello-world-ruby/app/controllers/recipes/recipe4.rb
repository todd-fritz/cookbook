# Create a Staff Member
def recipe4()
  # create a new staff member in the data store
  staff_loc = create_staff()
	logger.info "New Staff Location: " + staff_loc
	
	# find an edu org
	edu_org = JSON.parse RestClient.get REST_URL + "educationOrganizations", headers
	edu_org_loc = REST_URL + "educationOrganizations" + edu_org[0]["id"]
       
  # create an assocition between the staff member and the edu org
  # Recipe 4
  staff_assoc_loc = associate_with_edu_org(staff_loc, edu_org_loc)
  
  endpoint = staff_assoc_loc.sub REST_URL, ""
  json = JSON.parse RestClient.get staff_assoc_loc, headers

  # Clean up
  RestClient.delete staff_assoc_loc, headers
  RestClient.delete staff_loc, headers
  
  return endpoint, json
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

def associate_with_edu_org( staff_url, edu_org_url)
    staff = JSON.parse RestClient.get staff_url, headers
    staff_id = staff['id']
        
    edu_org = JSON.parse RestClient.get REST_URL + "educationOrganizations", headers
    edu_org_id = edu_org[0]['id']

    staff_class = "Other"
    begin_date =  "2013-05-01" 
      
    assoc = staff_education_organization_association( staff_id, edu_org_id, staff_class, begin_date)

    url = REST_URL + "staffEducationOrgAssignmentAssociations"

    response = RestClient.post url, assoc.to_json, headers

    return response.headers[:location]
  end
  
def staff_education_organization_association( staff_id, edu_org_id, staff_class, begin_date,  optional = {} )
  association_hash = {
    :staffReference => staff_id,
    :educationOrganizationReference => edu_org_id,
    :staffClassification =>  staff_class,
    :beginDate => begin_date
  }
  association_hash.update optional
end
