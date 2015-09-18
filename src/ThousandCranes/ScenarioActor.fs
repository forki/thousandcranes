module ScenarioActor

type Greet = Greet of string

let agent = MailboxProcessor<Greet>.Start <| fun inbox -> 
    let rec messageLoop () =
        async {
            let! message = inbox.Receive()            
            match message with
            | Greet who -> printf "Hello, %s!\n" who
            return! messageLoop() }
    messageLoop ()