module ThousandCranes.SimpleRunner.Main

open ThousandCranes
open System

[<EntryPoint>]
let main argv =
    ThousandCranes.Http.setServicePointManagerDefaults ()

    let scriptPath = Seq.tryPick Some argv
    match scriptPath with
    | Some scriptPath ->
        let result = ScriptRunner.evalScript scriptPath
        printfn "%A" result
    | None -> printfn "Usage: ThousandCranes.exe [scriptPath]"

    Console.ReadLine() |> ignore
    0
