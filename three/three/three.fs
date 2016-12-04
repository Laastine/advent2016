module three

open System.Net
open System
open System.IO
open System.Text.RegularExpressions

let generateElements(inputLine: List<string>): List<int*int*int> =
  let rec recur(input: List<string>, acc: List<int*int*int>) =
    match input with
    | [] -> acc
    | head::tail ->
                  let foo = Regex.Split(head, " +") |> Array.toList |> List.map(fun x ->
                                  let (_, d) = System.Int32.TryParse((x))
                                  d)
                  recur(tail, (foo.Item(0), foo.Item(1), foo.Item(2))::acc)
  recur(inputLine, [])

let rec remove n lst =
    match lst with
    | h::tl when h = n -> tl
    | h::tl -> h :: (remove n tl)
    | []    -> []

let findTriangles(candidates: List<int*int*int>) =
  candidates
  |> List.filter(fun x ->
                    let (a:int, b:int, c:int) = x
                    let hypotenusa: int = List.max [a;b;c]
                    let legs = remove hypotenusa [a;b;c]
                    let legsLength = legs.Item(0) + legs.Item(1)
                    legsLength > hypotenusa)

let readInputData: List<int*int*int> =
  System.IO.File.ReadLines("./input.txt")
  |> Seq.toList
  |> generateElements
  |> findTriangles

let everyNth(input: List<string>, nth: int) =
  input |> Seq.mapi (fun i el -> el, i)
      |> Seq.filter (fun (el, i) -> i % nth = nth - 1)
      |> Seq.map fst

let generateTriangleList(numbers: List<string>): List<string> =
  let list1 = everyNth(("0"::"0"::numbers), 3)
  let list2 = everyNth(("0"::numbers), 3)
  let list3 = everyNth(numbers, 3)
  [list1; list2; list3] |> Seq.concat |> Seq.toList

let readNumbersInputData =
  let input = System.IO.File.ReadAllText("./input.txt")
  Regex.Split(input, "\s+") |> Array.toList

let generateTuples(lists: List<string>): List<int*int*int> =
  let rec recur(input: List<string>, acc: List<int*int*int>): List<int*int*int> =
    match input with
    | [] -> acc
    | one::two::three::tail ->
                              let (_, a) = System.Int32.TryParse(one)
                              let (_, b) = System.Int32.TryParse(two)
                              let (_, c) = System.Int32.TryParse(three)
                              recur(tail, (a,b,c)::acc)
    | _ -> failwith "URHO MATTI"
  recur(lists, [])

let readColInputData: List<int*int*int> =
  readNumbersInputData
  |> generateTriangleList
  |> generateTuples
  |> findTriangles

[<EntryPoint>]
let main argv =
  let baz = readInputData
  printfn "Triangles %A" baz.Length
  let bar = readColInputData
  printfn "Col triangles %A" bar.Length
  0 // return an integer exit code
