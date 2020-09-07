// Learn more about F# at http://fsharp.org

open FSharp.Data
open RosalindLib
open NucleotideBase

type RnaCodon = CsvProvider<"TestData/rna-codon-table.csv">

let translateDnaToRna (dnaSequences:((string * string) list)) =
    dnaSequences
    |> List.map (fun s -> (fst(s), translateDnaToRna(snd(s))))

[<EntryPoint>]
let main argv =
    let rnaCodons = RnaCodon.Load("TestData/rna-codon-table.csv")
    let dnaSequences = ParseFasta.parseFastaEntries "TestData/dna-sequence.fasta"
    let rnaStrings = translateDnaToRna dnaSequences
    
    printfn "%A" dnaSequences
    printfn "%A" rnaStrings
    
    0 // return an integer exit code
