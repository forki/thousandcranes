module ThousandCrains.ConsoleHost

open ThousandCranes
open System

let SetServicePointManagerDefaults () =
    System.Net.ServicePointManager.DefaultConnectionLimit <- Int32.MaxValue

[<EntryPoint>]
let main argv =
    SetServicePointManagerDefaults ()

    let scriptPath = Seq.tryPick Some argv
    match scriptPath with
    | Some scriptPath ->
        let result = ScriptRunner.evalScript scriptPath        
        printfn "%A" result
    | None -> printfn "Usage: ThousandCranes.exe [scriptPath]"

    Console.ReadLine() |> ignore
    0
