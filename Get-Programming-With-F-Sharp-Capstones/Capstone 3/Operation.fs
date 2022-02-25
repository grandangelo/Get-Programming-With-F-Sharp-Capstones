module Capstone3.Operation

open Capstone3.Domain

let operate (operationType:OperationType) (amount:decimal) balance =
    let newBalance = 
        match operationType with
        | OperationType.Deposit -> balance + amount
        | OperationType.Widthdraw -> balance - amount
    OperationResult.Accepted, newBalance
