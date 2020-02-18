namespace Rosalind_Problems

open System
open System.IO
open Xunit
open Xunit.Abstractions
open Helpers

module Tests =

    let toUpper (x:string) = x.ToUpper()

    let explode (x:string) = [| for c in x -> c |]
    
    let getCharacter (c,_) = c 

    let countBases data =
        toUpper data
        |> explode
        |> Seq.countBy id
        |> Seq.sortBy(fun x -> getCharacter(x))
        |> Seq.toList

    let getCount (_,c) = c
        
    type RosalindTests(output : ITestOutputHelper) =
        do new Converter(output) |> Console.SetOut

        [<Fact>]
        member __.``Count occurrences of bases`` () =
            // Arrange.
            let inputFile = "/Users/mnenn/Downloads/rosalind_dna.txt"
            let data = File.ReadAllText(inputFile).Trim()
            
            // Act.
            let results = countBases data
            
            // Assert
            let output = results |> List.map(fun x -> getCount(x).ToString() + " ") |> String.Concat
            let outputFile = "/Users/mnenn/Downloads/rosalind_out.txt"
            if File.Exists(outputFile) then File.Delete(outputFile)
            File.WriteAllText(outputFile, output)
            Assert.Equal(4, results.Length)
