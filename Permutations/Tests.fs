namespace Permutations

open System
open System.Text
open Xunit
open UnitTestHelperLib.Converter
open Xunit.Abstractions

module Tests =
    type PermutationTests(output : ITestOutputHelper) =
        do new Converter(output) |> Console.SetOut
        
        let rec factorial n =
            match n with
            | 0 | 1 -> 1
            | _ -> n * factorial(n-1)
            
        let printCombinationCount n =             
            printfn "%d" (factorial n)

        let rec permutations = function
            | []      -> seq [List.empty]
            | x :: xs -> Seq.collect (insertions x) (permutations xs)
        and insertions x = function
            | []             -> [[x]]
            | (y :: ys) as xs -> (x::xs)::(List.map (fun x -> y::x) (insertions x ys))

        let formatOutput (p:int list) =
            let sb = StringBuilder(p.Length)
            p
            |> List.map (fun i -> sprintf "%d " i)
            |> List.iter (sb.Append >> ignore)
            sb.ToString()
                    
        let printPermutations n = 
            permutations [1..n]
            |> Seq.iter (fun p -> printf "%O" (formatOutput p))
        
        [<Fact>]
        let ``My test`` () =
            let n = 5
            printCombinationCount n
            printPermutations n
            Assert.True(true)
