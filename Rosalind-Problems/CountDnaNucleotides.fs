namespace Rosalind_Problems

open System
open System.IO
open Xunit
open Xunit.Abstractions
open Helpers

module Tests =

    let toUpper (x:string) = x.ToUpper()

    let validNucleobase c =
        match c with
        | ('A'|'C'|'G'|'T') -> c
        | _ -> ' '
    
    let explode (x:string) = [| for c in x -> validNucleobase(c) |]
    
    let getNucleobase (nb,_) = nb 

    let countNucleoBases data =
        toUpper data
        |> explode
        |> Seq.countBy id
        |> Seq.sortBy(fun x -> getNucleobase(x))
        |> Seq.toList

    let getCount (_,c) = c
        
    type RosalindTests(output : ITestOutputHelper) =
        do new Converter(output) |> Console.SetOut

        [<Fact>]
        member __.``Count occurrences of nucleobases`` () =
            // Arrange.
            let workingDir = Path.Combine(Directory.GetCurrentDirectory(), "TestData")
            let inputFile = Path.Combine(workingDir, "rosalind_dna.txt")
            let data = File.ReadAllText(inputFile).Trim()
            
            // Act.
            let results = countNucleoBases data
            
            // Assert
            let output = results |> List.map(fun x -> getCount(x).ToString() + " ") |> String.Concat
            let outputFile = Path.Combine(workingDir, "rosalind_out.txt")
            if File.Exists(outputFile) then File.Delete(outputFile)
            File.WriteAllText(outputFile, output)
            Assert.Equal(4, results.Length)
            for t in results do
                match getNucleobase(t) with
                | 'A' -> Assert.Equal(20, getCount(t))
                | 'C' -> Assert.Equal(12, getCount(t))
                | 'G' -> Assert.Equal(17, getCount(t))
                | 'T' -> Assert.Equal(21, getCount(t))
                | _ -> Assert.False(true)
        