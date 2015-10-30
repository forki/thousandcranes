module ThousandCranes.Http

open HttpFs.Client
open System
open System.Text

let setServicePointManagerDefaults () =
    System.Net.ServicePointManager.DefaultConnectionLimit <- Int32.MaxValue

let deleteMe () =
    let request =
        createRequest Post <| Uri("https://example.com")
        |> withQueryStringItem "search" "jeebus"
        |> withBasicAuthentication "myUsername" "myPassword" // UTF8-encoded
        |> withHeader (UserAgent "Chrome or summat")
        |> withHeader (Custom ("X-My-Header", "hi mum"))
        |> withAutoDecompression DecompressionScheme.GZip 
        |> withAutoFollowRedirectsDisabled
        |> withCookie (Cookie.Create("session", "123", path="/"))
        |> withBodyString "This body will make heads turn"
        |> withBodyStringEncoded "Check out my sexy foreign body" (Encoding.UTF8)
        |> withBody (BodyRaw [| 1uy; 2uy; 3uy |])
        |> withBody (BodyString "this is a greeting from Santa")
        // if you submit a BodyForm, then Http.fs will also set the correct Content-Type, so you don't have to
        |> withBody (BodyForm
            [
                // if you only have this in your form, it will be submitted as application/x-www-form-urlencoded
                NameValue ("submit", "Hit Me!")

                // a single file form control, selecting two files from browser
                FormFile ("file", ("file1.txt", ContentType.Create("text", "plain"), Plain "Hello World"))
                FormFile ("file", ("file2.txt", ContentType.Create("text", "plain"), Binary [|1uy; 2uy; 3uy|]))
            ])
        |> withResponseCharacterEncoding (Encoding.UTF8)
        |> withKeepAlive false
        |> withProxy {
              Address = "proxy.com";
              Port = 8080;
              Credentials = ProxyCredentials.Custom { username = "Tim"; password = "Password1" } }
    
    async {
        use! response = getResponse request // disposed at the end of async, don't
                                            // fetch outside async body
        // the above doesn't download the response, so you'll have to do that:
        let! bodyStr = Response.readBodyAsString response
        // OR:
        //let! bodyBs = Response.readBodyAsBytes

        // remember HttpFs doesn't buffer the stream (how would we know if we're
        // downloading 3GiB?), so once you use one of the above methods, you can't do it
        // again, but have to buffer/stash it yourself somewhere.
        return bodyStr
    }