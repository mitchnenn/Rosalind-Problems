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
            let data = File.ReadAllText("TestData/convert_dna_rna.txt")
                        
            // Act.
            let results = explodeDnaToRna data |> Seq.toList
            
            // Assert.
            let actual = implode results
            printfn "%s" actual
            Assert.Equal("GAUGGAACUUGACUACGUAAAUU", actual)
            