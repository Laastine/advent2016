module six

let explode (s:string) = [for c in s -> c]

let generateCharOccurenceList(charList: string, charsToBeCounted: List<char>): List<int*int*char>  =
  let rec recur(charsToBeCounted: List<char>, index: int, charList: string, acc: List<int*int*char>): List<int*int*char> =
    match charsToBeCounted with
    | [] -> acc
    | head::tail ->
                    let toBeAdded = (index, 1, head)
                    recur(tail, index+1, charList, toBeAdded::acc)
  recur(charsToBeCounted, 0, charList, [])

let addCharToList(inputList: string): List<int*int*char> =
  let invididualChars = explode(inputList)
  generateCharOccurenceList(inputList, invididualChars)

let areCharAndIndexPresent(index: int, char: char, inputList: List<int*int*char>): bool =
  inputList |> List.exists (fun (x,y,z) -> z = char && x = index)

let increaseOccurence(index: int, charCount: int, char: char, inputList: List<int*int*char>): List<int*int*char> =
  let concatenated =
    inputList
      |> List.filter (fun (x,y,z) -> z = char && x = index)
      |> List.reduce (fun acc next ->
                                          let (x,y,z) = next
                                          let (x1,y1,z1) = acc
                                          (x, y + y1, z))
  let rest = inputList |> List.filter (fun (x,y,z) -> z <> char && x <> index)
  concatenated::rest

let addInputToList(inputList: List<int*int*char>) =
  let rec recur(inputList: List<int*int*char>, acc: List<int*int*char>) =
    match inputList with
    | [] -> acc
    | head::tail ->
                    let (index, charCount, char) = head
                    if areCharAndIndexPresent(index, char, tail) then
                      let oneOccFlattened = increaseOccurence(index, charCount, char, inputList)
                      recur(tail, oneOccFlattened.Head::acc)
                    else recur(tail, acc)
  recur(inputList, [])

let calcOccurencies(inputList: List<int*int*char>) =
  let rec recur(inputList: List<int*int*char>, acc: List<int*int*char>) =
    match inputList with
    | [] -> acc
    | head::tail ->
                    let (index, charCount, char) = head
                    recur(inputList, [])
  recur(inputList, [])

let findMaxForEachIndex(inputList: List<int*int*char>): List<int*int*char> =
  let (wordLength, _, _) = inputList |> List.maxBy (fun (x,y,z) -> x)
  let listOfIndexies = [ for x in 0..wordLength -> x ]
  listOfIndexies
    |> List.map (fun index ->
      inputList
      |> List.filter (fun (x,y,z) -> x = index)
      |> List.maxBy (fun (x,y,z) -> y))

let parseInput =
  System.IO.File.ReadLines("./input.txt")
  |> Seq.toList
  |> List.collect addCharToList
  |> addInputToList
  |> findMaxForEachIndex
  |> List.map(fun (x,y,z) -> z)
  |> Array.ofList
  |> System.String.Concat

[<EntryPoint>]
let main argv =
    printfn "%A" parseInput
    0 // return an integer exit code
