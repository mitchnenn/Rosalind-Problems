namespace RosalindLib

open StringUtilities

module Common =
    
    let getNucleobase (nb,_) = nb 

    let getCount (_,c) = c

    // DNA
    let validDnaNucleobase c =
        match c with
        | ('A'|'C'|'G'|'T') -> c
        | _ -> ' '
    
    let complimentNucleotideBase c =
        match c with
        | 'A' -> 'T'
        | 'T' -> 'A'
        | 'C' -> 'G'
        | 'G' -> 'C'
        | _ -> ' '
    
    let explodeValidDnaNucleotideBase (x:string) = seq { 
        for c in explode x do
            yield validDnaNucleobase c
    }

    let countNucleoBases data =
        toUpper data
        |> explodeValidDnaNucleotideBase
        |> Seq.countBy id
        |> Seq.sortBy(fun x -> getNucleobase(x))
        |> Seq.toList

    // RNA
    let convertDnaNucleobaseToRna c =
        match c with
        | ('A'|'C'|'G') -> c
        | 'T' -> 'U'
        | _ -> ' '
    
    let explodeDnaToRna (x:string) = seq { 
        for c in explode x do
            yield convertDnaNucleobaseToRna c
    }

    let translateDnaToRna (x:string) =
        explodeDnaToRna x
        |> Seq.toList
        |> implode
