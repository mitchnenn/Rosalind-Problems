// Learn more about F# at http://fsharp.org

open System.Text.RegularExpressions
open FSharp.Data
open System.Linq
open RosalindLib

let getSequence name = 
    let url = sprintf "https://www.uniprot.org/uniprot/%s.fasta" name
    let filedata = Http.RequestString(url)
    Regex.Replace(filedata.Substring(filedata.IndexOf('\n')), @"\t|\n|\r", "")

let toMatch (aMatch:Match) = aMatch

let getMatches name =
    let data = getSequence name
    let matchColl = Regex.Matches(data, @"(?=(N[^P][ST][^P]))")
    matchColl |> Seq.cast |> Seq.map toMatch 

let toIndex (aMatch:Match) = aMatch.Index + 1

let formatIndexes indexes = indexes |> Seq.iter (printf "%A ")

[<EntryPoint>]
let main argv =
    for name in FileUtilities.readLines "TestData/SampleDataset.txt" do
        let matches = getMatches name
        if matches.Any() then
            let indexes = matches |> Seq.map toIndex
            printfn "%s" name
            formatIndexes indexes
            printfn ""
    0 // return an integer exit code
