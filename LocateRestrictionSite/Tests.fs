namespace LocateRestrictionSite

open System
open RosalindLib
open UnitTestHelperLib.Converter
open Xunit
open Xunit.Abstractions
open ParseFasta
open RosalindLib.DnaUtilities

module Tests =
    type PermutationTests(output : ITestOutputHelper) =
        do new Converter(output) |> Console.SetOut

        let isReversePalindrome (input:string) =
            input = (complimentReverseDna input)

        let getSubStringsByLengthRange (substring:string) lengths = seq {
            let dnaStringLen = substring.Length
            for start in [0..dnaStringLen] do
                for length in lengths do
                    if (start + length) <= dnaStringLen then
                        yield (start, length, substring.Substring(start, length))
        }
        
        let findSubstringsWhereTrue (dnaString:string) (lengths:int list) f = seq {
            for s in getSubStringsByLengthRange dnaString lengths do
                let (start, length, substring) = s
                if f substring then
                    yield (start + 1, length)
        }
        
        let printReversePalindromes (dnaString:string) =
            findSubstringsWhereTrue dnaString [4..12] isReversePalindrome
            |> Seq.map (fun (p,l) -> sprintf "%d %d" p l)
            |> Seq.iter(fun s -> printf "%s" s)
        
        [<Fact>]
        let ``My test`` () =
            parseFastaEntries "data/SampleData.fasta"
            |> List.iter (fun (_,dnaString) -> printReversePalindromes dnaString) 

            Assert.True(true)