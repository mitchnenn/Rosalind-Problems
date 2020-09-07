namespace RosalindLib

open FSharp.Data

module AminoAcid =

    let keywordStop = "Stop"
    
    let rnaCodonsPath = "TestData/rna-codon-table.csv"

    type RnaCodon = CsvProvider<"TestData/rna-codon-table.csv">

    let rnaCodonEntries = RnaCodon.Load(rnaCodonsPath)

    let readRnaCodonsPath path = FileUtilities.readLines path 

    let matchAminoAcid rnaCodon =
        match rnaCodon with
        | "UUC" | "UUU" -> "F"
        | "UUA" | "UUG" | "CUC" | "CUU" | "CUA" | "CUG" -> "L"
        | "UCU" | "UCC" | "UCA" | "UCG" | "AGU" | "AGC" -> "S"
        | "UAU" | "UAC" -> "Y"
        | "UAA" | "UAG" | "UGA" -> keywordStop
        | "UGU" | "UGC" -> "C"
        | "UGG" -> "W"
        | "CCU" | "CCC" | "CCA" | "CCG" -> "P"
        | "CAU" | "CAC" -> "H"
        | "CAA" | "CAG" -> "Q"
        | "CGU" | "CGC" | "CGA" | "CGG" | "AGA" | "AGG" -> "R"
        | "AUC" | "AUU" | "AUA" -> "I"
        | "AUG" -> "M"
        | "ACU" | "ACC" | "ACA" | "ACG" -> "T"
        | "AAU" | "AAC" -> "N"
        | "AAA" | "AAG" -> "K"
        | "GUC" | "GUU" | "GUA" | "GUG" -> "V"
        | "GCU" | "GCC" | "GCA" | "GCG" -> "A"
        | "GAU" | "GAC" -> "D"
        | "GAA" | "GAG" -> "E"
        | "GGU" | "GGC" | "GGA" | "GGG" -> "G"
        | _ -> "Unknown"    

    let parseAminoAcids (rnaCodonString:string) =
        (StringUtilities.chunkString 3 rnaCodonString
            |> List.map (fun rc -> matchAminoAcid rc)
            |> String.concat ""
        ).Split(keywordStop)
        |> Array.filter(fun s -> s <> "")

