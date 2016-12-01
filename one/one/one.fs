module one

open System.Net
open System
open System.IO

let adventOneUrl = "http://adventofcode.com/2016/day/1/input"

let fetchFromURL callback url =
  let request = WebRequest.Create(Uri(adventOneUrl))
  use response = request.GetResponse()
  use stream = response.GetResponseStream()
  use reader = new IO.StreamReader(stream)
  callback reader url

let printContent (reader: IO.StreamReader) url =
  let content = reader.ReadToEnd()
  printfn "Content %s" content
  content

let route = "R5, L2, L1, R1, R3, R3, L3, R3, R4, L2, R4, L4, R4, R3, L2, L1, L1, R2, R4, R4, L4, R3, L2, R1, L4, R1, R3, L5, L4, L5, R3, L3, L1, L1, R4, R2, R2, L1, L4, R191, R5, L2, R46, R3, L1, R74, L2, R2, R187, R3, R4, R1, L4, L4, L2, R4, L5, R4, R3, L2, L1, R3, R3, R3, R1, R1, L4, R4, R1, R5, R2, R1, R3, L4, L2, L2, R1, L3, R1, R3, L5, L3, R5, R3, R4, L1, R3, R2, R1, R2, L4, L1, L1, R3, L3, R4, L2, L4, L5, L5, L4, R2, R5, L4, R4, L2, R3, L4, L3, L5, R5, L4, L2, R3, R5, R5, L1, L4, R3, L1, R2, L5, L1, R4, L1, R5, R1, L4, L4, L4, R4, R3, L5, R1, L3, R4, R3, L2, L1, R1, R2, R2, R2, L1, L1, L2, L5, L3, L1"

let position = (0,0,0,0)

let parseElements(elements: List<String>) =
  elements
  |> List.map (fun (e) ->
    let orientation = e.[0].ToString()
    let distance = e.[1..];
    printfn "Orientation: %s Distance: %s" orientation distance
    (orientation, distance))

let parseRoute(input: String) =
  input.Split(' ')
  |> Array.toList
  |> parseElements

[<EntryPoint>]
let main argv =
  let dada = parseRoute route
  printfn "%A" dada
  0 // return an integer exit code
