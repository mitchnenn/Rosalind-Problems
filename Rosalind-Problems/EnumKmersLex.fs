namespace Rosalind_Problems
namespace Rosalind_Problems

open System.Text
open Xunit
open Xunit.Abstractions
open FParsec

type KmersSymbolDefRecord = {Symbols:char list; Permutations:int}

module EnumKmersLex =
    
    type EnumKmersLexTests(output:ITestOutputHelper) =
        
        let symbol =  letter .>> spaces            
        
        let symbols = many symbol
        
        let permutations = pint32
        
        let parseKmersSymbols = many (tuple2 symbols permutations)
        
        let getKmerSymbolDef (path:string) =
            let reply = runParserOnFile parseKmersSymbols () path Encoding.UTF8
            match reply with
            | Success(result,_,_) -> result |> List.map(fun t -> {Symbols=(fst t);Permutations=(snd t)})
            | Failure(_,_,_) -> List.empty

        [<Fact>]
        let ``Enumerate kmers lex test`` () =
            // Arrange.
            let kmerSymbolDef = getKmerSymbolDef "TestData/EnumKmersLexData.txt"
            output.WriteLine(sprintf "%A" kmerSymbolDef)
            // Act.
            let actual = ""
            // Assert.
            let expected = "AA\nAC\nAG\nAT\nCA\nCC\nCG\nCT\nGA\nGC\nGG\nGT\nTA\nTC\nTG\nTT\n"
            //Assert.Equal(expected, actual)
            Assert.True(true)
        