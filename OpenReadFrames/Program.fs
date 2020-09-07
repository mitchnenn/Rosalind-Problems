// Learn more about F# at http://fsharp.org

open FSharp.Data
open RosalindLib

type RnaCodon = CsvProvider<"TestData/rna-codon-table.csv">

[<EntryPoint>]
let main argv =
    let rnaCodons = RnaCodon.Load("TestData/rna-codon-table.csv")
    let entries = ParseFasta.parseFastaEntries "TestData/dna-sequence.fasta"
    printfn "%A" entries
    
    
    0 // return an integer exit code
