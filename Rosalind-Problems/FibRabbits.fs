namespace Rosalind_Problems

open System
open Helpers
open Xunit
open Xunit.Abstractions

module FibRabbits =
    
    let rec pairs (month:double) (size:double) =
        match month with
        | 0.0 -> 0.0
        | (1.0|2.0) -> 1.0
        | _ -> (pairs (month - 1.0) size) + (size * (pairs (month - 2.0) size)) 

    [<Theory>]
    [<InlineData(12.0, 1.0, 144.0)>]
    [<InlineData(3.0, 2.0, 3.0)>]
    [<InlineData(4.0, 2.0, 5.0)>]
    [<InlineData(5.0, 2.0, 11.0)>]
    [<InlineData(3.0, 3.0, 4.0)>]
    [<InlineData(4.0, 3.0, 7.0)>]
    [<InlineData(5.0, 3.0, 19.0)>]
    [<InlineData(6.0, 2.0, 21.0)>]
    [<InlineData(7.0, 2.0, 43.0)>]
    let ``Test with variable litter size`` month size expected =
        // Arrange
        // Act
        let pairs = pairs month size
        // Assert
        Assert.Equal(expected, pairs)
    
    type FibRabbitsTests(output : ITestOutputHelper) =
        do new Converter(output) |> Console.SetOut
            
        [<Fact>]
        member __.``Test downloaded dataset`` () =
            // Arrange.
            let month = 32.0
            let size = 5.0
            // Act.
            let pairs = pairs month size
            // Assert.
            Assert.Equal(40238153982301.0, pairs)
            