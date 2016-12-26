module eight

open System
open System.IO
open System.Text.RegularExpressions

let lenX = 8
let lenY = 3

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
  printfn "handleRotate %A" input
  let orientation = input.[1]
  let (_, index) = System.Int32.TryParse(Regex.Split(input.[2], "=").[1])
  let (_, amount) = System.Int32.TryParse(input.[input.Length-1])
  // printfn "Translate: orientation: %A, index: %A, amount: %A" orientation index amount
  if orientation = "row" then
                                      let row = row(index, array2d)
                                      printfn "row %A" row
                                      let shifted = listShiftElements(row, amount)
                                      printfn "shifted %A" shifted
                                      for y in 0..(lenX-1) do
                                        array2d.[index,y] <- shifted.[y]
                                      array2d
  else if orientation = "column" then
                                      let column = col(index, array2d)
                                      printfn "column %A" column
                                      let shifted = listShiftElements(column, amount)
                                      printfn "shifted %A" shifted
                                      for x in 0..(lenY-1) do
                                        array2d.[x,index] <- shifted.[x]
                                      array2d
  else failwith "orientation error"

let handleRect(input: List<string>, array2d: char[,]): char[,] =
  let coords = Regex.Split(input.[1], "x")
                                    |> Array.map(fun x ->
                                     let (_, value) = System.Int32.TryParse(x)
                                     value)
  array2d |> Array2D.mapi(fun x y z ->
                  if x < (coords.[1]) && y < (coords.[0]) then '#'
                  else z)

let selectCmd(input: List<string>, resList: char[,]): char[,] =
  // printfn "selectCmd %A" input
  if input.Head = "rotate" then handleRotate(input, resList)
  else if input.Head = "rect" then handleRect(input, resList)
  else failwith "selectCmd error"

let tokenizeString(x: string): List<string> = Regex.Split(x, " ") |> List.ofArray

let readInputData =
  let bar =
    System.IO.File.ReadLines("./eight2.txt")
      |> Seq.toList
      |> List.map tokenizeString
  let rec recur(input: List<List<string>>, acc: char[,]): char[,] =
     match input with
     | [] -> acc
     | head::tail -> recur(tail, selectCmd(head, acc))
  recur(bar, Array2D.init lenY lenX (fun x y -> '.'))

[<EntryPoint>]
let main argv =
  printfn "%A" readInputData
  0 // return an integer exit code
