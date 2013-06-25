# Assign a Grade to a Student
def recipe11()
  grade_loc, grade = create_grade()
  
  RestClient.delete grade_loc, headers
  
  return grade_loc, grade
end

def create_grade()
  new_grade = grade(
    "2000-2001",
    "Final",
    "d0947e676f376eedc3775993a4e5a309d588e553_id",
    "19b6e33399c60f9b839959fbf9d1bea50aea4477_id",
    "f6683a18d00d897c59c1533170fe1fb3eb7e23d6_id",
    "d0947e676f376eedc3775993a4e5a309d588e553_id6319342cc147e3038ac0894a4fa24511c90d4656_id"
  )
  
  endpoint = REST_URL + "grades"
  response = RestClient.post endpoint, new_grade.to_json, headers
  grade_loc = response.headers[:location]  
  
  logger.info "new grader URL: " + grade_loc
  
  return grade_loc, new_grade
  
end

def grade( school_year, grade_type, section_id, student_id, grading_period_id, student_section_associations_id,  optional ={})
  grade_hash = {
    :studentSectionAssociationId => student_section_associations_id,
    :sectionId => section_id,
    :studentId => student_id,
    :gradingPeriodId => grading_period_id,
    :schoolYear => school_year,
    :gradeType => grade_type
  }
  grade_hash.update optional

end
