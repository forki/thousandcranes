module ThousandCranes.ActorSystem

open Akka.Actor
open Akka.FSharp

type Greet = Greet of string

let create () =
    ActorSystem.Create "ThousandCranes"

let run system = 
    let greeter = spawn system "greeter" <| fun mailbox ->
        let rec loop() = actor {
            let! msg = mailbox.Receive()
            match msg with
            | Greet who -> printf "Hello, %s!\n" who
            return! loop() }
        loop()

    
    greeter <! Greet "World"

    