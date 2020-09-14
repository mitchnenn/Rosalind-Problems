// Learn more about F# at http://fsharp.org

open RosalindLib
open DnaUtilities
open AminoAcid

[<EntryPoint>]
let main argv =
    let aminoAcids = ParseFasta.parseFastaEntries "TestData/dna-sequence.fasta"
                     |> Seq.map (fun (k,s) -> k, s, complimentReverseDna s)
                     |> Seq.map (fun (k,s,rc) -> k, parseToAminoAcid matchDnaCondonToAminoAcid s, parseToAminoAcid matchDnaCondonToAminoAcid rc)
    printfn "%A" aminoAcids
        
    0 // return an integer exit code
