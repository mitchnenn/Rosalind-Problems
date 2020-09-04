namespace RosalindLib

module MathUtilities =
    
    let roundFloat (digits:int) (value:float)
        = System.Math.Round(value, digits)

    let rec factorial x:bigint =
        if x < bigint(1) then bigint(1)
        else x * factorial (x - bigint(1))
    