module Capstone3.Persistance

open Newtonsoft.Json
open Domain
open System.IO

let serializeAccount account = JsonConvert.SerializeObject(account)
let deserializeAccount serialData = JsonConvert.DeserializeObject<Account>(serialData)
let getAllJsonPath directoryPath = Directory.GetFiles(directoryPath, "*.json")

let readAccountFromFile filePath =
    let serialData = File.ReadAllText(filePath)
    deserializeAccount serialData

let readAllAccounts directoryPath =
    getAllJsonPath directoryPath |> Array.map readAccountFromFile

let writeAccountToFile (directoryPath) account =
    let filepath = $"{directoryPath}\\{account.Owner.Name}.json"
    File.WriteAllText (filepath, serializeAccount account)

let writeAllAccounts directoryPath accounts =
    accounts |> Array.iter (writeAccountToFile directoryPath)
