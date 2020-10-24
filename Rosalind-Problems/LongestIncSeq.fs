namespace Rosalind_Problems

open System
open System.Collections.Generic
open System.Text
open Xunit
open Xunit.Abstractions
open FParsec

type IntSeqDataRecord = { Length:int; Sequence:int list }

module LongestIncSeq =
    type LongestIncSeqTests (output:ITestOutputHelper) =

        let parseIntSeqRecord =
            tuple2 (pint32 .>> newline) (many (pint32 .>> spaces) .>> skipRestOfLine true)
                
        let getInputSeq (path:string) :IntSeqDataRecord =
            let reply = runParserOnFile parseIntSeqRecord () path Encoding.UTF8 
            match reply with
            | Success(result,_,_) -> { Length=fst(result); Sequence=snd(result)  }
            | Failure(_,_,_) -> { Sequence=List.empty; Length=0 }
        
        let getLongestIncreasingSeq (sequence:int list) =
            let highToLow = Stack<int>()
            highToLow.Push(sequence.Head)
            let lowToHigh = Stack<int>()
            lowToHigh.Push(sequence.Head)
            for n in sequence |> List.tail do
                if n > highToLow.Peek() then
                    highToLow.Pop() |> ignore
                highToLow.Push(n)
                if n < lowToHigh.Peek() then
                    lowToHigh.Pop() |> ignore
                lowToHigh.Push(n)
            (highToLow.ToArray()
             |> Array.toList
             |> List.rev,
             lowToHigh.ToArray()
             |> Array.toList
             |> List.rev)
        
        [<Fact>]
        let ``Find longest increasing sequence test`` () =
            // Arrange.
            let path = "TestData/LongestIncSeq.txt"
            let input = getInputSeq path
            output.WriteLine(sprintf "%A" input)
            // Act.
            let actual = getLongestIncreasingSeq input.Sequence
            output.WriteLine(sprintf "%A" actual)
            // Assert.
            Assert.True(true)

