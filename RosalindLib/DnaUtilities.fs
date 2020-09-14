namespace RosalindLib

open StringUtilities
open NucleotideBase

module DnaUtilities =

    let complimentDna dna =
        toUpper dna
        |> explodeValidDnaNucleotideBase
        |> Seq.map(fun b -> complimentNucleotideBase(b))
        |> Seq.toList
        |> implode
        
    let reverseDna dna = 
        toUpper dna
        |> explodeValidDnaNucleotideBase
        |> Seq.rev
        |> Seq.toList
        |> implode
        
    let complimentReverseDna dna =
        toUpper dna
        |> explodeValidDnaNucleotideBase
        |> Seq.map(fun b -> complimentNucleotideBase(b))
        |> Seq.rev
        |> Seq.toList
        |> implode
