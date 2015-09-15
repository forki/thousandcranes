#r "packages/Fake/tools/FakeLib.dll"

open Fake

let title = "ThousandCranes"
let authors = [ "James Gregory" ]
let githubLink = "https://github.com/jamesjrg/thousandcranes"

Target "CleanBuildDebug" <| fun _ ->
    !! @"ThousandCranes.sln"
    |> MSBuildDebug "" "Clean"
    |> Log "MsBuild"

Target "CleanBuildRelease" <| fun _ ->
    !! @"ThousandCranes.sln"
    |> MSBuildRelease "" "Clean"
    |> Log "MsBuild"

Target "BuildDebug" <| fun _ ->
    !! @"ThousandCranes.sln"
    |> MSBuildDebug "" "Build"
    |> Log "MsBuild"
    
Target "BuildRelease" <| fun _ ->
    !! @"ThousandCranes.sln"
    |> MSBuildRelease "" "Build"
    |> Log "MsBuild"

Target "Test" <| fun _ ->
    ()

"CleanBuildDebug" ==> "BuildDebug"
"CleanBuildRelease" ==> "BuildRelease"
"BuildDebug" ==> "Test"

RunTargetOrDefault "BuildDebug"
