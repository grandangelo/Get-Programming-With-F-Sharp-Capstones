namespace Capstone1

open System

type Destination =
    | Home
    | Office
    | Stadium
    | GasStation

type PetrolUnit = int

type TravelStage = {
    Destination : Destination
    PetrolCost : PetrolUnit
}

module main =
    let stages = [
        { Destination = Home; PetrolCost = -25 }
        { Destination = Office; PetrolCost = -50 }
        { Destination = Stadium; PetrolCost = -25 }
        { Destination = GasStation; PetrolCost = 40 }
    ]

    let waitForInput () = 
        Console.ReadLine()

    let performTravel petrolUnit travelStage =
        let petrolAfterTravel = petrolUnit + travelStage.PetrolCost
        printfn "Traveling to %A. Petrol after travel: %i" travelStage.Destination petrolAfterTravel
        petrolAfterTravel

    let rec manageInput petrolUnit =
        printfn "Current petrol: %A" petrolUnit
        printfn "Insert destination"
        printfn "q: quit - 1: Home - 2 Office - 3 Stadium - 4 GasStation"
        match waitForInput() with
        | "q" -> 0
        | "1" | "2" | "3" | "4" -> // Travel
                                   // Write remaining fuel
                                   manageInput petrolUnit
        | _ -> printfn "Unsupported command"
               manageInput petrolUnit

    [<EntryPoint>]
    let main args =
        printfn "Get programming with F# - Capstone 1"
        printfn "q for quit"
        manageInput 100
