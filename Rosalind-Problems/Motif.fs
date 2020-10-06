namespace Rosalind_Problems

open System
open UnitTestHelperLib.Converter
open Xunit
open Xunit.Abstractions

module FindMotif =

    let chunkIntoPatternSize (input:string) (pattern:string) =
        let patternSize = pattern.Length
        let rec loop (partial:string) (accum:string list) =
            let branch = partial.Length < patternSize
            match branch with
            | true -> accum |> List.rev
            | false ->
                let newPartial = partial.Substring(1)
                let inputChunk = partial.Substring(0,patternSize) 
                loop newPartial (inputChunk::accum)
        loop input []
    
    let findPatternIndexes (input:string) (pattern:string) = 
        let chunkByPatternSize = chunkIntoPatternSize input pattern
        let rec loop index accum =
            let branch = index < chunkByPatternSize.Length
            match branch with
            | false -> accum |> List.rev
            | true ->
                let nextIndex = index + 1
                if chunkByPatternSize.[index] = pattern then
                    loop nextIndex (index::accum)
                else
                    loop nextIndex accum
        loop 0 []
    
    type FindMotifTests(output: ITestOutputHelper) =
        do new Converter(output) |> Console.SetOut
        
        [<Fact>]
        member __.``Find Motif Test`` () =
            let s = "GATATATGCATATACTT"
            let t = "ATAT"
            let result = findPatternIndexes s t |> List.map(fun i -> i + 1)
            let resultString = String.Join(" ", result)
            printfn "%s" resultString
            Assert.Equal([|2;4;10|], result)