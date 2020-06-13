namespace RosalindLib

open System

module StringUtilities =
    
    let charCount x = Seq.filter ((=) x) >> Seq.length        

    let strCompareIgnoreCase a1 a2 =
        0 = String.Compare(a1, a2, StringComparison.InvariantCultureIgnoreCase)
    
    let chunkString size str  =
         let rec loop (s:string) accum =
             let branch = size < s.Length
             match branch with
             | true -> loop (s.[size..]) (s.[0..size-1]::accum)
             | false -> s::accum
         (loop str []) |> List.rev
