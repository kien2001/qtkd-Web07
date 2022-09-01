class Customer {
  constructor () {
    // this.CustomerId = '';
    this.PotentialId = ''
    this.PotentialName = ''
    this.LastMiddleName = ''
    this.FirstName = ''
    this.CompanyPhoneNum = ''
    this.CustomerPhoneNum = ''
    this.OtherPhoneNum = ''
    this.CustomerEmail = ''
    this.CompanyEmail = ''
    this.TaxCode = ''
    this.Zalo = ''
    this.AddressId = ''
    this.Description = ''
    this.SharingUse = ''
    this.DisableCall = ''
    this.DisableMail = ''
    this.Gender = ''
    this.GenderName = ''
    this.DateOfBirth = ''
    this.Facebook = ''
    this.Layout = ''
    this.Owner = ''
    this.Vocative = ''
    this.DepartmentId = ''
    this.OrganizationId = ''
    this.PositionId = ''
    this.SourceId = ''
  }

  setInsertCustomer (vocative, lastMiddleName, firstName, departmentId, positionId, customerPhoneNum, companyPhoneNum, sourceId, zalo, customerEmail, companyEmail, taxCode, description, sharingUse) {
    this.Vocative = vocative
    this.LastMiddleName = lastMiddleName
    this.FirstName = firstName
    this.DepartmentId = departmentId
    this.PositionId = positionId
    this.CustomerPhoneNum = customerPhoneNum
    this.CompanyPhoneNum = companyPhoneNum
    this.SourceId = sourceId
    this.Zalo = zalo
    this.CustomerEmail = customerEmail
    this.CompanyEmail = companyEmail
    this.TaxCode = taxCode
    this.Description = description
    this.SharingUse = sharingUse
  }
}
export default Customer
