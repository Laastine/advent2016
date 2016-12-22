module eight

open System
open System.IO
open System.Text.RegularExpressions

let screen = Array2D.init 6 50 (fun x y -> '.')

let handleRotate(input: List<string>) =
  printfn "handleRotate %A" input
  ""

let handleRect(input: List<string>) =
  printfn "handleRect %A" input
  ""

let selectCmd(input: List<string>) =
  if input.Head = "rotate" then handleRotate(input)
  else if input.Head = "rect" then handleRect(input)
  else failwith "error"

let tokenizeString(x: string) = Regex.Split(x, " ") |> List.ofArray

let readInputData =
  System.IO.File.ReadLines("./eight2.txt")
    |> Seq.toList
    |> List.map(tokenizeString >> selectCmd)

[<EntryPoint>]
let main argv =
  printfn "%A" screen
  0 // return an integer exit code
