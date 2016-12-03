module one

open System.Net
open System
open System.IO

let adventOneUrl = "http://adventofcode.com/2016/day/1/input"

let routeText = "R5, L2, L1, R1, R3, R3, L3, R3, R4, L2, R4, L4, R4, R3, L2, L1, L1, R2, R4, R4, L4, R3, L2, R1, L4, R1, R3, L5, L4, L5, R3, L3, L1, L1, R4, R2, R2, L1, L4, R191, R5, L2, R46, R3, L1, R74, L2, R2, R187, R3, R4, R1, L4, L4, L2, R4, L5, R4, R3, L2, L1, R3, R3, R3, R1, R1, L4, R4, R1, R5, R2, R1, R3, L4, L2, L2, R1, L3, R1, R3, L5, L3, R5, R3, R4, L1, R3, R2, R1, R2, L4, L1, L1, R3, L3, R4, L2, L4, L5, L5, L4, R2, R5, L4, R4, L2, R3, L4, L3, L5, R5, L4, L2, R3, R5, R5, L1, L4, R3, L1, R2, L5, L1, R4, L1, R5, R1, L4, L4, L4, R4, R3, L5, R1, L3, R4, R3, L2, L1, R1, R2, R2, R2, L1, L1, L2, L5, L3, L1"

let rec last = function
    | hd ::[] -> hd
    | hd :: tl -> last tl
    | _ -> failwith "Empty list."


let rec reverse list =
    match list with
    | [] -> []
    | [x] -> [x]
    | head::tail -> reverse tail @ [head]

let parseElements(elements: List<String>): List<String*String> =
  elements
  |> List.map (fun (e) ->
    let orientation = e.[0].ToString()
    let distance = e.[1..]
    (orientation, distance))

let incDistance(orientation: int, dist: int, acc: List<int*int*int*int*int>): List<int*int*int*int*int> =
  let rec recur(dist: int, orientation: int, accList: List<int*int*int*int*int>) =
    let (_, north, east, south, west) = accList.Head
    match dist with
    | d when d > 0 && orientation = 0 -> recur(dist-1, orientation, (orientation, north+1, east, south, west)::accList)
    | d when d > 0 && orientation = 1 -> recur(dist-1, orientation, (orientation, north, east+1, south, west)::accList)
    | d when d > 0 && orientation = 2 -> recur(dist-1, orientation, (orientation, north, east, south-1, west)::accList)
    | d when d > 0 && orientation = 3 -> recur(dist-1, orientation, (orientation, north, east, south, west-1)::accList)
    | _ -> accList
  recur(dist, orientation, acc)

let iterateRoute(inputList: List<String*String>, initialList: List<int*int*int*int*int>): List<int*int*int*int*int> =
  let rec orientations(inputList: List<String*String>, acc: List<int*int*int*int*int>): List<int*int*int*int*int> =
    let (orientation, north, east, south, west) = acc.Head
    if inputList.IsEmpty then acc
    else
      let (o,d) = inputList.Head
      let (_, distance) = System.Int32.TryParse(d)
      match o with
      | "R" when orientation = 3 -> orientations(inputList.Tail, incDistance(0, distance, acc))
      | "R" when orientation < 4 -> orientations(inputList.Tail, incDistance(orientation+1, distance, acc))
      | "L" when orientation = 0 -> orientations(inputList.Tail, incDistance(3, distance, acc))
      | "L" when orientation < 4 -> orientations(inputList.Tail, incDistance(orientation-1, distance, acc))
      | _ -> failwith "ERROR"
  orientations(inputList, initialList)

let reduceRoute(input: List<String*String>) =
  let orientation = 0
  iterateRoute(input, [(orientation, 0, 0, 0, 0)])
  |> List.map (fun x ->
                  let (_, north, east, south, west) = x
                  (north, east, south, west))

let parseRoute(input: String) =
  input.Split([|", "|], StringSplitOptions.None)
  |> Array.toList
  |> parseElements
  |> reduceRoute
  |> List.head

let calcAbsoluteDistance(position: int*int*int*int): Int32 =
  let (north, east, south, west) = position
  abs(north + south) + abs (east + west)

let calcAbsolutePosition(position: int*int*int*int): int*int =
  let (north, east, south, west) = position
  ((east + west), (north + south))

let compareTuples(a: int*int, b: int*int): bool =
  let (x1,y1) = a
  let (x2,y2) = b
  x1 = x2 && y1 = y2

let detectDuplicates(listWithDuplicates: List<int*int>): List<int*int> =
  let rec recur(posList: List<int*int>, acc: List<int*int>) =
    match posList with
    | [] -> acc
    | head::tail ->
                  let found = (List.filter(fun x -> compareTuples(x, head)) tail)
                  if List.isEmpty found then recur(tail, acc)
                  else recur(tail, found @ acc)
  recur(listWithDuplicates, [])

let detectDoubleVisits(input: String) =
  let allAbosolutePos =
    input.Split([|", "|], StringSplitOptions.None)
    |> Array.toList
    |> parseElements
    |> reduceRoute
    |> List.map calcAbsolutePosition
  detectDuplicates allAbosolutePos

[<EntryPoint>]
let main argv =
  let route = parseRoute routeText
  let finalDistance = calcAbsoluteDistance route
  printfn "Final position: %A, length: %A" route finalDistance
  let duplicates = detectDoubleVisits routeText
  printfn "Duplicates %A" duplicates
  let firstContact = duplicates.Head
  let (x,y) = firstContact
  let length = abs x + abs y
  printfn "%A" length
  0
