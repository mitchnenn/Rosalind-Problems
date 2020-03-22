namespace RosalindLib

module CGContent =

    open StringUtilities
    open MathUtilities
    
    let calculateGcContentRatio (seqOfGC:string) =
        let c_count = seqOfGC |> charCount 'C'
        let g_count = seqOfGC |> charCount 'G'
        ((float)(c_count + g_count) / (float)(seqOfGC |> Seq.length))
        
    let calculateGcContentPercentage (seqOfGC:string) =
        calculateGcContentRatio seqOfGC * 100.0
        
    let calcGcContentFromFastaSeq (sequences:seq<string * string>) =
        seq {
            for s in sequences do
                let (header,sequence) = s
                let roundDigits = 6
                let percentCG = calculateGcContentPercentage sequence |> roundFloat roundDigits
                yield (header,sequence,percentCG)
        }
