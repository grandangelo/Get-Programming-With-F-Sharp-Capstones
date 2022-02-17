namespace Capstone2

open System
open System.IO

type CustomerName = string
type Balance = decimal

type Customer = {
    CustomerName : CustomerName
    Balance : Balance
}

module main =
    module logging = 
        let buildLogEntry customer = sprintf "Customer: %s - Balance %M euro" customer.CustomerName customer.Balance

        let logToConsole customer parameters = buildLogEntry customer |> printfn "%s"

        let logToFile customer filePath = File.WriteAllText(filePath, buildLogEntry customer)


    module inputManagement = 
        let waitForInput queryMessage = 
            printfn "%s" queryMessage
            Console.ReadLine()

        let stringToDecimal (inputString:string) = Decimal.TryParse(inputString)

        let rec loopForBalance() =
            let stringBalance = waitForInput "Insert initial balance"
            let convertedBalance = stringToDecimal stringBalance
            match convertedBalance with
            | false, _ -> 
                printfn "Invalid balance"
                loopForBalance()
            | true, balance when balance <= 0m -> 
                printfn "Initial balance must be positive"
                loopForBalance()
            | true, balance -> balance


    module accountManagement =
        open inputManagement

        let applyTransaction customer amount = { customer with Balance = customer.Balance + amount }

        let createAccount customerName initialBalance = { CustomerName = customerName; Balance = initialBalance }

        let createCustomer () =
            let customerName = waitForInput "Insert customer name"
            let initialBalance = loopForBalance()
            { CustomerName = customerName; Balance = initialBalance }

        let tryTransaction customer amount = 
            let amountAfterWidthDraw = customer.Balance + amount
            match amountAfterWidthDraw with
            | amountAfterWidthDraw when amountAfterWidthDraw >= 0m -> applyTransaction customer amount
            | _ ->
                printfn "Insufficient balance"
                customer


    open accountManagement
    open logging
    open inputManagement

    [<EntryPoint>]
    let main args =
        printfn "Get programming with F# - Capstone 2"
        let customer = createCustomer()

        let rec commandLoop customer =
            let command = waitForInput "Enter transaction amount (q for exit)"
            match command with
            | "q" -> 0
            | _ -> 
                let convertedAmount = stringToDecimal command
                match convertedAmount with
                | false, _ -> 
                    printfn "Invalid amount."
                    logToConsole customer ""
                    commandLoop customer
                | true, amount -> 
                    let updatedCustomer = tryTransaction customer amount
                    logToConsole updatedCustomer ""
                    commandLoop updatedCustomer

        commandLoop customer
