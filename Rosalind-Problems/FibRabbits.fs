namespace Rosalind_Problems

open System
open Helpers
open Xunit
open Xunit.Abstractions

module FibRabbits =
    
    type FibRabbitsTests(output : ITestOutputHelper) =
        do new Converter(output) |> Console.SetOut
        
        let rec pairs month size =
            match month with
            | 0 -> 0
            | (1|2) -> 1
            | _ -> (pairs (month - 1) size) + (size * (pairs (month - 2) size)) 
            
        [<Theory>]
        [<InlineData(12, 1, 144)>]
        [<InlineData(3, 2, 3)>]
        [<InlineData(4, 2, 5)>]
        [<InlineData(5, 2, 11)>]
        [<InlineData(3, 3, 4)>]
        [<InlineData(4, 3, 7)>]
        [<InlineData(5, 3, 19)>]
        [<InlineData(6, 2, 21)>]
        [<InlineData(7, 2, 43)>]
        member __.``Test with variable litter size`` month size expected =
            // Arrange
            // Act
            let pairs = pairs month size
            // Assert
            Assert.Equal(expected, pairs)
