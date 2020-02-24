namespace Rosalind_Problems

open System.IO
open Xunit
open RosalindLib.ProcessFasta

module ParseFastaChars =
        
    let isValidChar (e,_) = e
    
    let readChar (_,c) = c
    
    let fileChars (processer : FastAReader) =
        seq {
            let mutable r = processer.publicGetChar(true)
            while (isValidChar r) = true do
                yield readChar(r)
                r <- processer.publicGetChar(true)
        }
    
    [<Fact>]
    let ``Test character reads`` () =
        // Arrange.
        let path = Path.Combine(Directory.GetCurrentDirectory(), "TestData", "rosalind.fasta")
        use reader = File.OpenRead(path)
        use processer = new FastAReader(reader)
        
        // Act.
        let charSeq = fileChars processer |> Seq.take 10 |> Seq.toList
        
        // Assert
        Assert.True(false)