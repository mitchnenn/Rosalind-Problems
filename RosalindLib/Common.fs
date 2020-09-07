namespace RosalindLib

open System.Text

module Common =
    
    let toUpper (x:string) = x.ToUpper()
    
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
    
    let explode (x:string) = [| for c in x -> validDnaNucleobase(c) |]

    let countNucleoBases data =
        toUpper data
        |> explode
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
        for c in x.ToCharArray() do
            yield convertDnaNucleobaseToRna c
    }
        
    let implode (x:char list) =
        let sb = StringBuilder(x.Length)
        x |> List.iter (sb.Append >> ignore)
        sb.ToString()
