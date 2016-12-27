module nine

open System
open System.Text
open System.IO
open System.Text.RegularExpressions

let explode (s: string): List<char> = [for c in s -> c]

let decompress(input: string): string =
  let content = explode input
  let rec recur(inputString: List<char>, acc: List<char>): string =
    match inputString with
    | [] -> String.Concat(Array.ofList(acc |> List.rev))
    | head::tail ->
                    if head = '(' then
                                      let ruleString = Regex.Match(String.Concat(Array.ofList(inputString)), "\([0-9x]+\)").Value
                                      let rules = Regex.Split(ruleString, "x") |> Array.map(fun x -> Regex.Match(x, "\d").Value)
                                      let (_, amount) = System.Int32.TryParse(rules.[0])
                                      let (_, times) = System.Int32.TryParse(rules.[1])
                                      printfn "amount %A, times %A" amount times
                                      let newTail = tail.[(amount+ruleString.Length-1)..(tail.Length-1)]
                                      let newPart = tail.[(ruleString.Length-1)..(ruleString.Length-2+amount)] |> String.Concat
                                      printfn "newPart %A, newTail %A" newPart newTail
                                      let decompressedPart = String.replicate times newPart
                                      let newAcc = (explode(decompressedPart) |> List.rev) @ acc
                                      printfn "decompressedPart %A, newAcc %A" decompressedPart newAcc
                                      recur(newTail, newAcc)
                    else recur(tail, head::acc)
  recur(content, [])

let readInputData =
  let data =
    File.ReadLines("./nine2.txt")
      |> Seq.toList
      |> List.map decompress
  data

[<EntryPoint>]
let main argv =
    printfn "%A" readInputData
    0 // return an integer exit code
