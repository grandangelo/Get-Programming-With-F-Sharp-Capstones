module Capstone3.Persistance

open Newtonsoft.Json
open Domain

let serializeAccount account = JsonConvert.SerializeObject(account)
let deserializeAccount serialData = JsonConvert.DeserializeObject<Account>(serialData)

