namespace Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open Capstone3.Domain

[<TestClass>]
type TestClass () =

    [<TestMethod>]
    member this.TestMethodPassing () =
        Assert.IsTrue(true);

    [<TestMethod>]
    member this.JsonSerDeserWorking () =
        let customer1 = { Name = "Customer 1"}
        Assert.IsTrue(true);