namespace RosalindLib

module MathUtilities =
    
    let roundFloat (digits:int) (value:float)
        = System.Math.Round(value, digits)

