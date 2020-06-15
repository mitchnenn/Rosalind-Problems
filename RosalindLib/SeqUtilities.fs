namespace RosalindLib

module GeneralUtilities = 

    let count x = Seq.filter((=) x) >> Seq.length
