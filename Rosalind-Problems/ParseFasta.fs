namespace Rosalind_Problems

open System.Linq
open System.IO
open Xunit

module ParseFasta =
    
    let readLines (path:string) =
        seq {
            use sr = new StreamReader(path)
            while not sr.EndOfStream do
                yield sr.ReadLine()
        }

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
    
    [<Fact>]
    let parseFastaTest () =
        let workingDir = Path.Combine(Directory.GetCurrentDirectory(), "TestData")
        let inputFastaFile = Path.Combine(workingDir, "rosalind.fasta")
        let lines = readLines inputFastaFile
            
        let recordLines = getSequences lines
        
        Assert.True(recordLines.Count() > 0);
        