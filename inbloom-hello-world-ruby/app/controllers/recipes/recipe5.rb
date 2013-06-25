# Create an assessment
def recipe5()

  assess_loc, new_assessment = create_assessment() 
  
  # Clean up the datastore
  RestClient.delete assess_loc, headers

  
  return assess_loc, new_assessment
end

def create_assessment()
  # create a new assessment onject
  new_assessment = assessment(
    "Generic Assessment Name2", 
    [assessment_identification_code("Other","Some Identification")],
    1
  )
 
  url = REST_URL + 'assessments'
  
  #store the new assesment in the data store
  response = RestClient.post url, new_assessment.to_json, headers
  assess_loc = response.headers[:location]  
 
  return assess_loc, new_assessment.to_json
end

# helper methods
################
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
