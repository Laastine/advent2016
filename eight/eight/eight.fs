module eight

open System
open System.IO
open System.Text.RegularExpressions

let lenX = 50
let lenY = 6

let listShiftElements(input: List<'T>, amount: int): List<'T> =
  let rec recur(inputList: List<'T>, amount: int): List<'T> =
    match amount with
    | a when a <= 0 -> inputList
    | a when a > 0 ->
                    let newHead = inputList.[input.Length-1]
                    let newTail = inputList |> List.rev |> List.tail |> List.rev
                    recur(newHead::newTail, amount-1)
    | _ -> failwith "listShiftElements error"
  recur(input, amount)

let row (i: int, arr: 'T[,]) = arr.[i..i, *] |> Seq.cast<'T> |> List.ofSeq

let col (i: int, arr: 'T[,]) = arr.[*, i..i] |> Seq.cast<'T> |> List.ofSeq

let handleRotate(input: List<string>, array2d: char[,]): char[,] =
  let orientation = input.[1]
  let (_, index) = System.Int32.TryParse(Regex.Split(input.[2], "=").[1])
  let (_, amount) = System.Int32.TryParse(input.[input.Length-1])
  if orientation = "row" then
                                      let row = row(index, array2d)
                                      let shifted = listShiftElements(row, amount)
                                      for x in 0..(lenX-1) do
                                        array2d.[index,x] <- shifted.[x]
                                      array2d
  else if orientation = "column" then
                                      let column = col(index, array2d)
                                      let shifted = listShiftElements(column, amount)
                                      for y in 0..(lenY-1) do
                                        array2d.[y,index] <- shifted.[y]
                                      array2d
  else failwith "handleRotate error"

let handleRect(input: List<string>, array2d: char[,]): char[,] =
  let coords = Regex.Split(input.[1], "x")
                                    |> Array.map(fun x ->
                                     let (_, value) = System.Int32.TryParse(x)
                                     value)
  array2d |> Array2D.mapi(fun x y z ->
                  if x < (coords.[1]) && y < (coords.[0]) then '#'
                  else z)

let selectCmd(input: List<string>, resList: char[,]): char[,] =
  if input.Head = "rotate" then handleRotate(input, resList)
  else if input.Head = "rect" then handleRect(input, resList)
  else failwith "selectCmd error"

let tokenizeString(x: string): List<string> = Regex.Split(x, " ") |> List.ofArray

let readInputData =
  let data =
    System.IO.File.ReadLines("./eight.txt")
      |> Seq.toList
      |> List.map tokenizeString
  let rec recur(input: List<List<string>>, acc: char[,]): char[,] =
     match input with
     | [] -> acc
     | head::tail -> recur(tail, selectCmd(head, acc))
  recur(data, Array2D.init lenY lenX (fun x y -> '.'))

let calcHashes(input: char[,]): int =
  input |> Seq.cast<char> |> Seq.filter (fun x -> x = '#') |> Seq.length

[<EntryPoint>]
let main argv =
  printfn "%270A" readInputData
  printfn "Length: %A" (calcHashes(readInputData))
  0 // return an integer exit code
