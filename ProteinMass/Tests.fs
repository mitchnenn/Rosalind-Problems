namespace ProteinMass

open System
open System.IO
open FSharp.Data
open Xunit
open Xunit.Abstractions

type Converter( output : ITestOutputHelper ) =
    inherit TextWriter()
    override __.Encoding = stdout.Encoding
    override __.WriteLine message = output.WriteLine message
    override __.Write message = output.WriteLine message

type MonoIsotopicWeight = CsvProvider<"Data/MonoIsotopicMassTable.csv">

module ProteinMassTests =
    type ProteinMassTests(output : ITestOutputHelper) =
        do new Converter(output) |> Console.SetOut
    
        let getMonoIsotopicWeight l =
            let entry = MonoIsotopicWeight.Load("Data/MonoIsotopicMassTable.csv").Rows
                        |> Seq.find (fun i -> i.AminoAcid = l)
            float entry.Weight                        
        
        let getTotalMonoIsotopicWeight (p:string) =
            p.ToCharArray()
            |> Array.map (fun i -> getMonoIsotopicWeight (i.ToString()))
            |> Array.reduce (+)
        
        [<Fact>]
        let ``Fetch monoisotopic weight`` () =
            let weight = getMonoIsotopicWeight "S"
            printfn "%f" weight
            Assert.Equal(87.03203, weight)

        [<Theory>]
        [<InlineData("SKADYEK", 821.392)>]
        [<InlineData("WDCWQYPLMGLHGWSHRYEESDALHCMGCMWGYAKYTDCKAHFEPWTCKDEGTRKHSYERLTQWSAFGHGPTIGAWQMWAGKGAPSYTFTKDKHKQDHWPGAARRPETPGKVLHTTGGWDPPWVSFWLFGFVETVLTGQEHNWNNEICGRERQIRVADSWIEGNTYKAMYIDNWIEQYIAYTHGWQGTLFHYGFSEFMRPFMDDFSRCIHTVCAFMVPIIWVRFHDNFMCNHIGLGVIMVSRCQGNAMDVQMVRCKACMFTEMNNGSLKGVVMKEMLSSMMFHKMQYYKHDTYRCVLMGSMVNVDTDYVSYAITMDLTQWTRLDAIIIMQMIARYEYRPAGTIENTLFLGQIHVLRRAKSMPAIVNVFKHNRHYHVPFFGPNVMTMFGYWVAAFHGGFCRTDGKEWWCHSMNWDEQFERRRVCEGEQERDHVLMVATTYIRNNPVCLKRQWYANWLGLRKCDKLRMDEMSGQPTFTECKREANEPVSIHFTPRMTYKLTWKMQSNMPCHKAGCADTDAWMPIIVHHSNYRKHHVSSGVYRTFSMWYWVLPNWQVYVKIFCKTEMNPPPAPHHHVAEGAHKGMFIPVAIKRPCVYVKGAANNFKDITCMYNSWVRPNCRAQSIEHDMLLCAQGRDMKLWDMKEDGVWQFQTTKGLQMEWPGSPCVERLAMKIPDAFDQRHKRLMVNKWYWHQYMMVMIPACIEESYRKHMFELRYFGLTLMHLVITVLAVGMIVLWLYTKDDNACMHIDAGCAYHKTIPTWPGEYDRKRNDQKASCHLMGHCVQIGVRMCSFVSRIGPMNKPYCMQGVWKNMFIGLWFQWFEWLRYLDDHWRFLQIGLGHVIFRSNMDGELVWTLAAYVDSCCLVIINMWDEGLVKVGRTERQVKPWRNEDATMFICGGWHASMETTFLHVPQHSRYVITGVIINWTGTGLLAYYALHGQKCERIYKGEENHVKEEGRMCIMMHLENIIFMWLRKSIM", 115185.16)>]
        let ``Determine amino acid string weight`` (p, expected) =
            let totalWeight = getTotalMonoIsotopicWeight p
            let roundedTotalWeight = Math.Round(totalWeight, 3) 
            printfn "%f" roundedTotalWeight
            Assert.Equal(expected, roundedTotalWeight)
            