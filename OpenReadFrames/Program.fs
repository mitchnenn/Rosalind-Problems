open System.Text.RegularExpressions
open FSharp.Data
open RosalindLib
open DnaUtilities
open AminoAcid

type DnaCodon = CsvProvider<"TestData/dna-codon-table.csv">

let findProteinStrings (dnaSeq:string) = seq {
    let pattern = @"(?=(ATG(?:...)*?)(?=TAA|TGA|TAG))"
    let regex = Regex(pattern, RegexOptions.Compiled)
    let matches = regex.Matches(dnaSeq)
    for m in matches do
        for g in m.Groups do
            for c in g.Captures do
                if System.String.IsNullOrEmpty(c.Value) <> true then
                    yield c.Value
}

[<EntryPoint>]
let main argv =
    let dnaCodons = DnaCodon.Load("TestData/dna-codon-table.csv")
    for d in dnaCodons.Rows do
        let m = matchDnaCondonToAminoAcid d.DnaCodon
        if m <> d.AminoAcid then
            printfn "Bad match %s" d.DnaCodon
    
    let proteins = FileUtilities.readLines "TestData/dna-sequence.fasta"
                   |> ReadFasta.getSequences
                   |> Seq.map (fun (k,s) -> k, s, complimentReverseDna s)
                   |> Seq.map (fun (k,f,r) -> k, findProteinStrings f, findProteinStrings r)
                   |> Seq.map (fun (k,f,r) -> k, f |> Seq.append r)
                   |> Seq.map (fun (k,a) -> k, a |> Seq.distinct)
    for (_,a) in proteins do
        for p in a do
            printfn "%s" (matchDnaToProteins p |> String.concat "")
            
    0 // return an integer exit code
