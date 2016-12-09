module five

open System.Security.Cryptography
open System.Text

let inputPassword = "wtnhxymk"
let inputPassword2 = "abc"

let charToString(input: char) =
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
    // if acc.Length > 2 then printfn "ACC %s" acc
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

[<EntryPoint>]
let main argv =
    let foo = generatePostfix(inputPassword2, 1000000)
    printfn "%A" foo
    0 // return an integer exit code
