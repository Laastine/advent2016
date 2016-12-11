module six

let explode (s:string) = [for c in s -> c]

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

let parseInput =
  System.IO.File.ReadLines("./input2.txt")
  |> Seq.toList
  |> List.map addCharToList

[<EntryPoint>]
let main argv =
    printfn "%A" parseInput
    0 // return an integer exit code
