module ThousandCranes.HttpRequest

open System
open System.Net.Http.Headers

type Http = {
    BaseUrl: Uri
    DefaultRequestHeaders: HttpRequestHeaders 
}