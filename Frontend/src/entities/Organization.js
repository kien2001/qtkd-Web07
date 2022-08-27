class Organization {
  constructor () {
    // this.OrganizationId = ''
    this.BankAccount = ''
    this.BankName = ''
    this.CreatedAccountAt = ''
    this.OrganizationName = ''
    this.TypeId = ''
    this.Domain = ''
    this.Career = ''
    this.RevenueId = ''
  }

  setInsertOrganization (bankAccount, bankName, createdAccountAt, typeId, domain, career, revenueId) {
    this.BankAccount = bankAccount
    this.BankName = bankName
    this.CreatedAccountAt = createdAccountAt
    this.TypeId = typeId
    this.Domain = domain
    this.Career = career
    this.RevenueId = revenueId
  }
}
export default Organization
