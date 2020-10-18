namespace RnaSplicing

open System
open Xunit
open Xunit.Abstractions
open UnitTestHelperLib.Converter
open RosalindLib.ParseFasta

module RnaSplicingTests = 
    type PermutationTestsType(output : ITestOutputHelper) =
        do new Converter(output) |> Console.SetOut

        let getExons dnaSequence introns = ()
        
        // Assumes only on result data set will exist for
        // a given input data set.
        [<Fact>]
        let ``My test`` () =
            // Arrange.
            let entries = parseFastaEntries "data/SampleData.fasta"
            let dnaSequence = entries |> List.head |> fun r -> r.Sequence
            let introns = entries |> List.skip 1 |> List.map(fun r -> r.Sequence)
            // Assert.
            let exons = getExons dnaSequence introns
            let actual = dnaSequence
            // Assert.
            printfn "actual: %s" actual
            Assert.Equal("MVYIADKQHVASREAYGHMFKVCA", actual)
                