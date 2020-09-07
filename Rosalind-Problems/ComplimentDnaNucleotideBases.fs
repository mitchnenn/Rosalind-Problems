namespace Rosalind_Problems

open System
open System.IO
open Helpers
open Xunit
open Xunit.Abstractions
open RosalindLib.StringUtilities
open RosalindLib.NucleotideBase

module ComplimentDnaNucleotideBases =
    
    let complimentDna dna =
        toUpper dna
        |> explodeValidDnaNucleotideBase
        |> Seq.map(fun b -> complimentNucleotideBase(b))
        |> Seq.toList
        |> implode
        
    let reverseDna dna = 
        toUpper dna
        |> explodeValidDnaNucleotideBase
        |> Seq.rev
        |> Seq.toList
        |> implode
        
    let complimentReverseDna dna =
        toUpper dna
        |> explodeValidDnaNucleotideBase
        |> Seq.map(fun b -> complimentNucleotideBase(b))
        |> Seq.rev
        |> Seq.toList
        |> implode
    
    type ComplimentNucleotideBasesTests(output : ITestOutputHelper) =
        do new Converter(output) |> Console.SetOut
        
        [<Fact>]
        member __.``Complement nucleotide base test`` () =
            // Arrange.
            let dna = "GTCA"
            
            // Act.
            let actual = complimentDna dna
            
            // Assert.
            Assert.Equal("CAGT", actual)
        
        [<Fact>]
        member __.``Reverse nucleotide base test`` () =
            // Arrange.
            let dna = "GTCA"
            
            // Act.
            let actual = reverseDna dna
            
            // Assert.
            Assert.Equal("ACTG", actual)
        
        [<Fact>]
        member __.``Reverse compliment test`` () =
            // Arrange.
            let workingDir = Path.Combine(Directory.GetCurrentDirectory(), "TestData")
            let inputFile = Path.Combine(workingDir, "rosalind_compliment.txt")
            let dna = File.ReadAllText(inputFile)

            // Act.
            let actual = complimentReverseDna dna
                        
            // Assert.
            printfn "%s" actual
            let outputFile = Path.Combine(workingDir, "rosalind_compliment_output.txt") 
            if File.Exists(outputFile) then File.Delete(outputFile)
            File.WriteAllText(outputFile, actual)
            
            Assert.Equal("ACCGGGTTTT", actual)