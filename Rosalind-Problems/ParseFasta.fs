namespace Rosalind_Problems

open System.IO
open System.Text.RegularExpressions
open Xunit
open Common

module ParseFasta =
    
    let readChars (inputFastaFile:string) =
        seq {
            use sr = new StreamReader (inputFastaFile)
            while sr.Peek() >= 0 do
                let c : char = sr.Read() |> char
                yield c
        }

    let parseRecord recordChars =
        let labelPos = recordChars |> Seq.findIndex (fun c -> c = '\n')
        let trim = @">|\t|\n|\r|\s*"
        let label = Regex.Replace(recordChars |> Seq.take labelPos |> Seq.toList |> implode, trim, "")
        let dna = Regex.Replace(recordChars |> Seq.skip labelPos |> Seq.toList |> implode, trim, "")
        (label, dna)
    
    let getRecord chars startPos =
        let skip = if startPos = 0 then 1 else startPos
        let pos = chars |> Seq.skip skip |> Seq.findIndex(fun c -> c = '>')
        let record = chars |> Seq.take(pos) 
        parseRecord record

    let fastaEntries (inputFastaFile:string) =
        let chars = readChars inputFastaFile
        getRecord chars 0
                                
    [<Fact>]
    let parseFastaTest () =
        // Arrange.
        let workingDir = Path.Combine(Directory.GetCurrentDirectory(), "TestData")
        let inputFastaFile = Path.Combine(workingDir, "rosalind.fasta")
        // Act.
        let dnaStrings = fastaEntries inputFastaFile
        // Assert.
        Assert.True(false)
        