open Suave  
open System.Net

[<EntryPoint>]
let main argv = 

    let parsePort s =
        match System.UInt16.TryParse s with
        | (true, port) -> port
        | _ -> 8083us

    let parseIp s =
        match IPAddress.TryParse s with
        | (true, ip) -> ip
        | _ -> IPAddress.Loopback    

    let (ip, port) =
        match argv.Length with
        | 1 ->
            (IPAddress.Loopback, parsePort argv.[0])
        | n when n >= 2 ->        
            (parseIp argv.[0], parsePort argv.[1])
        | _ -> (IPAddress.Loopback, 8083us)        

    // start suave
    startWebServer
        { defaultConfig with
            bindings = 
                [ 
                    HttpBinding.create 
                        HTTP ip
                        port 
                ] 
        }
        (   
            "<h1>F#/Suave loves Dropstack!</h1>"
            |> Successful.OK
        )        

    0