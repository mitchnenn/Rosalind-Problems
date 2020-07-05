namespace RosalindLib

open System.Text
open FParsec

module ParseFasta =

    let delimiter = ">"

    let id = pstring delimiter
             >>. many1CharsTill anyChar newline
             
    let sequenceChars = 
        let isSeqChar c = c = 'A' || c = 'C' || c = 'G' || c = 'T'
        many1Satisfy isSeqChar

    let sequence =
        many1Strings (sequenceChars .>> newline)

    let entries = many1 (tuple2 id sequence)

    let parseFastaEntries (path:string) =
        let reply = runParserOnFile entries () path Encoding.UTF8
        match reply with
        | Success(result, _, _) -> result
        | Failure(_,_,_) -> List.empty

    let parseFastaTest =
        let reply = runParserOnFile entries () "TestData/rosalind.fasta" Encoding.UTF8      
        match reply with
        | Success(result, _, _) -> printfn "%A" result; 0
        | Failure(errorMsg, _, _) -> printfn "%s" errorMsg; 1
