open System

printfn "Get programming with F# - Capstone 1"
printfn "q for quit"

let waitForInput () = 
    Console.ReadLine()

let rec manageInput () =
    printfn "Insert command"
    match waitForInput() with
    | "q" -> 0
    | _ -> printfn "Unsupported command"
           manageInput()


[<EntryPoint>]
let main args =
    manageInput()
