namespace Rosalind_Problems

open System
open Xunit
open Xunit.Abstractions
open Helpers

module Tests =

    let toUpper (x:string) = x.ToUpper()

    let explode (x:string) = [| for c in x -> c |]

    type RosalindTests(output : ITestOutputHelper) =
        do new Converter(output) |> Console.SetOut

        [<Fact>]
        member __.``Count occurances of bases`` () =
            // Arrange.
            let data = "AGCTTTTCATTCTGACTGCAACGGGCAATATGTCTCTGTGTGGATTAAAAAAAGAGTGTCTGATAGCAGC"
            
            // Act.
            let results =
                toUpper data
                |> explode
                |> Seq.countBy id
                |> Seq.toList
            
            // Assert
            printfn "%A" results
            Assert.Equal(4, results.Length)
