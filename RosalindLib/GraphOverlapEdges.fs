namespace RosalindLib

open RosalindLib.ParseFasta

module GraphOverlapEdges =
    
    let determineEdges (k:int) (dnastrings:FastaRecord list) =
        seq {
            for dnastring in dnastrings do
                let suffix = dnastring.Sequence.Substring(dnastring.Sequence.Length - k, k)
                for compareDnaString in dnastrings do
                    if dnastring.Sequence <> compareDnaString.Sequence then
                        let prefix = compareDnaString.Sequence.Substring(0, k)
                        if suffix = prefix then
                            yield (id,compareDnaString.Id)                
        }

