namespace Rosalind_Problems

open System
open System.IO
open Xunit
open Xunit.Abstractions
open UnitTestHelperLib.Converter
open RosalindLib.HammingDistance

module HammingDistance =
    
    type ParseFastaTests(output : ITestOutputHelper) =
        do new Converter(output) |> Console.SetOut
    
        let readHammingStrings =
            let workingDir = Path.Combine(Directory.GetCurrentDirectory(), "TestData")
            let path = Path.Combine(workingDir, "hamming.txt")
            use sr = new StreamReader(path)
            (sr.ReadLine(),sr.ReadLine())
        
        [<Fact>]
        member __.``Calculate Hamming distance`` () =
            // Arrange.
            let (s,t) = readHammingStrings
            // Act.
            // because string implements seq<_> interface.
            let hamming = calcHammingDistance s t
            // Assert.
            Assert.Equal(527, hamming)