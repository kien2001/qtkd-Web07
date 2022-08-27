class Address {
  constructor () {
    this.AddressName = ''
    this.HomeNumber = ''
    this.PostalCode = ''
    this.CountryId = ''
    this.CityId = ''
    this.DistrictId = ''
    this.WardId = ''
  }

  setInsertAddress (addressName, homeNumber, postalCode, countryId, cityId, districtId, wardId) {
    this.AddressName = addressName
    this.HomeNumber = homeNumber
    this.PostalCode = postalCode
    this.CountryId = countryId
    this.CityId = cityId
    this.DistrictId = districtId
    this.WardId = wardId
  }
}
export default Address
