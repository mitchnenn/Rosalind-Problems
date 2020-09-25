namespace RosalindLib

open FSharp.Data

module AminoAcid =

    let keywordStop = "Stop"
    let keywordUnknown = "Unknown"
    
    let rnaCodonsPath = "TestData/rna-codon-table.csv"

    type RnaCodon = CsvProvider<"TestData/rna-codon-table.csv">

    let rnaCodonEntries = RnaCodon.Load(rnaCodonsPath)

    let readRnaCodonsPath path = FileUtilities.readLines path 

    let matchRnaCodonToAminoAcid rnaCodon =
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
        | _ -> keywordUnknown    

    let matchRnaToProteins (sequence:string) =
        let result = StringUtilities.chunkString 3 sequence
                     |> List.map (fun dc -> matchRnaCodonToAminoAcid dc)
                     |> String.concat ""
        result.Split(keywordStop) |> Array.filter(fun s -> s <> "")

    let matchDnaCondonToAminoAcid dnaCondon =
        match dnaCondon with
        | "TTT" | "TTC" -> "F"
        | "TTA" | "TTG" | "CTT" | "CTC" | "CTA" | "CTG" -> "L"
        | "TCT" | "TCC" | "TCA" | "TCG" | "AGT" | "AGC" -> "S"
        | "TAT" | "TAC" -> "Y"
        | "TAA" | "TAG" | "TGA" -> keywordStop
        | "TGT" | "TGC" -> "C"
        | "TGG" -> "W"
        | "CCT" | "CCC" | "CCA" | "CCG" -> "P"
        | "CAT" | "CAC" -> "H"
        | "CAA" | "CAG" -> "Q"
        | "CGT" | "CGC" | "CGA" | "CGG" | "AGA" | "AGG" -> "R"
        | "ATT" | "ATC" | "ATA" -> "I"
        | "ATG" -> "M" // Start
        | "ACT" | "ACC" | "ACA" | "ACG" -> "T"
        | "AAT" | "AAC" -> "N"
        | "AAA" | "AAG" -> "K"
        | "GTT" | "GTC" | "GTA" | "GTG" -> "V"
        | "GCT" | "GCC" | "GCA" | "GCG" -> "A"
        | "GAT" | "GAC" -> "D"
        | "GAA" | "GAG" -> "E"
        | "GGT" | "GGC" | "GGA" | "GGG" -> "G"
        | _ -> keywordUnknown


    let matchDnaToProteins (sequence:string) =
        let result = StringUtilities.chunkString 3 sequence
                     |> List.map (fun dc -> matchDnaCondonToAminoAcid dc)
                     |> String.concat ""
        result.Split(keywordStop) |> Array.filter(fun s -> s <> "")
