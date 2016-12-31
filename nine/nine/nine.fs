module nine

open System
open System.Text
open System.IO
open System.Text.RegularExpressions

let explode (s: string): List<char> = [for c in s -> c]

let decompressString(inputString: List<char>, ruleString: string, times: int, amount: int): string =
  let tail = inputString.Tail
  let ruleStringLength = ruleString.Length - 1
  // printfn "amount %A, times %A, tail %A, len %d" amount times tail tail.Length
  let newPart = tail.[ruleStringLength..(ruleStringLength-1+amount)] |> String.Concat
  // printfn "newPart %A" newPart
  String.replicate times newPart

let lastMatch(input: string) =
    Regex.Matches(input, "\([0-9x]+\)")
    |> Seq.cast<Match>
    |> Seq.groupBy (fun m -> m.Value)
    |> List.ofSeq
    |> List.last

let decompress(input: string): string =
  let content = explode input
  let rec recur(inputString: List<char>, acc: List<char>): string =
    match inputString with
    | [] -> String.Concat(Array.ofList(acc |> List.rev))
    | head::tail ->
                    if head = '(' then
                                      let ruleString = Regex.Match(String.Concat(Array.ofList(inputString)), "\([0-9]+x[0-9]+\)").Value
                                      // printfn "ruleString %A" ruleString
                                      let rules = Regex.Split(ruleString, "x") |> Array.map(fun x -> Regex.Match(x, "\d+").Value)
                                      // printfn "rules %A" rules
                                      let (_, amount) = System.Int32.TryParse(rules.[0])
                                      let (_, times) = System.Int32.TryParse(rules.[1])
                                      let decompressedPart = decompressString(inputString, ruleString, times, amount)
                                      // printfn "decompressedPart %A" decompressedPart
                                      let newAcc = (explode(decompressedPart) |> List.rev) @ acc
                                      let newTail = tail.[(amount+ruleString.Length-1)..(tail.Length-1)]
                                      // printfn "newAcc %A, newTail %A" newAcc newTail
                                      recur(newTail, newAcc)
                    else recur(tail, head::acc)
  recur(content, [])

let readInputData =
  let data = File.ReadLines("./nine.txt")
              |> Seq.toList
              |> List.map decompress
  printfn "data %A" data
  data |> List.map (fun x -> x.Length) |> List.sum

[<EntryPoint>]
let main argv =
    printfn "%A" readInputData
    0 // return an integer exit code
