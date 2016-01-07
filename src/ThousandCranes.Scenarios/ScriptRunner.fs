module ThousandCranes.ScriptRunner

open Microsoft.FSharp.Compiler.Interactive.Shell
open System
open System.IO
open System.Text
open System.Diagnostics

type TestResult = {
    TimeTaken: TimeSpan
    ScriptOutput : string
    ScriptErrors : string
}

let evalScript scriptPath =
    let sbOut = new StringBuilder()
    let sbErr = new StringBuilder()
    let inStream = new StringReader("")
    let outStream = new StringWriter(sbOut)
    let errStream = new StringWriter(sbErr)

    (* first argument is a legacy requirement of F# compiler services code
       second stops FSCS from starting a background thread that watches stdin 
    *)
    let argv = [| "sacrifical victim of List.tail" ; "--noninteractive"|]

    let fsiConfig = FsiEvaluationSession.GetDefaultConfiguration()
    let fsiSession = FsiEvaluationSession.Create(fsiConfig, argv, inStream, outStream, errStream)

    let stopwatch = Stopwatch.StartNew()
    fsiSession.EvalScript(scriptPath)
    stopwatch.Stop()

    { TimeTaken = stopwatch.Elapsed
      ScriptOutput = sbOut.ToString()
      ScriptErrors = sbErr.ToString() }

