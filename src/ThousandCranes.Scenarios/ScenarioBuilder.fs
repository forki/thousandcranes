module ThousandCranes.ScenarioBuilder

open HttpFs.Client
open System
open System.Text
open Ploeh.AutoFixture
open Newtonsoft.Json

type Apple = {
    SomeProperty : string
}

let fixture = Fixture()
let playerId = 149128732198127L
let actionsCount = 2
let requestsCount = 2
let requestBodies =
    fixture.Build<Apple>()
        .With(fun x -> x.SomeProperty, "fslfj")
        .CreateMany(requestsCount)

let uriString = "http://localhost:8181/blah"

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
    

