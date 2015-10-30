module ThousandCranes.MailboxActor

type Greet = Greet of string

let create () = MailboxProcessor<Greet>.Start <| fun inbox -> 
    let rec messageLoop () =
        async {
            let! message = inbox.Receive()
            match message with
            | Greet who -> printf "Hello, %s!\n" who
            return! messageLoop() }
    messageLoop ()