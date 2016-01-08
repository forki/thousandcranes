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

let httpConf = http
    .baseURL("http://computer-database.gatling.io")
    .acceptHeader("text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8")
    .doNotTrackHeader("1")
    .acceptLanguageHeader("en-US,en;q=0.5")
    .acceptEncodingHeader("gzip, deflate")
    .userAgentHeader("Mozilla/5.0 (Windows NT 5.1; rv:31.0) Gecko/20100101 Firefox/31.0")

let scn =
    scenario("BasicSimulation")
    .exec(http("request_1") 
    .get("/"))
    .pause(5)

(setUp(scn.inject(atOnceUsers(1))).protocols(httpConf)

