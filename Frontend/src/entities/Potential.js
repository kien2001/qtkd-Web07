class Potential {
  constructor () {
    // this.PotentialId = ''
    this.PotentialName = ''
    this.PotentialCode
  }

  setInsertPotential (potentialName, potentialCode) {
    this.PotentialName = potentialName
    this.PotentialCode = potentialCode
  }
}
export default Potential
