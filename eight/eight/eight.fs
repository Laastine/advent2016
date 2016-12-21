module eight

open System
open System.IO
open System.Text.RegularExpressions

let handleRotate(input: List<string>) =
  ""

let handleRect(input: List<string>) =
  ""

let readInputData =
  System.IO.File.ReadLines("./eight.txt")
    |> Seq.toList
    |> List.map(fun x -> Regex.Split(x, " "))

[<EntryPoint>]
let main argv =
  printfn "%A" readInputData
  0 // return an integer exit code
