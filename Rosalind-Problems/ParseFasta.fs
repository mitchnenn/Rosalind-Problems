namespace Rosalind_Problems

open System
open System.Linq
open System.IO
open Xunit
open Xunit.Abstractions
open Helpers

module ParseFasta =

    type ParseFastaTests(output : ITestOutputHelper) =
        do new Converter(output) |> Console.SetOut

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
            
        let charCount x = Seq.filter ((=) x) >> Seq.length        
            
        let calcCGContent (sequence:string) =
            let c_count = sequence |> charCount 'C'
            let g_count = sequence |> charCount 'G'
            ((float)(c_count + g_count) / (float)(sequence |> Seq.length)) * 100.0
            
        let calcRoundedValue (x:float) = System.Math.Round(x, 6)
        
        let getCGContent (sequences:seq<string * string>) =
            seq {
                for s in sequences do
                    let (header,sequence) = s
                    let percentCG = calcCGContent sequence |> calcRoundedValue
                    yield (header,sequence,percentCG)
            }
        
        let writeResults (path:string) (data:string * string * float) =
            if File.Exists(path) then File.Delete(path)
            use sw = new StreamWriter(path)
            let (header,_,percent) = data
            sw.WriteLine(header)
            sw.WriteLine(sprintf "%.6f" percent)
                
        [<Fact>]
        member __.``Calculate GC percentage from Fasta`` () =
            // Arrange.
            let workingDir = Path.Combine(Directory.GetCurrentDirectory(), "TestData")
            let inputFastaFile = Path.Combine(workingDir, "rosalind.fasta")
            let lines = readLines inputFastaFile
            // Act.    
            let highestCgContent =
                getSequences lines
                |> getCGContent
                |> Seq.sortBy(fun s ->
                    let (_,_,percent) = s
                    percent)
                |> Seq.last
            writeResults "cgcontent.out" highestCgContent
            // Assert.
            Assert.NotNull(highestCgContent);
        