namespace Rosalind_Problems

open System
open System.IO
open Xunit
open Xunit.Abstractions
open RosalindLib.Common
open Helpers

module ConvertDnaToRna = 

    type ConvertDnaToRnaTests(output : ITestOutputHelper) =
        do new Converter(output) |> Console.SetOut
        
        [<Fact>]
        member __.``Convert DNA to RNA test`` () =
            // Arrange.
            let workingDir = Path.Combine(Directory.GetCurrentDirectory(), "TestData")
            let inputFile = Path.Combine(workingDir, "convert_dna_rna.txt")
            let data = File.ReadAllText(inputFile)
                        
            // Act.
            let results = explodeDnaToRna data |> Array.toList
            
            // Assert.
            let actual = implode results
            printfn "%s" actual
            let outputFile = Path.Combine(workingDir, "convert_dna_rna-output.txt") 
            if File.Exists(outputFile) then File.Delete(outputFile)
            File.WriteAllText(outputFile, actual)
            Assert.Equal("GAUGGAACUUGACUACGUAAAUU", actual)
            