namespace Rosalind_Problems

open System.IO
open Xunit.Abstractions

module Helpers = 

    type Converter( output : ITestOutputHelper ) =
        inherit TextWriter()
        override __.Encoding = stdout.Encoding
        override __.WriteLine message =
            output.WriteLine message
        override __.Write message =
                output.WriteLine message
