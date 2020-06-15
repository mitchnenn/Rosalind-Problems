namespace Rosalind_Problems

open RosalindLib
open System
open Rosalind_Problems.Helpers
open Xunit
open Xunit.Abstractions

module MostLikelyCommonAncestor =
    
    let dnaStrings (fastalines:string seq) = 
        let fastaLinesArray = fastalines |> Seq.toArray
        let rec loop index accum =
            let branch = index < fastaLinesArray.Length
            match branch with
            | false -> accum |> List.rev
            | true ->
                let value = fastaLinesArray.[index+1]
                loop (index+2) (value::accum)
        (loop 0 [])
    
    let createConsensusMatrix (dnaStrings:string []) = 
        let rec loop index (accum:char[,]) =
            let branch = index < dnaStrings.Length
            match branch with
            | false -> accum
            | true ->
                let nextIndex = index + 1
                let charArray = dnaStrings.[index].ToCharArray()
                let rec assign charindex =
                    let isDone = charindex < dnaStrings.[0].Length
                    match isDone with
                    | false -> 0 |> ignore
                    | true ->
                        accum.[index, charindex] <- charArray.[charindex]
                        assign (charindex+1)
                assign 0
                loop nextIndex accum 
        loop 0 (Array2D.zeroCreate<char> dnaStrings.Length dnaStrings.[0].Length)
    
    type MostLikelyCommonAncestorTests(output: ITestOutputHelper) =
        do new Converter(output) |> Console.SetOut
        
        [<Fact>]
        member __.``Most likely common ancestor test`` () =
            let lines = FileUtilities.readLines "TestData/MostLikelyCommonAncestor.txt"
            let dnaStrings = dnaStrings lines |> Seq.toArray
            let matrix = createConsensusMatrix dnaStrings 
            printfn "%A" matrix
            
            Assert.True(true)
