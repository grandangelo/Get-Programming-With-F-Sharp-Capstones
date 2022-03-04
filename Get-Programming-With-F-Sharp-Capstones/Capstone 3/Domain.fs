module Capstone3.Domain

type OperationType = | Deposit | Widthdraw
type OperationResult = | Accepted | Rejected
type Operation = { 
    OperationType: OperationType; 
    OperationAmount: decimal; 
    OperationResult: OperationResult; 
    BalanceBefore: decimal; 
    BalanceAfter: decimal 
    }
type Customer = { Name: string }
type Account = { AccountID: System.Guid; Owner: Customer; Balance: decimal; Operations: Operation list }
