﻿namespace Capstone1

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
        { Destination = Home; PetrolCost = 25 }
        { Destination = Office; PetrolCost = 50 }
        { Destination = Stadium; PetrolCost = 25 }
        { Destination = GasStation; PetrolCost = 10 }
    ]

    let canTravel petrolUnit travelStage =
        let requiredPetrol = petrolUnit - travelStage.PetrolCost
        match requiredPetrol with
        | _ when requiredPetrol >= 0 -> true
        | _ -> false

    let getPetrolAfterTravel petrolUnit travelStage =
        let petrolAfterTravel = petrolUnit - travelStage.PetrolCost
        match travelStage.Destination with
        | _ when travelStage.Destination = Destination.GasStation -> petrolAfterTravel + 50
        | _ -> petrolAfterTravel

    let waitForInput () = 
        Console.ReadLine()

    let performTravel petrolUnit travelStage =
        match canTravel petrolUnit travelStage with
        | false -> 
            printfn "Not enough petrol"
            petrolUnit
        | true -> 
            let petrolAfterTravel = getPetrolAfterTravel petrolUnit travelStage
            printfn "Traveling to %A. Petrol after travel: %i" travelStage.Destination petrolAfterTravel
            petrolAfterTravel

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
