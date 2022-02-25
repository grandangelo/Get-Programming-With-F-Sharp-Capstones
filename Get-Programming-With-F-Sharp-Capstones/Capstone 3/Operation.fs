module Capstone3.Operation

open Capstone3.Domain

let operate operationType amount balance =
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

