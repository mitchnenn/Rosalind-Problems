namespace Rosalind_Problems

open System
open Xunit
open Xunit.Abstractions
open UnitTestHelperLib.Converter
open RosalindLib.MathUtilities

module MendelSecondLaw =
    
    type MendelSecondLawTests(output: ITestOutputHelper) =
        do new Converter(output) |> Console.SetOut    

        let singleProb i pop =
            let f1 = double(factorial pop)
            let f2 = double(factorial i)
            let f3 = double(factorial (pop - i))
            let p1 = pown (double 0.25) (int(i))
            let p2 = pown (double 0.75) (int(pop - i))
            (f1 / (f2 * f3)) * p1 * p2 
        
        let sumProbability N pop : double = 
            seq { N .. pop + 1 }
            |> Seq.map(fun i -> (singleProb (bigint(i)) (bigint(pop))))
            |> Seq.sum
        
        [<Fact>]
        member __.``Mendels law of independence test`` () =
            let k = 5 // generation
            let N = 8 // least number of AaBb in the generation    
            let P = pown 2 k // population of generation
            printfn "%.3f" (sumProbability N P)
            Assert.True(true)
