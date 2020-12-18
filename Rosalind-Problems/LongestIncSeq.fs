namespace Rosalind_Problems

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
            | Failure _ -> { Length=0; Sequence=List.empty }
        
        let inc x y = y > x
        
        let dec x y = y < x
        
        let getSequence f current (rest:int list) =
            List.fold (fun acc elem ->
                       match f (acc |> List.head) elem with 
                       | true -> elem::acc
                       | false -> acc)
                       [current]
                       rest
            |> List.rev
            
        let getAllSequences f (input:int list) =
            let rec loop f list acc =
                match list with
                | [] -> acc    
                | _::tail ->
                    let seqs = getSequence f input.Head tail
                    if List.length seqs > (List.head acc |> List.length) then
                        loop f tail [seqs]
                    else
                        loop f tail acc
            loop f input []
        
        let getEverySequence f (input:int list) =
            let rec loop f list acc =
                match list with
                | [] -> acc |> List.sortBy(List.length) |> List.rev
                | _::tail -> loop f tail (getAllSequences f list @ acc)
            loop f input []

        [<Fact>]
        let ``Test get sequence`` () =
            // Arrange.
            let list = [2;1;6;5;7;4;3;9]
            // Act.
            let actual = getEverySequence inc list
            // Assert.
            let expected = [2;6;7;9]
            Assert.Contains(expected, actual)
                
        [<Fact>]
        let ``longest increasing sequence test 2`` () =
            // Arrange.
            let input = [0; 8; 4; 12; 2; 10; 6; 14; 1; 9; 5; 13; 3; 11; 7; 15]
            // Act.
            let actual = getEverySequence inc input
            // Assert.
            Assert.Contains([0; 8; 12; 14; 15], actual)
            Assert.Contains([0; 4; 12; 14; 15], actual)
            Assert.Contains([0; 12; 14; 15], actual)
        
        [<Fact>]
        let ``Find longest increasing sequence test`` () =
            // Arrange.
            let input = getInputSeq "TestData/LongestIncSeq.txt"
            output.WriteLine(sprintf "%A" input)
            // Act.
            let increasing = getEverySequence inc input.Sequence
            output.WriteLine(sprintf "%A" (increasing |> List.head))
            let increasing = getEverySequence dec input.Sequence
            output.WriteLine(sprintf "%A" (increasing |> List.head))
            // Assert.
            Assert.True(true)

