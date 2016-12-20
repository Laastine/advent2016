module seven

open System
open System.IO
open System.Text.RegularExpressions

let stripBrackets(inputList: string) =
  Regex.Split(inputList, "\[A-Za-z\]") |> Array.toList |> List.rev |> String.Concat

let readInputData: List<string> =
  System.IO.File.ReadLines("./input.txt")
    |> Seq.toList
    |> List.map stripBrackets

[<EntryPoint>]
let main argv =
    printfn "%A" argv
    0 // return an integer exit code
