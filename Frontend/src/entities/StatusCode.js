const StatusCode = {
    ErrorCode:{
        DuplicateName : "E001",
        DuplicatePotentialCode : "E002",
        NotFound : "E003",
        NotValue : "E004",
        ProcessError : "E005",
        InsertFailed : "E006",
        UpdateFailed : "E007",
        NotExists : "E008",
        DuplicateEmail : "E009",
        DuplicatePhone : "E010",
        DuplicateTaxCode : "E011",
        DuplicateBankAccount : "E012",
        DeletedFail : "E013",
        DuplicateValue : "E014",
        EmptyFirstName : "E015",
    },
    SuccessCode:{
        GetSuccess : "S01",
        UpdateSuccess : "S02",
        DeleteSuccess : "S03",
        SearchSuccess : "S04",
        InsertSuccess : "S05",
    }
}
export default StatusCode