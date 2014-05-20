# Update an Assessment
def recipe6() 

  assess_loc, new_assessment = create_assessment()
  
  logger.info "Assessment location:" + assess_loc
  
  assess_loc, update_assessment = update_assessment(assess_loc)
  
  # Clean up the datastore
  RestClient.delete assess_loc, headers
  
  return assess_loc
end

# Update an assessment 
def update_assessment(endpoint)
   
  an_assessment = JSON.parse RestClient.get endpoint, headers
  
  an_assessment[:maxRawScore] = rand(100)
  RestClient.put endpoint, an_assessment.to_json, headers
  
  return endpoint, an_assessment
end
