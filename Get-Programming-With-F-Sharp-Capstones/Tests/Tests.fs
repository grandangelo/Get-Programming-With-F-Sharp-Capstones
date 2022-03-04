namespace Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Capstone3.Domain
open Capstone3.Operation

[<TestClass>]
type TestClass () =

    [<TestMethod>]
    member this.TestMethodPassing () =
        Assert.IsTrue(true);

    [<TestMethod>]
    member this.NegativeAmountIsRejected () =
        let result1, amount1 = operateBalance OperationType.Deposit -1m 10m
        let result2, amount2 = operateBalance OperationType.Widthdraw -1m 10m
        Assert.AreEqual(OperationResult.Rejected, result1)
        Assert.AreEqual(OperationResult.Rejected, result2)
        Assert.AreEqual(10m, amount1)
        Assert.AreEqual(10m, amount2)

    [<TestMethod>]
    member this.ValidWidthdrawIsAccepted () =
        let result, amount = operateBalance OperationType.Widthdraw 1m 10m
        Assert.AreEqual(OperationResult.Accepted, result)
        Assert.AreEqual(9m, amount)

    [<TestMethod>]
    member this.InvalidWidthdrawIsRejected () =
        let result, amount = operateBalance OperationType.Widthdraw 10m 1m
        Assert.AreEqual(OperationResult.Rejected, result)
        Assert.AreEqual(1m, amount)

    [<TestMethod>]
    member this.DepositIsAccepted () =
        let result, amount = operateBalance OperationType.Deposit 10m 10m
        Assert.AreEqual(OperationResult.Accepted, result)
        Assert.AreEqual(20m, amount)

    [<TestMethod>]
    member this.OperationListIsCorrectlyPopulated () =
        let account = 
            {
            AccountID = System.Guid.NewGuid();
            Owner = { Name = "Customer Name" };
            Balance = 0m;
            Operations = []
            }

        let updatedAccount = 
            operateAccount OperationType.Deposit 100m account |>
            operateAccount OperationType.Widthdraw 200m


        Assert.AreEqual(2, updatedAccount.Operations.Length)
        Assert.AreEqual(100m, updatedAccount.Balance)
        Assert.AreEqual(OperationResult.Rejected, (updatedAccount.Operations.Item(1)).OperationResult)