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
  inputList |> List.exists (fun (x,y,z) -> z = char && x = index && y > 0)

let increaseOccurence(index: int, charCount: int, char: char, inputList: List<int*int*char>): int*int*char =
  inputList
    |> List.filter (fun (x,y,z) -> z = char && x = index)
    |> List.fold (fun acc next ->
                                  let (x1,y1,z1) = acc
                                  (x1, y1 + 1, z1)) (index, 0, char)

let addInputToList(inputList: List<int*int*char>) =
  let rec recur(inputList: List<int*int*char>, acc: List<int*int*char>) =
    match inputList with
    | [] -> acc
    | head::tail ->
                    let (index, charCount, char) = head
                    if areCharAndIndexPresent(index, char, tail) then
                      let oneOccFlattened = increaseOccurence(index, charCount, char, inputList)
                      let newTail = tail |> List.filter (fun (x,y,z) -> not (z = char && x = index))
                      recur(newTail, oneOccFlattened::acc)
                    else if not (areCharAndIndexPresent(index, char, tail)) then
                      let newTail = tail |> List.filter (fun (x,y,z) -> not (z = char && x = index))
                      recur(newTail, (index, 1, char)::acc)
                    else failwith "Not here"
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

let findMinForEachIndex(inputList: List<int*int*char>): List<int*int*char> =
  let (wordLength, _, _) = inputList |> List.maxBy (fun (x,y,z) -> x)
  let listOfIndexies = [ for x in 0..wordLength -> x ]
  listOfIndexies
    |> List.map (fun index ->
      inputList
        |> List.filter (fun (x,y,z) -> x = index)
        |> List.minBy (fun (x,y,z) -> y))

let parseInput =
  System.IO.File.ReadLines("./input.txt")
  |> Seq.toList
  |> List.collect addCharToList
  |> addInputToList
  |> findMinForEachIndex
  // |> findMaxForEachIndex
  |> List.map(fun (x,y,z) -> z)
  |> Array.ofList
  |> System.String.Concat

[<EntryPoint>]
let main argv =
    printfn "%A" parseInput
    0 // return an integer exit code
