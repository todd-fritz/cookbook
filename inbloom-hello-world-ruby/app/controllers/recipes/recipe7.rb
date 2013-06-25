# Start Recipe #7
# Delete an assessment
def recipe7() 

  # create an assessment, then promptly delete it
  assess_loc, new_assessment = create_assessment()
  
  logger.info "Assessment location:" + assess_loc
  
  assess_loc = delete_assessment(assess_loc)
   
  return assess_loc
end

def delete_assessment(location)

  response = RestClient.delete location, headers

  return location
end
