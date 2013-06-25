class HelloController < ApplicationController

  def index
  
   	if session[:token]
   	  redirect_to :controller => 'recipe', :action => 'index'
			#@endpoint = 'home'
			#url = REST_URL + @endpoint
			#@rest_home = RestClient.get url, headers
			
	# Start recipe #3 and #4
#      staff_loc = create_staff()
#			logger.info "New Staff Location: " + staff_loc
			
#			edu_org = JSON.parse RestClient.get REST_URL + "educationOrganizations", headers
#			edu_org_loc = REST_URL + "educationOrganizations" + edu_org[0]["id"]
#       logger.info "EduOrg: " + edu_org.inspect   
          
#      staff_assoc_loc = associate_with_edu_org(staff_loc, edu_org_loc)
#      logger.info "New Staff Assoc Location: " + staff_assoc_loc
      
#      @endpoint = staff_assoc_loc.sub REST_URL, ""
#      @json = JSON.parse RestClient.get staff_assoc_loc, headers

#      RestClient.delete staff_assoc_loc, headers
#      RestClient.delete staff_loc, headers
 	# End recipe #3 and #4
      
  # Start recipe #5, #6, #7
      #new_assessment_endpoint = create_assessment() 
      #logger.info "New Assessment endpoint: " + new_assessment_endpoint
      
      #endpoint = update_assessment(new_assessment_endpoint)
      #@json = JSON.parse RestClient.get endpoint, headers
               
      #RestClient.delete new_assessment_endpoint, headers           
  # End recipe #5, #6, #7

  # Start Recipe #8      
      #student = "https://api.sandbox.inbloom.org/api/rest/v1.2/students/0993685bdafcb960937ad8278ec9d2fac8a5ef83_id"
      #section = "https://api.sandbox.inbloom.org/api/rest/v1.2/sections/e8310178e3e6d5cb8ef88d79bf7ef513998cf390_id"
      #begin_date = "2013-05-01"
      
      #assoc = create_student_section_association( student, section )
      #@json = JSON.parse RestClient.get assoc, headers
      
      #RestClient.delete assoc, headers
  # End Recipe #8
  
  # Start Recpipe #9
      #url = REST_URL + "students?name.firstName=Damon"
      
      #@json = JSON.parse RestClient.get url, headers
  # Stop Recpipe #9
  
  # Start Recpipe #10


  # Stop Recpipe #10
  
	  end 	
  end

# Start Recipe #4  
  # Associate staff with an educational organization
  def associate_with_edu_org( staff_url, edu_org_url )
    staff = JSON.parse RestClient.get staff_url, headers
    staff_id = staff['id']
        
    edu_org = JSON.parse RestClient.get REST_URL + "educationOrganizations", headers
    edu_org_id = edu_org[0]['id']

    staff_class = "Other"
    begin_date =  "2013-05-01" 
      
    assoc = staff_education_organization_association( staff_id, edu_org_id, staff_class, begin_date)
    #    logger.info "Assoc: " + assoc.to_json

    url = REST_URL + "staffEducationOrgAssignmentAssociations"

    response = RestClient.post url, assoc.to_json, headers
    #    logger.info "Staff-EduOrg Association Location: " + response.headers[:location]
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
  
  # Start Recipe #8
  # Enroll  a student in a section
  def create_student_section_association( student_endpoint, section_endpoint )
    student = JSON.parse RestClient.get student_endpoint, headers
    student_id = student['id']
    
    section = JSON.parse RestClient.get section_endpoint, headers
    section_id = section['id']
    
    begin_date = Time.now.strftime("%Y-%m-%d")
    
    assoc = student_section_association(student_id, section_id, begin_date)
    logger.info "Association: " + assoc.to_json
      
    url = REST_URL + "studentSectionAssociations"
    response = RestClient.post url, assoc.to_json, headers
    location = response.headers[:location]
  end
  
def student_section_association( student_id, section_id, begin_date, optional ={} )
  student_section_association_hash = {
    :studentId => student_id,
    :sectionId => section_id,
    :beginDate => begin_date
  }
  student_section_association_hash.update optional 
end
  
  # Start Recipe #6
  # Update an assessment 
  def update_assessment(endpoint)
     
    an_assessment = JSON.parse RestClient.get endpoint, headers
    
    an_assessment[:maxRawScore] = 100
    RestClient.put endpoint, an_assessment.to_json, headers
    
    return endpoint
  end

  # Start Recipe #5  
  # Create an assessment
  def create_assessment() 
    new_assessment = assessment(
      "rand1", 
      [assessment_identification_code("Other","Some Identification")],
      1
    )
   
    url = REST_URL + 'assessments'
    response = RestClient.post url, new_assessment.to_json, headers
    location = response.headers[:location]
  end

  def assessment( assessment_title, array_of_assessment_identification_codes, version, optional ={} )
    assessment_hash = {
      :assessmentTitle => assessment_title,
      :assessmentIdentificationCode => array_of_assessment_identification_codes,
      :version => version
    }
    assessment_hash.update optional
  end

  def assessment_identification_code( identification_system, id, optional ={} )
     assessment_identification_code = {
      :identificationSystem => identification_system,
      :ID => id
     }
     assessment_identification_code.update optional
  end

  
   
  # Start Recipe #3
  def create_staff() 
    new_staff = staff("yyy", name("John", "Doe"), "Male", "No Degree")
    
    url = REST_URL + 'staff'
    logger.info "Staff: " + new_staff.to_json
	  response = RestClient.post url, new_staff.to_json, headers
    logger.info "Staff Location: " + response.headers[:location]          
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
  
  def headers
   raise ArgumentError, "This funciton assumes that session[:token] is defined" unless session[:token]
	  headers = {
	    :Content_Type => 'application/vnd.slc+json',
		  :Accept => 'application/vnd.slc+json',
		  :Authorization => 'bearer ' + session[:token]
    }		  
  end
end
