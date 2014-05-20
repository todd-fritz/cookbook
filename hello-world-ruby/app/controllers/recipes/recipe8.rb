# Enroll a Student in a Section
def recipe8()

  assoc_loc, assoc = create_student_section_association()

  # Clean up the datastore
  RestClient.delete assoc_loc, headers
  
  return assoc_loc, assoc
end

def create_student_section_association()
   # setup
  students = JSON.parse RestClient.get REST_URL + "students", headers
  student_id = students[0]['id']
  
  sections = JSON.parse RestClient.get REST_URL + "sections", headers
  section_id = sections[0]['id']

  begin_date = Time.now.strftime("%Y-%m-%d")
  
  # create assocation
  assoc = student_section_association(student_id, section_id, begin_date)
  logger.info "Association: " + assoc.to_json
    
  # store new assocation in datastore
  url = REST_URL + "studentSectionAssociations"
  response = RestClient.post url, assoc.to_json, headers
  location = response.headers[:location]
  
  return location, assoc.to_json
end
  
def student_section_association( student_id, section_id, begin_date, optional ={} )
  student_section_association_hash = {
    :studentId => student_id,
    :sectionId => section_id,
    :beginDate => begin_date
  }
  student_section_association_hash.update optional 
end
