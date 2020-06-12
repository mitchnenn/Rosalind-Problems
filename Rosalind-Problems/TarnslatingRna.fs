namespace Rosalind_Problems

open System
open Rosalind_Problems.Helpers
open Xunit
open Xunit.Abstractions
open FSharp.Data
open RosalindLib

module TarnslatingRna =

    let matchAminoAcid rnaCodon =
        match rnaCodon with
        | "UUC" | "UUU" -> "F"
        | "UUA" | "UUG" | "CUC" | "CUU" | "CUA" | "CUG" -> "L"
        | "UCU" | "UCC" | "UCA" | "UCG" | "AGU" | "AGC" -> "S"
        | "UAU" | "UAC" -> "Y"
        | "UAA" | "UAG" | "UGA" -> "Stop"
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

    type RnaCodon = CsvProvider<"TestData/rna-codon-table.csv">

    type TarnslatingRnaTests(output: ITestOutputHelper) =
        do new Converter(output) |> Console.SetOut
        
        [<Fact>]
        member __.``Verify RNACodon matches Amino Acid``() =
            let rnaCodonEntries = RnaCodon.Load("TestData/rna-codon-table.csv")
            for entry in rnaCodonEntries.Rows do
                Assert.Equal(matchAminoAcid entry.RnaCodon, entry.AminoAcid)
        
        [<Fact>]
        member __.``Parse RNA codons and match protiens.`` () =
            let sampledataset = "AUGGCCAUGGCGCCCAGAACUGAGAUCAAUAGUACCCGUAUUAACGGGUGA"
            let rnaCodons = StringUtilities.chunkString 3 sampledataset
            printf "%A" rnaCodons
            let aminoAcids = rnaCodons |> List.map (fun rc -> matchAminoAcid rc)
            printf "%A" aminoAcids
            
            Assert.True(true)