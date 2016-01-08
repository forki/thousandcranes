module ThousandCranes.ScenarioBuilder

open HttpFs.Client
open System
open System.Text
open Ploeh.AutoFixture
open Newtonsoft.Json

let go () =
    let requests =
        requestBodies
        |> Seq.map (fun body ->
            createRequest Post <| Uri(uriString)
                |> withHeader (ContentType (ContentType.Parse "application/json" |> Option.get))
                |> withBodyString (JsonConvert.SerializeObject(body)))

    requests
        |> Seq.map (Request.responseAsString)
        |> Async.Parallel
        |> Async.RunSynchronously
        |> Array.iter (printfn "%s")
