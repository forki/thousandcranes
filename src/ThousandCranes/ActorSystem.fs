module ThousandCranes.ActorSystem

open Akka.Actor
open Akka.FSharp

type Greet = Greet of string

let f () = 
    let system = ActorSystem.Create "ThousandCranes"

    // Use F# computation expression with tail-recursive loop
    // to create an actor message handler and return a reference
    let greeter = spawn system "greeter" <| fun mailbox ->
        let rec loop() = actor {
            let! msg = mailbox.Receive()
            match msg with
            | Greet who -> printf "Hello, %s!\n" who
            return! loop() }
        loop()

    
    greeter <! Greet "World"

    