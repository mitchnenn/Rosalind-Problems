namespace RosalindLib

open System
open System.IO

module ProcessFasta =
    
    type FastAReader (source : Stream) =
        
        let stream = source
        
        let mutable reader : StreamReader = new StreamReader(stream)
        
        [<Literal>]
        let bufferSize = 1000
                
        let mutable buffer : char[] = Array.zeroCreate bufferSize
        
        let mutable currentPosition = bufferSize
        
        let mutable endOfFile = false
        
        let updateBuffer() : bool =
            reader.ReadBlock(buffer, 0, buffer.Length) <> 0
            
        let initialize() =
            let pos = source.Seek(0L, SeekOrigin.Begin)
            if not (pos = 0L) then
                invalidOp "sdf"
                
        let rec getChar (eat : bool) : (bool * char) =
            let position = currentPosition
            match position with
            | 1000 ->
                match updateBuffer() with
                | false ->
                    endOfFile <- true
                    false, Char.MinValue
                | true ->
                    currentPosition <- 0
                    getChar(eat)
            | _ ->
                match int buffer.[position] with
                | 0 ->
                    false, Char.MinValue
                | _ ->
                    match eat with
                    | true ->
                        currentPosition <- currentPosition + 1
                        true, buffer.[position]
                    | false ->
                        true, buffer.[position]

        do
            match source with
            | null -> nullArg "source"
            | _ -> ()
            match source.CanRead with
            | false -> invalidArg "source" "The source stream can't be read."
            | _ -> ()
            match source.CanSeek with
            | false -> invalidArg "source" "The source stream doesn't support seek."
            | _ -> ()
                
            initialize()
    
        interface IDisposable with
            member this.Dispose() = 
                match reader with
                | null -> ()
                | _ -> reader.Dispose(); reader <- null

        private new() = new FastAReader(null)
        
        member x.publicGetChar = getChar