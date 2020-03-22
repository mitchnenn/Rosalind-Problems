namespace RosalindLib

module ReadFasta =    

    let (|Header|_|) (line:string) =
        if line.[0] = '>' then
            Some <| Header (line.[1..])
        else
            None
            
    let getSequences (lines:string seq) =
        seq {
            let mutable header = ""
            let mutable sequence = ""
            for line in lines do
                match line with
                | l when l.Trim().StartsWith(";") -> ()
                | l when l.Trim() = "" -> ()
                | Header h ->
                    if header <> "" then yield (header,sequence)
                    header <- h
                    sequence <- ""
                | _ -> sequence <- sequence + line
            if header <> "" then yield (header,sequence)
        }
