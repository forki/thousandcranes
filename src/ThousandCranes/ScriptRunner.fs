module ThousandCranes.ScriptRunner

open Microsoft.FSharp.Compiler.Interactive.Shell
open System.IO
open System.Text

let evalScript scriptPath =
    let sbOut = new StringBuilder()
    let sbErr = new StringBuilder()
    let inStream = new StringReader("")
    let outStream = new StringWriter(sbOut)
    let errStream = new StringWriter(sbErr)

    (* first argument is a sacrificial victim to a call to List.tail within F# compiler services
       second stops FSCS from starting a background thread that watches stdin 
    *)
    let argv = [| "fsi.exe" ; "--noninteractive"|]

    let fsiConfig = FsiEvaluationSession.GetDefaultConfiguration()
    let fsiSession = FsiEvaluationSession.Create(fsiConfig, argv, inStream, outStream, errStream)  

    fsiSession.EvalScript(scriptPath)

