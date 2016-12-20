module seven

open System
open System.IO
open System.Text.RegularExpressions

let explode (s: string): List<char> = [for c in s -> c]

let flatBooleans(abbaList: List<string>): bool =
  abbaList |> Seq.fold (fun acc x -> acc && x.Length = 0) false

let findRepeatedChars(content: List<string>*List<string>): bool =
  let (bracketSectionString, inputString) = content
  let rec recur(input: List<char>, acc: List<string>): List<string> =
    match input with
    | [] -> acc
    | a::b::c::d::tail ->
                          if (a = d && b = c && a <> b) then recur(tail, System.String.Concat(Array.ofList([a,b,c,d]))::acc)
                          else recur(b::c::d::tail, acc)
    | head::tail -> acc

  let isAbba = inputString |> List.collect (fun x -> recur(explode(x), [])) |> List.isEmpty|> not
  let isBracketAbba = bracketSectionString |> List.collect (fun x -> recur(explode(x), [])) |> List.isEmpty

  isAbba && isBracketAbba

let stripBrackets(inputList: string): List<string>*List<string> =
  Regex.Split(inputList, "(\[[a-z]+\])+")
    |> Array.toList
    |> List.fold (fun acc x ->
                          let (fst, snd) = acc
                          if x.Contains("[") then x::fst,snd
                          else fst,x::snd) ([],[])

let readInputData =
  System.IO.File.ReadLines("./input.txt")
    |> Seq.toList
    |> List.map (stripBrackets >> findRepeatedChars)
    |> List.filter id

[<EntryPoint>]
let main argv =
    printfn "%A %A" readInputData readInputData.Length
    0 // return an integer exit code
