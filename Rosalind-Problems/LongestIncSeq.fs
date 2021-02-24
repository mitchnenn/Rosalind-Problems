namespace Rosalind_Problems

open System.Collections
open System.Text
open Xunit
open Xunit.Abstractions
open FsUnit
open FParsec

type IntSeqDataRecord = { Length:int; Sequence:int list }

module LongestIncSeq =
    type LongestIncSeqTests (output:ITestOutputHelper) =

        let inc x y = y > x
        
        let dec x y = y < x



