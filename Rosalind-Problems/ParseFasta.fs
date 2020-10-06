namespace Rosalind_Problems

open System
open System.IO
open Xunit
open Xunit.Abstractions
open UnitTestHelperLib.Converter
open RosalindLib.FileUtilities
open RosalindLib.ReadFasta
open RosalindLib.CGContent

module ParseFasta =

    type ParseFastaTests(output : ITestOutputHelper) =
        do new Converter(output) |> Console.SetOut
        
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
                |> calcGcContentFromFastaSeq
                |> Seq.sortBy(fun s ->
                    let (_,_,percent) = s
                    percent)
                |> Seq.last
            writeResults "cgcontent.out" highestCgContent
            // Assert.
            Assert.NotNull(highestCgContent);
        