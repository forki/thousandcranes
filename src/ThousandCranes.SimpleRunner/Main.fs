module ThousandCranes.SimpleRunner.Main

open ThousandCranes
open System
open System.IO

[<EntryPoint>]
let main argv =
    ThousandCranes.Http.setServicePointManagerDefaults ()

    let scriptPath = Seq.tryPick Some argv
    match scriptPath with
    | Some scriptPath ->
        let scriptText = File.ReadAllText(scriptPath)
        let result = ScriptRunner.evalScript scriptText
        printfn "%A" result
    | None -> printfn "Usage: ThousandCranes.exe [scriptPath]"

    Console.ReadLine() |> ignore
    0
