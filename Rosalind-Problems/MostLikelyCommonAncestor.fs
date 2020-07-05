namespace Rosalind_Problems

open RosalindLib
open System
open Rosalind_Problems.Helpers
open Xunit
open Xunit.Abstractions

module MostLikelyCommonAncestor =
    
    let createConsensusMatrix (dnaStrings:string []) = 
        let rec loop index (accum:char[,]) =
            let branch = index < dnaStrings.Length
            match branch with
            | false -> accum
            | true ->
                let nextIndex = index + 1
                let charArray = dnaStrings.[index].ToCharArray()
                for i in [0..(charArray.Length-1)] do
                    accum.[index, i] <- charArray.[i]
                loop nextIndex accum 
        loop 0 (Array2D.zeroCreate<char> dnaStrings.Length dnaStrings.[0].Length)
    
    let getConsensusFastaAsMatrix (path:string) = 
        FileUtilities.readLines path
            |> ReadFasta.getSequences
            |> Seq.map (fun (_,v) -> v)
            |> Seq.toArray
            |> createConsensusMatrix 
    
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
            let matrix = getConsensusFastaAsMatrix "TestData/MostLikelyCommonAncestor.txt"
            let consensus = String.Join("", (getConsensus matrix))
            printfn "%s" consensus
            Assert.Equal("GAAGACATATGCATGAATCAGCGGTACGAAAACATAACCCGATGTAGTTAACTTAACTATACGGCTAATGGGGCGTCCCAGACTTTGATGGTATGTCCCGAACAGAACGCTCACGTGTGTGAACGCCCTATTCTTGGTCATGTCAGAGCTTTGATCGAACGGATCTAGGTTGGGATGTGAGAGTTAGCCTGGTGGTGAACAGCTTTACGCCACAGTTAGTATGGGTGAAACATGAGTAGGTACTTCGGTCGGGCATCACATGTAGATACGACTGTTACTGCTCTCCTACCTGCTATATGGCGCTGCGTATTGCCGTTGCGCTGTCCGGTTAATGCAAACATCGTGTTGAGTGGGAGTAGTAATTCTTGTCAGGGTAATTAGTATAAAGAAAGCATCGTCACCTAGCCAATCAGGCTGGCAATTATCATCTAGGGTACCACAGAGGTGTTTACATCCTAGTCAGTGCCAACAAAAACCCCAGAAAGATAAGTTAGCGGTGACCTGATGAAGACACGCCGTAGATCCCGCACACGTGACGAATCGACTCCCCATTGAGCATCTAGATGTATAGAGAGTGAGAGCGCTGAGCCGGTTAGTAACGCAAGCGTTGAGCATTCCAGTCTACATTCAGATGTAACTGAGTCATCGTTTAGGGGCACTACCCTGACATGCTATAGAACCCGAGTGCCGGTCTTTAACTAACATAGAGATGTGCCCAAAGCCACTATCTGTTGGGTTCTGCCCCAGCCGCACTGCCGCCAAATATCGTCGTAGACCGCAGTGTATCAGCGCGGTGAGAGCTAGGGACCAGCGCTCCTGAACCACAGCTGGTTGGATGAATCCAATCCGATTGTTTTTTTGACTGGCCTAGTCATTATTGGACACAGTGGTCCAACTTCACTGGCGCACATGTCAATGATTGCAGGCGCTTCCACGCTCG", consensus)
            
        [<Fact>]
        member __.``Calculate consunsus counts test`` () =
            let matrix = getConsensusFastaAsMatrix "TestData/MostLikelyCommonAncestor.txt"
            let dnasymbols = ['A';'C';'G';'T']
            for symbol in dnasymbols do
                let consensusCounts = String.Join("", (getConsensusCountBySymbol symbol matrix))
                printfn "%A" consensusCounts
            Assert.True(true);
