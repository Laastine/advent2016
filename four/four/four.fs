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

let fromListStart(inputList: List<char*int>): List<char*int> =
  let desiredIndex = if inputList.Length >= 4 then 4 else inputList.Length - 1
  let (_, minOcc) = inputList.Item desiredIndex
  let orderedList =
    inputList
      |> List.filter (fun (_,elements) -> elements >= minOcc)
      |> List.toSeq
      |> Seq.take (desiredIndex+1)
      |> Seq.toList
  orderedList

let areListIdentical(a: List<char>, b: List<char>): bool =
  let setA = set a
  let setB = set b
  let diff = setB - setA |> Set.toArray
  Array.isEmpty diff

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

let isValidChecksum(inputList: List<char*int>, checksum: string): bool =
  let charList = explode checksum
  let charsOfInputList = List.map (fun x ->
                                          let (chars, _) = x
                                          chars) inputList
  areListIdentical(charsOfInputList, charList)

let shift(asciiCode: int, shiftCount: int) =
  let num = int(asciiCode) - int('a')
  let offsetNum = (num + shiftCount) % 26
  let res = offsetNum + int('a')
  if int(asciiCode) = 45 then
    ' '
  elif offsetNum < 0 then
    char(int('z') + offsetNum + 1)
  else
    char(offsetNum + int('a'))

let shiftCharsForward(inputString: string, shiftCount: int): string =
  inputString.ToCharArray()
    |> Array.map (fun x -> shift((int x), shiftCount))
    |> String.Concat

let readInputData =
  let parsedInputData =
    System.IO.File.ReadLines("./input.txt")
    |> Seq.toList
    |> List.map (fun x ->
                          let (inputString, code, checksum) = parseLine(x)
                          (inputString, code, checksum))
  parsedInputData
    |> List.map (fun x ->
                        let (inputString, code, checksum) = x
                        let fiveMostCommonChars = addCharToList(inputString)
                                                  |> List.sortBy (fun (x,y) -> (-y,x))
                                                  |> fromListStart
                        let isValid = isValidChecksum(fiveMostCommonChars, checksum)
                        if isValid then code else 0)
    |> List.sum

let nortPole =
  let parsedInputData =
    System.IO.File.ReadLines("./input.txt")
    |> Seq.toList
    |> List.map (fun x ->
                          let (inputString, code, checksum) = parseLine(x)
                          (inputString, code, checksum))
  parsedInputData
    |> List.map (fun x ->
                        let (inputString, code, checksum) = x
                        (shiftCharsForward(inputString, code), code))
    |> List.filter (fun (encodedString, code) -> encodedString.Contains("north"))

[<EntryPoint>]
let main argv =
    printfn "%A" nortPole
    0 // return an integer exit code
