namespace Rosalind_Problems

module Common =
    
    let toUpper (x:string) = x.ToUpper()

    let validDnaNucleobase c =
        match c with
        | ('A'|'C'|'G'|'T') -> c
        | _ -> ' '
    
    let explode (x:string) = [| for c in x -> validDnaNucleobase(c) |]
    
    let getNucleobase (nb,_) = nb 

    let countNucleoBases data =
        toUpper data
        |> explode
        |> Seq.countBy id
        |> Seq.sortBy(fun x -> getNucleobase(x))
        |> Seq.toList

    let getCount (_,c) = c
