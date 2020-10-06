namespace Rosalind_Problems

open System
open System.IO
open Xunit
open Xunit.Abstractions
open RosalindLib.NucleotideBase
open UnitTestHelperLib.Converter

module ConvertDnaToRna = 

    type ConvertDnaToRnaTests(output : ITestOutputHelper) =
        do new Converter(output) |> Console.SetOut
        
        [<Fact>]
        member __.``Convert DNA to RNA test`` () =
            // Arrange.
            let data = File.ReadAllText("TestData/convert_dna_rna.txt")
                        
            // Act.
            let actual = translateDnaToRna data
            
            // Assert.
            printfn "%s" actual
            Assert.Equal("GAUGGAACUUGACUACGUAAAUU", actual)
            