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
        match petrolAfterTravel with
        | petrol when petrolAfterTravel >= 0 -> printfn "Traveling to %A. Petrol after travel: %i" travelStage.Destination petrolAfterTravel
                                                petrolAfterTravel
        | _ -> printfn "Not enough petrol."
               petrolUnit

    let getDestination command =
        match command with
        | "1" -> Some(Destination.Home)
        | "2" -> Some(Destination.Office)
        | "3" -> Some(Destination.Stadium)
        | "4" -> Some(Destination.GasStation)
        | _ -> None

    let getTravelStage destination = stages |> List.filter (fun i -> i.Destination = destination) |> List.exactlyOne

    let rec manageInput petrolUnit =
        printfn "Current petrol: %A" petrolUnit
        printfn "Insert destination"
        printfn "q: quit - 1: Home - 2 Office - 3 Stadium - 4 GasStation"
        let command = waitForInput()
        match command with
        | "q" -> 0
        | _ -> let destination = getDestination command
               let petrolAfterTravel = 
                match destination with
                | Some currentDestination -> performTravel petrolUnit (getTravelStage currentDestination)
                | None -> printfn "Unsupported command"
                          petrolUnit
               manageInput petrolAfterTravel
                                    
    [<EntryPoint>]
    let main args =
        printfn "Get programming with F# - Capstone 1"
        printfn "q for quit"
        manageInput 100
