namespace Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Capstone3.Domain
open Capstone3.Operation
open Capstone3
open Capstone3
open Capstone3
open Microsoft.VisualStudio.TestTools.UnitTesting
open Capstone3

[<TestClass>]
type TestClass () =
    
    let dbPath = "C:\Users\Andrea Angeloni\Desktop\Temp"

    let createAccount customerName = {
        AccountID = System.Guid.NewGuid();
        Owner = { Name = customerName };
        Balance = 0m;
        Operations = []
        }

    let populateOperations account =
        operateAccount OperationType.Deposit 100m account |>
        operateAccount OperationType.Widthdraw 200m

    let populateAccounts = 
            [| (createAccount "Customer 1"); (createAccount "Customer 2") |]
            |> Array.map populateOperations
  
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
        let account = createAccount "Customer Name"

        let updatedAccount = populateOperations account

        Assert.AreEqual(2, updatedAccount.Operations.Length)
        Assert.AreEqual(100m, updatedAccount.Balance)
        Assert.AreEqual(OperationResult.Rejected, (updatedAccount.Operations.Item(1)).OperationResult)

    [<TestMethod>]
    member this.AccountIsCorreclySerDeser() =
        let account = createAccount "Customer Name" |> populateOperations
        let serializedData = Persistance.serializeAccount account
        let deserializedData = Persistance.deserializeAccount serializedData

        Assert.AreEqual(account, deserializedData)

    [<TestMethod>]
    member this.AccountsAreCorrectlySavedAndRestored () =
        let accounts = populateAccounts
        accounts |> (Persistance.writeAllAccounts dbPath)

        let readAccounts = Persistance.readAllAccounts dbPath

        CollectionAssert.AreEqual(accounts, readAccounts)

    [<TestMethod>]
    member this.AccountIsCorrectlyFetchedByName () =
        let account = 
            Persistance.readAllAccounts dbPath 
            |> Array.map populateOperations
            |> Persistance.getAccount  "Customer 1"
        
        Assert.AreEqual("Customer 1", account.Owner.Name)
        Assert.AreEqual(0m, account.Balance)

    [<TestMethod>]
    member this.AccountIsCorrectlyFetchedFromDb () =
        let account = Persistance.getAccountFromDb dbPath  "Customer 1"
        
        Assert.AreEqual("Customer 1", account.Owner.Name)
        Assert.AreEqual(100m, account.Balance)



