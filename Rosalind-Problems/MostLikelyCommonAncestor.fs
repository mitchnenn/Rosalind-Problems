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
    
    let getColumnConsensus (matrix:char[,]) colIndex =
        matrix.[*,colIndex]
        |> Seq.countBy id
        |> Seq.sortByDescending (fun (_,c) -> c)
        |> Seq.head
        |> fun (i,_) -> i

    let getConsensus (matrix:char[,]) = seq [
        let columns = Array2D.length2 matrix
        for i in 0..(columns-1) do
            yield getColumnConsensus matrix i
    ]
    
    let getColumnConsensusCountBySymbol symbol (matrix:char[,]) colIndex =
        matrix.[*,colIndex]
        |> Seq.countBy id
        |> Seq.sortByDescending (fun (_,c) -> c)
        |> Seq.tryFind (fun (i,_) -> i = symbol)

    let getConsensusCountBySymbol (symbol:char) (matrix:char[,]) = seq [
        yield (sprintf "%c:" symbol)
        let columns = Array2D.length2 matrix
        for i in 0..(columns-1) do
            let count = getColumnConsensusCountBySymbol symbol matrix i
            match count with
            | Some (_,c) -> yield (sprintf " %i" c)
            | None -> yield " 0"
    ]
    
    type MostLikelyCommonAncestorTests(output: ITestOutputHelper) =
        do new Converter(output) |> Console.SetOut
        
        [<Fact>]
        member __.``Calculate consensus test.`` () =
            let lines = FileUtilities.readLines "TestData/MostLikelyCommonAncestor.txt"
            let dnaStrings = dnaStrings lines |> Seq.toArray
            let matrix = createConsensusMatrix dnaStrings 
            let consensus = String.Join("", (getConsensus matrix))
            printfn "%s" consensus
            Assert.Equal("ATGCAACT", consensus)
            
        [<Fact>]
        member __.``Calculate consunsus counts test`` () =
            let lines = FileUtilities.readLines "TestData/MostLikelyCommonAncestor.txt"
            let dnaStrings = dnaStrings lines |> Seq.toArray
            let matrix = createConsensusMatrix dnaStrings 
            let dnasymbols = ['A';'C';'G';'T']
            for symbol in dnasymbols do
                let consensusCounts = String.Join("", (getConsensusCountBySymbol symbol matrix))
                printfn "%A" consensusCounts
            Assert.True(true);
