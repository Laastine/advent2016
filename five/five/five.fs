module five

open System.Security.Cryptography
open System.Text
open System
open System.IO

let inputPassword = "wtnhxymk"
let inputPassword2 = "abc"

let explode (s:string): List<char> = [for c in s -> c]

let charToString(input: char): string =
    string (List.fold (fun (sb:StringBuilder) (c:char) -> sb.Append(c)) (new StringBuilder()) [input])

let md5hash(input: string): string =
  use md5 = MD5.Create()
  input
  |> Encoding.ASCII.GetBytes
  |> md5.ComputeHash
  |> Seq.map (fun x -> x.ToString("X2"))
  |> Seq.reduce (+)

let generatePostfix(input: string, init: int): string =
  let rec recur(input: string, init: int, acc: string) =
    match acc with
    | a when String.length a = 8 -> acc
    | a when String.length a < 8 ->
            let inputHash = sprintf "%s%d" input init
            let hash = md5hash(inputHash)
            let hashPrefix = hash.[0..4]
            if hashPrefix = "00000" then
                          printfn "FOUND: %A" hash
                          let newLetter = hash.[5] |> charToString
                          let output =  sprintf "%s%s" acc newLetter
                          printfn "output: %s" output
                          recur(input, init+1, output)
            else recur(input, init+1, a)
    | _ -> failwith "Urho Matti"
  recur(input, init, "")

let generatePostfixWithIndex(input: string, init: int): string =
  let rec recur(input: string, init: int, acc: string) =
    match acc with
    | a when a.Contains("_") ->
            let inputHash = sprintf "%s%d" input init
            let hash = md5hash(inputHash)
            let hashPrefix = hash.[0..4]
            if hashPrefix = "00000" then
                          let (isSuccess, newLetterIndex) = System.Int32.TryParse((hash.[5] |> charToString))
                          let newLetter = hash.[6]
                          let inputList = acc |> explode |> List.toArray
                          if newLetterIndex < 8 && isSuccess && inputList.[newLetterIndex] = '_' then
                            (inputList.[newLetterIndex] <- newLetter)
                            let output = inputList |> Array.toList |> String.Concat
                            printfn "Output: %s" output
                            recur(input, init+1, output)
                          else
                            recur(input, init+1, acc)
            else recur(input, init+1, acc)
    | a when a.Length = 8 -> acc
    | _ -> failwith "Urho Matti"
  recur(input, init, "________")

[<EntryPoint>]
let main argv =
    let foo = generatePostfix(inputPassword, 1000000)
    printfn "generatePostfix %A" foo
    let bar = generatePostfixWithIndex(inputPassword, 1000000)
    printfn "generatePostfixWithIndex %A" bar
    0 // return an integer exit code
