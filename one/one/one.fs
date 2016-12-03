module one

open System.Net
open System
open System.IO

let adventOneUrl = "http://adventofcode.com/2016/day/1/input"

let route = "R5, L2, L1, R1, R3, R3, L3, R3, R4, L2, R4, L4, R4, R3, L2, L1, L1, R2, R4, R4, L4, R3, L2, R1, L4, R1, R3, L5, L4, L5, R3, L3, L1, L1, R4, R2, R2, L1, L4, R191, R5, L2, R46, R3, L1, R74, L2, R2, R187, R3, R4, R1, L4, L4, L2, R4, L5, R4, R3, L2, L1, R3, R3, R3, R1, R1, L4, R4, R1, R5, R2, R1, R3, L4, L2, L2, R1, L3, R1, R3, L5, L3, R5, R3, R4, L1, R3, R2, R1, R2, L4, L1, L1, R3, L3, R4, L2, L4, L5, L5, L4, R2, R5, L4, R4, L2, R3, L4, L3, L5, R5, L4, L2, R3, R5, R5, L1, L4, R3, L1, R2, L5, L1, R4, L1, R5, R1, L4, L4, L4, R4, R3, L5, R1, L3, R4, R3, L2, L1, R1, R2, R2, R2, L1, L1, L2, L5, L3, L1"

let parseElements(elements: List<String>): List<String * String> =
  elements
  |> List.map (fun (e) ->
    let orientation = e.[0].ToString()
    let distance = e.[1..]
    (orientation, distance))

let out(inputList: List<String * String>, initialList: List<int * int * int * int * int>): List<int * int * int * int * int> =
  let rec orientations(inputList: List<String * String>, acc: List<int * int * int * int * int>): List<int * int * int * int * int> =
    let (orientation, north, east, south, west) = acc.Head
    if inputList.IsEmpty then acc
    else
      let (o,d) = inputList.Head
      let (_, distance) = System.Int32.TryParse(d)
      match o with
      | "R" when orientation = 0 -> orientations(inputList.Tail, (orientation+1, north, east+distance, south, west)::acc)
      | "R" when orientation = 1 -> orientations(inputList.Tail, (orientation+1, north, east, south+distance, west)::acc)
      | "R" when orientation = 2 -> orientations(inputList.Tail, (orientation+1, north, east, south, west+distance)::acc)
      | "R" when orientation = 3 -> orientations(inputList.Tail, (0, north+distance, east, south, west)::acc)
      | "L" when orientation = 0 -> orientations(inputList.Tail, (3, north, east, south, west+distance)::acc)
      | "L" when orientation = 1 -> orientations(inputList.Tail, (orientation-1, north+distance, east, south, west)::acc)
      | "L" when orientation = 2 -> orientations(inputList.Tail, (orientation-1, north, east+distance, south, west)::acc)
      | "L" when orientation = 3 -> orientations(inputList.Tail, (orientation-1, north, east, south+distance, west)::acc)
      | _ -> printfn "ERROR"; acc
  orientations(inputList, initialList)

let reduceRoute(input: List<String * String>) =
  let orientation = 0
  out(input, [(orientation, 0, 0, 0, 0)])
  |> List.map (fun x ->
                let (_, north, east, south, west) = x
                (north, east, south, west))
  |> List.head

let parseRoute(input: String) =
  input.Split([|", "|], StringSplitOptions.None)
  |> Array.toList
  |> parseElements
  |> reduceRoute

[<EntryPoint>]
let main argv =
  let route = parseRoute route
  printfn "%A" route
  let (north, east, south, west) = route
  let length = abs(north - south) + abs (east - west)
  printfn "%A" length
  0
