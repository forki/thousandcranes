module ThousandCranes.Scenarios.Tests.Integration.SmokeTests

open Fuchu
open Suave
open Suave.Filters
open Suave.Operators
open System
open System.Net
open System.Threading
open Swensen.Unquote.Assertions

let suaveApp =
  choose
    [ POST >=> choose 
        [ path "/hello" >=> Successful.OK "Hello World!"
          RequestErrors.NOT_FOUND "Nope." ] ]

let cts = new CancellationTokenSource()

let setUp () =
    let config =
      { defaultConfig with
          cancellationToken = cts.Token
          logger = Logging.Loggers.saneDefaultsFor Suave.Logging.LogLevel.Warn }
    let listening, server = startWebServerAsync config suaveApp
    Async.Start(server, cts.Token) |> ignore

let tearDown () =
    cts.Cancel true |> ignore

let runTest scriptText doAssertions =
    setUp ()
    let result = ThousandCranes.ScriptRunner.evalScript scriptText
    doAssertions result 
    tearDown ()

[<Tests>]
let smokeTests =
    testList "Smoke tests"
        [
        testCase "Light-winged Smoke, Icarian bird," (fun () ->
            runTest "foo" (fun result ->
                test <@ "x" = "x" @>))
        ]

