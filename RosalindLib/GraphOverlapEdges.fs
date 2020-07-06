namespace RosalindLib

module GraphOverlapEdges =
    
    let determineEdges (k:int) (dnastrings:(string * string) list) =
        seq {
            for dnastring in dnastrings do
                let (id,sequence) = dnastring
                let suffix = sequence.Substring(sequence.Length - k, k)
                for compareDnaString in dnastrings do
                    let (compareId,compareSeq) = compareDnaString
                    if sequence <> compareSeq then
                        let prefix = compareSeq.Substring(0, k)
                        if suffix = prefix then
                            yield (id,compareId)                
        }

