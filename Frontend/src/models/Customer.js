class Customer {
  constructor (fullName, calling, title, personPhoneNum, personPhoneWork, emailWork, emailPerson, address, organization, province, district, ward, root, typefield, description, layout, department, type, country, receipt) {
    this.fullName = fullName
    this.calling = calling
    this.type = type
    this.receipt = receipt
    this.country = country
    this.department = department
    this.title = title
    this.personPhoneNum = personPhoneNum
    this.personPhoneWork = personPhoneWork
    this.emailWork = emailWork
    this.emailPerson = emailPerson
    this.address = address
    this.organization = organization
    this.province = province
    this.district = district
    this.ward = ward
    this.root = root
    this.typefield = typefield
    this.description = description
    this.layout = layout
  }
}

export default Customer
