namespace Rosalind_Problems

open System.IO
open Xunit

module MendelProbability =
    
    let getPopulations =
        let workingDir = Path.Combine(Directory.GetCurrentDirectory(), "TestData")
        let path = Path.Combine(workingDir, "MendelPopulations.txt")
        use sr = new StreamReader(path)
        let pops = sr.ReadLine().Split(" ")
        (float pops.[0],float pops.[1],float pops.[2])

    let propOfDominantAlle (k:float) (m:float) (n:float) =
        let t = k + m + n
        let exp1 = k / t 
        let exp2 = m * (k + ((3.0/4.0) * m) - (3.0/4.0) + (n/2.0)) / (t * (t - 1.0))
        let exp3 = n * (k + (m/2.0)) / (t * (t - 1.0))
        exp1 + exp2 + exp3
        
            
    [<Fact>]
    let ``Probability organism produce dominant allele`` () =
        // Arrange
        // k homozygous dominant, m heterozygous, n homozygous recessive 
        let (k,m,n) = getPopulations
        // Act.
        let propValue = propOfDominantAlle k m n
        // Assert.
        let actual = sprintf "%.5f" propValue                        
        Assert.Equal("0.72626", actual)