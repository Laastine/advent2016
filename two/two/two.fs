module two

open System.Net
open System
open System.IO

// let numpadRoute = "ULLR\nRRDDD\nLURDLL\nUUUUD"  //2985

// let numpadRoute = "ULL\nRRDDD\nLURDL\nUUUUD"  //1985
let numpadRoute = "DLUUULUDLRDDLLLUDULLULLRUURURLUULDUUUDLDDRUDLUULLRLDDURURDDRDRDLDURRURDLDUURULDDULDRDDLDLDLRDRUURLDLUDDDURULRLLLLRLULLUDRDLDUURDURULULULRLULLLULURLRDRDDDDDDDLRLULUULLULURLLDLRLUDULLDLLURUDDLDULDLULDDRLRLRDDLRURLLLURRLDURRDLLUUUUDRURUULRLDRRULLRUDLDRLUDRDRDRRDDURURRDRDRUDURDLUDRUDLRRULDLRDDRURDDUUDLDRDULDDRRURLLULRDRURLRLDLLLUULUUDLUDLDRRRRDUURULDUDUDRLDLLULLLRDDDDDLRDDLLUULLRRRDURLRURDURURLUDRRLRURDRDRRRRULUDLDRDULULRUDULLLUDRRLRLURDDURULDUUDULLURUULRDRDULRUUUDURURDDRRUDURRLRDRULRUUU\nLDRURRUUUULDRDDDLLULDRUDDRLLDLDRDLRUDDDLDDULULULLRULDUDRRDLRUURURDRURURDLLRUURDUUDRLDURDRDLRRURURDUUUURUURRLLLDRDUURRRRURULUUUDLUDDRUURRLDULRDULRRRRUDURRLURULRURRDRDLLDRRDUDRDURLDDRURULDRURUDDURDLLLUURRLDRULLURDRDRLDRRURRLRRRDDDDLUDLUDLLDURDURRDUDDLUDLRULRRRDRDDLUDRDURDRDDUURDULRRULDLDLLUDRDDUDUULUDURDRLDURLRRDLDDLURUDRLDUURLLRLUDLLRLDDUDLLLRRRLDLUULLUDRUUDRLDUUUDUURLRDDDDRRDRLDDRDLUDRULDDDRDUULLUUUUULDULRLLLRLLDULRDUDDRDDLRRLRDDULLDURRRURDDUDUDDRLURRLUUUULLDRDULUUDRDULDLLUDLURDLLURRDLUULURRULRLURRRRRUURDDURLRLLDDLRRDUUURDRDUDRDDDLLDDRDRRRLURRDUULULULULRRURDDLDDLLLRUDDDDDDLLLRDULURULLRLRDRR\nDDRLLLDLRRURRDLDDRUURRURRLRRRRUURUURDLURRRDDLRUDRURLUURLLRRLRLURLURURDULLLLDLRURULUUDURRLULRDRDRRDDLLULRLUDLUUUDRLLRRURRLDULDDLRRLUUUUDDLRLDRLRRDRDLDDURDDRDDLDLURLRRRDDUDLLRLRLURRRRULLULLLLDRLDULDLLDULRLDRDLDDRRDDDDRUDRLLURULRLDDLLRRURURDDRLLLULLULDDRDLDDDLRLLDRLDRUURRULURDDRLULLDUURRULURUUDULLRUDDRRLLDLLRDRUDDDDLLLDDDLLUUUULLDUUURULRUUDUUUDDLDURLDRDRRLLUDULDLUDRLLLDRRRULUUDDURUDRLUDDRRLLDUDUURDDRURLUURDURURURRUUDUDDLLLDRRRURURRURDLRULLDUDRLRLLRUDRUDLR\nRRRDRLRURLRRLUURDRLDUURURLRDRRUDLLUUDURULLUURDLLDRRLURRUDUUDRRURLRRDULLDDLRRRUDUUDUUDLDDDLUUDLDULDDULLDUUUUDDUUDUDULLDDURRDLRRUDUDLRDUULDULRURRRLDLLURUDLDDDRRLRDURDLRRLLLRUDLUDRLLLRLLRRURUDLUDURLDRLRUDLRUULDRULLRLDRDRRLDDDURRRUDDDUDRRDRLDDRDRLLRLLRDLRDUDURURRLLULRDRLRDDRUULRDDRLULDLULURDLRUDRRDDDLDULULRDDRUDRLRDDRLDRDDRRRDUURDRLLDDUULRLLLULLDRDUDRRLUUURLDULUUURULLRLUDLDDLRRDLLRDDLRDRUUDURDDLLLDUUULUUDLULDUDULDRLRUDDURLDDRRRDLURRLLRRRUDDLDDRURDUULRUURDRRURURRRUUDUDULUDLUDLLLUUUULRLLRRRRDUDRRDRUDURLUDDLDRDLDDRULLRRULDURUL\nDLLLRDDURDULRRLULURRDULDLUDLURDDURRLLRRLLULRDLDRDULRLLRDRUUULURRRLLRLDDDRDRRULDRRLLLLDLUULRRRURDDRULLULDDDLULRLRRRUDRURULUDDRULDUDRLDRRLURULRUULLLRUURDURLLULUURUULUUDLUDLRRULLLRRLRURDRRURDRULRURRUDUDDDRDDULDLURUDRDURLDLDLUDURLLRUULLURLDDDURDULRLUUUDLLRRLLUURRDUUDUUDUURURDRRRRRRRRRUDULDLULURUDUURDDULDUDDRDDRDRLRUUUUDLDLRDUURRLRUUDDDDURLRRULURDUUDLUUDUUURUUDRURDRDDDDULRLLRURLRLRDDLRUULLULULRRURURDDUULRDRRDRDLRDRRLDUDDULLDRUDDRRRD"

//"    1
//   2 3 4
// 5 6 7 8 9
//   A B C
//     D"

let rec last = function
    | hd ::[] -> hd
    | hd :: tl -> last tl
    | _ -> failwith "Empty list."

let one(direction: char): Char =
  if direction = 'D' then '3'
  else '1'

let two(direction: char): Char =
  if direction = 'R' then '3'
  elif direction = 'D' then '6'
  else '2'

let three(direction: char): Char =
  if direction = 'U' then '1'
  elif direction = 'L' then '2'
  elif direction = 'R' then '4'
  elif direction = 'D' then '7'
  else '3'

let four(direction: char): Char =
  if direction = 'L' then '3'
  elif direction = 'D' then '8'
  else '4'

let five(direction: char): Char =
  if direction = 'R' then '6'
  else '5'

let six(direction: char): Char =
  if direction = 'U' then '2'
  elif direction = 'D' then 'A'
  elif direction = 'L' then '5'
  elif direction = 'R' then '7'
  else '6'

let seven(direction: char): Char =
  if direction = 'U' then '3'
  elif direction = 'D' then 'B'
  elif direction = 'L' then '6'
  elif direction = 'R' then '8'
  else '7'

let eight(direction: char): Char =
  if direction = 'U' then '4'
  elif direction = 'D' then 'C'
  elif direction = 'L' then '7'
  elif direction = 'R' then '9'
  else '8'

let nine(direction: char): Char =
  if direction = 'L' then '8'
  else '9'

let A(direction: char): Char =
  if direction = 'U' then '6'
  elif direction = 'R' then 'B'
  else 'A'

let B(direction: char): Char =
  if direction = 'U' then '7'
  elif direction = 'D' then 'D'
  elif direction = 'L' then 'A'
  elif direction = 'R' then 'C'
  else 'B'

let C(direction: char): Char =
  if direction = 'U' then '8'
  elif direction = 'L' then 'B'
  else 'C'

let D(direction: char): Char =
  if direction = 'U' then 'B'
  else 'D'

let explode (s:string) = [for c in s -> c]

let charToPos(inputChar: Char, direction: Char): Char =
  match inputChar with
    | n when n = '1' -> one(direction)
    | n when n = '2' -> two(direction)
    | n when n = '3' -> three(direction)
    | n when n = '4' -> four(direction)
    | n when n = '5' -> five(direction)
    | n when n = '6' -> six(direction)
    | n when n = '7' -> seven(direction)
    | n when n = '8' -> eight(direction)
    | n when n = '9' -> nine(direction)
    | n when n = 'A' -> A(direction)
    | n when n = 'B' -> B(direction)
    | n when n = 'C' -> C(direction)
    | n when n = 'D' -> D(direction)
    | _ -> failwith "VIHTORI MATTI!"

let handleInputList(input: List<Char>, lastPos: Char) =
  printfn "lastPos %A" lastPos
  let rec recur(listToIterate: List<Char>, acc: List<Char>) =
    match listToIterate with
      | [] -> acc
      | head::tail ->
                  let newPosition = charToPos(acc.Head, head)
                  recur(tail, newPosition::acc)
  recur(input, [lastPos])

let handleStringLists(input: List<string>) =
  let rec recur(inputList: List<string>, acc: List<Char>) =
    // printfn "ACC %A" acc
    match inputList with
      | [] -> acc
      | head::tail ->
                    let tokenizedList = explode(head)
                    printfn "tokenizedList %A" tokenizedList
                    let endNumber = (handleInputList(tokenizedList, acc.Head))
                    printfn "endNumber %A" endNumber
                    recur(tail, (endNumber.Head)::acc)
  recur(input, ['5'])

[<EntryPoint>]
let main argv =
  let listOfListOfNumpadRoutes = numpadRoute.Split([|'\n'|], StringSplitOptions.None) |> Array.toList
  printfn "listOfListOfNumpadRoutes %A" listOfListOfNumpadRoutes
  let res = handleStringLists listOfListOfNumpadRoutes |> List.rev |> List.tail
  printfn "res %A" res
  0 // return an integer exit code
