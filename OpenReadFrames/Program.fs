// Learn more about F# at http://fsharp.org

open System.Collections.Generic
open FSharp.Data
open RosalindLib

type DnaCodon = CsvProvider<"TestData/dna-codon-table.csv">

let getDnaCodons = DnaCodon.Load("TestData/dna-codon-table.csv").Rows
                |> Seq.map(fun x -> x.DnaCodon, x.AminoAcid)
                |> dict

let getAminoAcid dnaCodon (dnaCodons:IDictionary<string,string>) =
    dnaCodons.Item dnaCodon

[<EntryPoint>]
let main argv =
    let dnaCodons = getDnaCodons
    let dnaSequences = ParseFasta.parseFastaEntries "TestData/dna-sequence.fasta"
    
    let aminoAcids = dnaSequences
                     |> Seq.map (fun (k,s) -> k, StringUtilities.chunkString 3 s)
                     |> Seq.map (fun (k,a) -> k, a |> List.map(fun s -> getAminoAcid s dnaCodons))
                     |> Seq.map (fun (k,a) -> k, a |> String.concat "")
                     |> Seq.map (fun (k,s) -> k, s.Split "Stop")
    printfn "%A" aminoAcids
        
    0 // return an integer exit code
