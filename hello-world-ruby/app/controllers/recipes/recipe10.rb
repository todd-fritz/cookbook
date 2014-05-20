#Update a Student’s Profile
def recipe10

  new_other_name = create_other_name()

  students_url = REST_URL + "students/"

  students = JSON.parse RestClient.get students_url, headers
  student = students[0]
  
  student['otherName'] << new_other_name
  
  student_url = students_url + student['id']
  logger.info "Student URl: " + student_url
  student_json = JSON.generate student
  response = RestClient.put student_url, student_json, headers
  #logger.info "Student update at location: " + response.headers[:location]           
  
  return response.to_s
end

def create_other_name()
  new_other_name = other_name("Mark", "Twain", "Alias")
  
end

def other_name( first_name, last_surname, other_name_type, optional = {})
  other_name_hash = {
    :firstName => first_name,
    :lastSurname => last_surname,
    :otherNameType => other_name_type
  }
  other_name_hash.update optional 
end
