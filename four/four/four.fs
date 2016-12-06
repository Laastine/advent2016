module four

open System.Net
open System
open System.IO
open System.Text.RegularExpressions

let explode (s:string) = [for c in s -> c]

let rec last = function
  | hd ::[] -> hd
  | hd :: tl -> last tl
  | _ -> failwith "Empty list."

let countCharFromString(getStr: string, chkdChar: char): int =
  let rec recur(index: int, count: int) =
    if index < getStr.Length then
      if getStr.[index] = chkdChar then recur(index+1, count+1)
      else recur (index+1, count)
    else count
  recur(0, 0)

let generateCharOccurenceList(charList: string, charsToBeCounted: List<char>): List<char*int>  =
  let rec recur(charsToBeCounted: List<char>, charList: string, acc: List<char*int>): List<char*int> =
    match charsToBeCounted with
    | [] -> acc
    | head::tail ->
                    let charCount = countCharFromString(charList, head)
                    let toBeAdded = (head, charCount)
                    recur(tail, charList, toBeAdded::acc)
  recur(charsToBeCounted, charList, [])

let addCharToList(inputList: string): List<char*int> =
  let invididualChars = explode(inputList)
                      |> Set.ofList
                      |> Set.toList
  generateCharOccurenceList(inputList, invididualChars)

let parseLine(inputString: string): string*int*string =
  let tokenizedString = Regex.Split(inputString, "-") |> Seq.toList |> List.rev
  let checksum = Regex.Match(tokenizedString.Head, "[A-Za-z]+").Value
  let (_, value) = System.Int32.TryParse(Regex.Match(tokenizedString.Head, "\d+").Value)
  let characters = tokenizedString.Tail |> List.rev |> String.Concat
  (characters, value, checksum)

let readInputData =
  System.IO.File.ReadLines("./input2.txt")
  |> Seq.toList
  |> List.map parseLine
  |> List.map (fun x ->
                        let (inputString, _, _) = x
                        addCharToList(inputString))

[<EntryPoint>]
let main argv =
    let foo = readInputData
    printfn "%A" foo
    0 // return an integer exit code
