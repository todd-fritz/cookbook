#List Parent Contact Information for an Entire Class
def recipe12()
  # grab the first section
  sections = JSON.parse RestClient.get REST_URL + "sections", headers
  section = sections[0]
  

  # get the parent associations  
  parent_assoc = getStudentParentAssociationsBySection(section['id'])

  
  # pull out the contact restrictions
  contact_restrictions = Hash.new;
 
  parent_assoc.each do |assoc|
    contact_restrictions[assoc['parentId']] = assoc['contactRestrictions']
  end
  
    parents = JSON.parse RestClient.get REST_URL + "parents", headers

  # Add contact restrictions to the parent obj
  parents.each do |parent|
    parent['contactRestrictions'] = contact_restrictions[parent["id"]]
    logger.info "contactRestrictions: " + contact_restrictions[parent["id"]].to_s
  end

  return "parents", parents
end

def getStudentParentAssociationsBySection ( section_id )

  url = REST_URL + "sections/" + section_id + "/studentSectionAssociations/students/studentParentAssociations";
  assocs = JSON.parse RestClient.get url, headers
end
