namespace RnaSplicing

open System.Text
open Xunit
open Xunit.Abstractions
open FParsec
open UnitTestHelperLib.FParsecHelper
open RosalindLib.ParseFasta
open RosalindLib.AminoAcid

module RnaSplicingTests = 
    type PermutationTestsType(output:ITestOutputHelper) =

        let getCodingRegion (dnaSequence:string) (introns:string list) =
            let mutable mrnaString = dnaSequence
            for i in introns do
                mrnaString <- mrnaString.Replace(i, "")
            mrnaString
        
        // Assumes only on result data set will exist for
        // a given input data set.
        [<Fact>]
        let ``My test`` () =
            // Arrange.
            let entries = parseFastaEntries "data/SampleData.fasta"
            let dnaSequence = entries |> List.head |> fun r -> r.Sequence
            let introns = entries |> List.skip 1 |> List.map(fun r -> r.Sequence)
            // Assert.
            let codingRegion = getCodingRegion dnaSequence introns
            let actual = matchDnaToProteins codingRegion |> Seq.head
            output.WriteLine(sprintf "%s" actual)
            // Assert.
            Assert.Equal("MSSPAYRWVRKTKFLHMTYRLSWYRGETFSLGIASCGTSCCLSLALAFRDELVCFSTFVQSGRILPVRLTGGLASLGLSYTGKSAIVGICGANSALPAVCLPAAALSLSSLSSPGDCHTSELWHVKYEEAAVLEAAHARRAIRAMQMLPMMTKSRKYQASWRTDAVKGTQSCT", actual)
                