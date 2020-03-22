namespace RosalindLib

module HammingDistance =
    
    let calcHammingDistance s t =
        Seq.map2 ((=)) s t
        |> Seq.sumBy (fun b -> if b then 0 else 1)


