namespace Rosalind_Problems

open System
open Xunit
open Xunit.Abstractions
open Rosalind_Problems.Helpers
open RosalindLib.ParseFasta

module AdjacentGraphList =

    type AdjacentGraphListTests(output: ITestOutputHelper) =
        do new Converter(output) |> Console.SetOut

        [<Fact>]
        member __.``Parse fasta test`` () =
            let entries = parseFastaEntries "TestData/rosalind.fasta"
            printfn "%A" entries.[0]
            let (id, sequence) = entries.[0]
            Assert.True(id.StartsWith("Rosalind"))
            Assert.True(sequence.StartsWith("GCCGGGTGCCGTGGCAAGAGTAGCGGTTCTCAGACCATA"))
            
        [<Fact>]
        member __.``Test adjacent graph list`` () =
            let entries = parseFastaEntries "TestData/AdjacentGraphList.fasta"
            let (id, sequence) = entries.[0]
            printfn "%s" id
            Assert.True(true)