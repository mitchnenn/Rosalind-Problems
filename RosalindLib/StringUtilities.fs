namespace RosalindLib

module StringUtilities =
    
    let charCount x = Seq.filter ((=) x) >> Seq.length        

