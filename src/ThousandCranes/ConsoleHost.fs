module ThousandCrains.ConsoleHost

open ThousandCranes
open System.Diagnostics
open System

[<EntryPoint>]
let main argv =
    let scriptPath = Seq.tryPick Some argv
    match scriptPath with
    | Some scriptPath ->
        let stopwatch = Stopwatch.StartNew()

        ActorSystem.f()
        ScriptRunner.evalScript scriptPath

        stopwatch.Stop()
        printfn "Finished in: %dms" stopwatch.ElapsedMilliseconds
    | None ->      
        printfn "Usage: ThousandCranes.exe [scriptPath]"

    Console.ReadLine() |> ignore
    0
