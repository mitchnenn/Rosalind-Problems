// Learn more about F# at http://fsharp.org

open FSharp.Data
open RosalindLib    

type RnaCodon = CsvProvider<"TestData/rna-codon-table.csv">

let freq (rnaCodons:CsvProvider<"TestData/rna-codon-table.csv">) (aa:string) =
    rnaCodons.Rows
    |> Seq.map (fun rc -> rc.AminoAcid)
    |> Seq.countBy (fun i -> i = aa)
    |> Seq.sortDescending
    |> Seq.head
    |> snd
    |> bigint
   
let getStopFrequency rnaCodons = freq rnaCodons "Stop"

[<EntryPoint>]
let main argv =
    let rnaCodons = RnaCodon.Load("TestData/rna-codon-table.csv")
    let sequences = FileUtilities.readLines "TestData/sequences.txt"
    let mutable possible = getStopFrequency rnaCodons
    for sequence in sequences do
        let aminoAcids = sequence.ToCharArray()
        for aa in aminoAcids do
            possible <- (possible * (freq rnaCodons (aa.ToString())))
    possible <- possible % bigint(1000000)
    printfn "%A" possible
        
    0 // return an integer exit code
