namespace RosalindLib

module FileUtilities = 

    open System.IO

    let readLines (path:string) =
        seq {
            use sr = new StreamReader(path)
            while not sr.EndOfStream do
                yield sr.ReadLine()
        }
        