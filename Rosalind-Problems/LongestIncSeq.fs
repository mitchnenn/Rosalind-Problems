namespace Rosalind_Problems

open System.Text
open Xunit
open Xunit.Abstractions
open FParsec

type IntSeqDataRecord = { Length:int; Sequence:int list }

module LongestIncSeq =
    type LongestIncSeqTests (output:ITestOutputHelper) =

        
        let inc x y = y > x
        
        let dec x y = y < x

        let getSequences f (input:int list) =
            let rec loop condition theRest (acc:int list) =
                match theRest with
                | [] -> List.rev acc
                | head::tail when (condition acc.Head head) -> loop condition tail (head::acc)
                | _::tail -> loop condition tail acc
            loop f input.Tail [input.Head]
        
        [<Fact>]
        let ``Test get sequence`` () =
            // Arrange.
            let list = [2;1;6;5;7;4;3;9]
            // Act.
            let actual = getSequences inc list
            // Assert.
            let expected = [2;6;7;9]
            Assert.Contains(expected, actual)
                
        [<Fact>]
        let ``longest increasing sequence test 2`` () =
            // Arrange.
            let input = [0; 8; 4; 12; 2; 10; 6; 14; 1; 9; 5; 13; 3; 11; 7; 15]
            // Act.
            let actual = getSequences inc input
            // Assert.
            Assert.Contains([0; 8; 12; 14; 15], actual)
            Assert.Contains([0; 4; 12; 14; 15], actual)
            Assert.Contains([0; 12; 14; 15], actual)
        
        let parseIntSeqRecord =
            tuple2 (pint32 .>> newline) (many (pint32 .>> spaces) .>> skipRestOfLine true)
                
        let getInputSeq (path:string) :IntSeqDataRecord =
            let reply = runParserOnFile parseIntSeqRecord () path Encoding.UTF8 
            match reply with
            | Success(result,_,_) -> { Length=fst(result); Sequence=snd(result)  }
            | Failure _ -> { Length=0; Sequence=List.empty }

        [<Fact>]
        let ``Find longest increasing sequence test`` () =
            // Arrange.
            let input = getInputSeq "TestData/LongestIncSeq.txt"
            output.WriteLine(sprintf "%A" input)
            // Act.
//            let increasing = getEverySequence inc input.Sequence
//            output.WriteLine(sprintf "%A" (increasing |> List.head))
//            let decreasing = getEverySequence dec input.Sequence
//            output.WriteLine(sprintf "%A" (decreasing |> List.head))
            // Assert.
            Assert.True(true)

