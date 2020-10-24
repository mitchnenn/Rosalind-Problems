namespace Rosalind_Problems
namespace Rosalind_Problems

open Xunit
open Xunit.Abstractions
 
module AlphabetPermutations =
    type AlphabetPermutationsTests (output:ITestOutputHelper) =
         
        let concatString (strings:string list) (alphabet:string list) = seq {
            for s in strings |> List.sort do
                for a in alphabet |> List.sort do
                    yield s + a
        }
 
        let concatStringList (strings:string list) (alphabet:string list) =
            concatString strings alphabet |> Seq.toList
                    
        let getPermutationsByLength (alphabet:string list) (length:int) =
            let rec recFunc strings len =
                match len < length with
                | true -> recFunc (concatStringList strings alphabet) (len + 1)
                | false -> strings
            recFunc alphabet 1
         
        [<Fact>]
        let ``Alphabet Permutations Test`` () =
            let alphabet = ["A"; "B"; "C"; "D"; "E"; "F"; "G"; "H"] 
            let length = 3
            let alphabetPermutations = getPermutationsByLength alphabet length
            alphabetPermutations |> List.iter(fun i -> output.WriteLine(sprintf "%s" i))
            Assert.True(true);
             
        