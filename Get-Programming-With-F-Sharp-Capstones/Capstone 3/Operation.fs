module Capstone3.Operation

open Capstone3.Domain

let operateBalance operationType amount balance =
    match amount with
    | _ when amount < 0m -> OperationResult.Rejected, balance
    | _ ->
        match operationType with
        | OperationType.Deposit -> OperationResult.Accepted, balance + amount
        | OperationType.Widthdraw ->
            let balanceAfterWidthdraw = balance - amount
            let isNegativeBalance = balanceAfterWidthdraw < 0m
            match isNegativeBalance with
                | true -> OperationResult.Rejected, balance
                | false -> OperationResult.Accepted, balanceAfterWidthdraw

let operateAccount operationType amount account =
    let result, newBalance = operateBalance operationType amount account.Balance
    let lastOperation = { 
        OperationType = operationType;
        OperationAmount = amount;
        OperationResult = result;
        BalanceBefore = account.Balance
        BalanceAfter = newBalance
    }
    let updatedOperations = account.Operations@[lastOperation]
    { account with Balance = newBalance; Operations = updatedOperations }