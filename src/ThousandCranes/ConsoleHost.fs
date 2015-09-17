module ThousandCrains.ConsoleHost

open ThousandCranes
open System.Diagnostics
open System

let SetServicePointManagerDefaults () =
    System.Net.ServicePointManager.DefaultConnectionLimit <- Int32.MaxValue

[<EntryPoint>]
let main argv =
    SetServicePointManagerDefaults ()
    let system = ActorSystem.create()

    let scriptPath = Seq.tryPick Some argv
    match scriptPath with
    | Some scriptPath ->
        let stopwatch = Stopwatch.StartNew()

        ActorSystem.f system
        ScriptRunner.evalScript scriptPath

        stopwatch.Stop()
        printfn "Finished in: %dms" stopwatch.ElapsedMilliseconds
    | None ->      
        printfn "Usage: ThousandCranes.exe [scriptPath]"

    Console.ReadLine() |> ignore
    0
