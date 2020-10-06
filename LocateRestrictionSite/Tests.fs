namespace LocateRestrictionSite

open System
open UnitTestHelperLib.Converter
open Xunit
open Xunit.Abstractions

module Tests =
    type PermutationTests(output : ITestOutputHelper) =
        do new Converter(output) |> Console.SetOut

        [<Fact>]
        let ``My test`` () =
            Assert.True(true)