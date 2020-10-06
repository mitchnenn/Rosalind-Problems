namespace Rosalind_Problems

open System
open Xunit.Abstractions
open UnitTestHelperLib.Converter
open Xunit

/// # of offspring genotype
/// 100% : AA-AA -> AA, AA, AA, AA -> 1
/// 100% : AA-Aa -> AA, AA, Aa, Aa -> 2
/// 100% : AA-aa -> Aa, Aa, Aa, Aa -> 3
/// 75%  : Aa-Aa -> Aa, Aa, Aa, aa -> 4
/// 50%  : Aa-aa -> Aa, Aa, aa, aa -> 5
/// 0%   : aa-aa -> aa, aa, aa, aa -> 6

module CalcExpectedOffspring =

    let E'offspringDomPhenotype p'offspringDomPhenotype numChildren numPop =
        p'offspringDomPhenotype * numChildren * numPop
    
    let calcOffspring info =
        match info with
        | numPop, p'offspringDomPhenotype -> E'offspringDomPhenotype p'offspringDomPhenotype 2.0 numPop
    
    type CalcExpectedOffspringTests(output: ITestOutputHelper) =
        do new Converter(output) |> Console.SetOut

        [<Theory>]
        [<InlineData(1,0,0,1,0,1,3.5)>]
        [<InlineData(19174,18178,17936,16680,17341,19823,152937.0)>]
        member __.``Calculate expected offspring`` (nAAAA:double, nAAAa:double, nAAaa:double, nAaAa:double, nAaaa:double, naaaa:double, expected:double) =
            let answer = [(nAAAA, 1.0);(nAAAa, 1.0);(nAAaa, 1.0);(nAaAa, 0.75);(nAaaa, 0.5);(naaaa, 0.0)]
                         |> List.map calcOffspring
                         |> List.sum
            Console.WriteLine("Answer: {0}", answer)
            Assert.Equal(expected, answer)