// Learn more about F# at http://fsharp.org

open System.Text.RegularExpressions
open FSharp.Data

let toMatch (aMatch:Match) = aMatch 

[<EntryPoint>]
let main argv =
    let name = "B5ZC00"
    let url = sprintf "https://www.uniprot.org/uniprot/%s.fasta" name
    let filedata = Http.RequestString(url)
    let data = Regex.Replace(filedata.Substring(filedata.IndexOf('\n')), @"\t|\n|\r", "")
    let matchColl = Regex.Matches(data, @"(?=(N[^P][ST][^P]))")
    let matches = matchColl |> Seq.cast |> Seq.map (fun (r:Match) -> r)
    
                
    0 // return an integer exit code
