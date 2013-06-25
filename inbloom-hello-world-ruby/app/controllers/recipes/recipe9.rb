#Find a Student by Criteria
def recipe9() 
 
  field = "name.firstName"
  criteria = "Daniel"
  url = REST_URL + "students?" + field + "=" + criteria
  
  students = JSON.parse RestClient.get url, headers
  
  return url, students

end
