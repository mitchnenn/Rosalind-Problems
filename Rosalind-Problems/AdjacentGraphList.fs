namespace Rosalind_Problems

open System
open Xunit
open Xunit.Abstractions
open UnitTestHelperLib.Converter
open RosalindLib.ParseFasta
open RosalindLib.GraphOverlapEdges

module AdjacentGraphList =
    
    type AdjacentGraphListTests(output: ITestOutputHelper) =
        do new Converter(output) |> Console.SetOut

        [<Fact>]
        member __.``Parse fasta test`` () =
            let entries = parseFastaEntries "TestData/rosalind.fasta"
            printfn "%A" entries.[0]
            let record = entries.[0]
            Assert.True(record.Id.StartsWith("Rosalind"))
            Assert.True(record.Sequence.StartsWith("GCCGGGTGCCGTGGCAAGAGTAGCGGTTCTCAGACCATA"))
        
        [<Theory>]
        [<InlineData(3)>]
        member __.``Test adjacent graph list`` (k:int) =
            let entries = parseFastaEntries "TestData/AdjacentGraphList.fasta"
            let edges = determineEdges k entries
            for edge in edges do
                let (e1,e2) = edge
                Console.Write("{0} {1}", e1, e2)
            Assert.True(true)