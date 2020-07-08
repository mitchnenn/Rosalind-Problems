namespace Rosalind_Problems

open System
open Xunit
open Xunit.Abstractions
open Helpers
open RosalindLib.ParseFasta

module CommonMotif =
 
    let getCandidates (input:string) =
        seq {
            for candidateLen in 2 .. input.Length do
                yield input.Substring(0, candidateLen)
        }
               
    let getAllCandidates (input:string) =
        seq {
            for startPos in 0 .. input.Length - 2 do
                for candidate in  getCandidates (input.Substring(startPos)) do
                    yield candidate
        }
    
    let longestSubString (stringList:seq<string>) (candidates:seq<string>) =
        seq {
            for candidate in candidates do
                let found = stringList |> Seq.forall (fun t -> t.Contains(candidate))
                if found = true then
                    yield candidate
        }
    
    type CommonMotifTests(output: ITestOutputHelper) =
        do new Converter(output) |> Console.SetOut
        
        [<Theory>]
        [<InlineData("TestData/commonmotif.fasta")>]
        member __.``Test common motif search`` path =
            let entries = parseFastaEntries path
                          |> List.map (fun (_,astr) -> astr)
            if entries.IsEmpty <> true then
                let refString = entries.[0]
                let candidates = getAllCandidates refString
                                 |> Seq.sortBy (fun str -> str.Length)
                                 |> Seq.rev
                let result = longestSubString entries candidates |> Seq.tryHead
                printfn "%s" result.Value
                
            Assert.True(true)