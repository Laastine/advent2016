module nine


let readInputData =
  let data =
    System.IO.File.ReadLines("./nine2.txt")
      |> Seq.toList
  data

[<EntryPoint>]
let main argv =
    printfn "%A" readInputData
    0 // return an integer exit code
