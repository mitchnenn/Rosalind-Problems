namespace Rosalind_Problems

open System
open UnitTestHelperLib.Converter
open Xunit
open Xunit.Abstractions
open RosalindLib
open AminoAcid

module TranslatDnatoRnaModule =

    type TarnslatingRnaTests(output: ITestOutputHelper) =
        do new Converter(output) |> Console.SetOut
        
        [<Fact>]
        member __.``Verify RNACodon matches Amino Acid``() =
            let rnaCodonEntries = RnaCodon.Load("TestData/rna-codon-table.csv")
            for entry in rnaCodonEntries.Rows do
                Assert.Equal(matchRnaCodonToAminoAcid entry.RnaCodon, entry.AminoAcid)
        
        [<Fact>]
        member __.``Parse RNA codons and match proteins.`` () =
            let aminoAcids = FileUtilities.readLines "TestData/rna_codon_data_set.txt"
                             |> Seq.head
                             |> matchRnaToProteins 
 
            let expected = "MTPPNRLALTRCSLSINLAKNVARHRENIRAFTLFRNVLLSYGQVACKIHAAPTPDGESSRANTVLVSIIARGGNSADDQDCGRGAGARVFACPGLFVPGQRVNSRHQSLLTRGVHTPSLGPRKGSTLKRRACNAVWVSIPLMCPNAGGMRYAHQIYEHARQLRHAESCNTRRLFRTSADTNSVTQCVLATGIVPSRPSTPRLWTGRSFDPHSFARNMLALACGGSALILRKRCMHTPTVNQCLNSLRISTLLGRTRKLSTKETSITMWVTGTVHCAIAKVMLTLTLSNAGAPYHHGLGRNFLSTPPPVAQGEVAEPLPDPGDIVESSDLPFRIKVIYWRYKLIVDILTKKPDPAEPGFSRRGWIIALLDRLMKFRALSAVAVLPALSLLAVSQRNHIRLTEVLLCCSRTSNLRRGGQPGIGAKLTIARRTRLRERVRTKSPQAQRRMAAVTVLSALPMLNSVVLMIPFNLVHTGTDLVLCLLALWLFTDFSSNIKREGWDRKAKFTSDINPATTAVVRRLQKLWDIGVTPLTKRFAFASEESHLLLDHNETAGATINDTISQPCLRSGGHQYQNMCKCGVYSCGITGIKEHKARSIVVIGGANEQEKNRNTACALSARTGACRNPEKLLCRQYCHYLMREPNREELLRRALAAAYTGMDDNSVDLEGGPPTQPRKADYPLLRADQPGISSSSYSPPAQQTNEGLSRQPILPPNRGRPHGSYLGCGTTELRTTTEQVPVVKSGALSSAVAPIRSDWLIIELQYMVLRSPDLPTDLRRNHLGRQGSSRSSTVRIPSTQGSSSVRKVIFRSFLFCRAYRNYQLLSTLYRMTLWLCRTLYAPPVYLHTRPLERRQCGLGSEGTTLLRHITPNPSQEPQERYTVPLPTPRFGPHNAAPRVESNLNRCNTENPTMDTSSHISPRRSTGLRNAPGRVIRNGALFYRYHATGYIGVPISPGMQTFTHSAIAQTGNTNSHALHLTNPGIELNNPVLVTKKVPILRVRSKDGKQNATRPWEGHSSRAGERQNAECHVTRQRIISEFRALSNPLKSRYIPNIVLRWQLGTRIPHSSTPPTRRTSGVMYLLPSNLLRCGPWGSQLQKGRCALLNDRLLLLVVIMPLRRREQKVTYQGPQKSIVSSTTVGPRAHYTLTEYLEKTPAQETLLLMRTPLTPRTQPHLLALELHFYSFLALSSYLPRLKIHLYPRPVFDHCGRSLRRHYAHSGSDKSRTLFGRPPCPTARRHTEHRTVLPSVQHIQTGNSGSAMHIVQTWPRPFGAHSLLLCNNTAETLSPAQSIQRGHNGRLMNSHSGDDSKGYPSLGLLHKQRVSLDSRTGVYPHRTSRTKGGKQAEAKKSSLRNSSHITTLRAARVSLNMKVGTKSIGYWRRVGWVQFRERQVLRDAGRAEFQVGQRNHQLPTPLLPLLHEGRLEEFSSSTDSGFNYNPLLAYLISPPVFSFAQSRFASMRAAVAPAYVVPSIRLKAMDLQPLFVTSRSYSAWTSAPRTEAQSTSMIRHFFAFKPLYAANFDCRETRSITGVSAGGVTALSSGLSIRLTPPRIRNPSGRGGKFRGLWPWKNSPYQLTVYFGLVTHRFIPSAPLTVLIYQNFNLNGGIAHRRALCPDSNFAHSIAALIILRPRSQLSTTSLVHWSPALIRGSFSTPRVTRRQHYSGEVLGTNPRSHLRALGLLYQNSDRQPRPPGNGFPQRDLIRGSSWLGCRKIYCRNPITSLILRDTLRPRYNGLGCISGDVPRIKVALSEIWGGVVHQLTINPNPTLHVVTGRKPRIDWRSRGSLPSPVFMGRDVGHYVLKLRVASLTCLTWKPGQGRRQRTRMAEAPVASGYVQCWTYTYNPYIYLALATRYKFMQNITMATENHGPEHQQIQGSDLFPAFNHSIVSEMSFRSTELRTADARQQVTVWCTKSPPAERRLVTIFSRRGERAPRDIPVRASSIYYVPSIRHRGANSREFINVSGTHKGTIVATGSRPKRVFKTIILSSTMQAGNCKCPCLEKRFSRTKIFVDIQRGCLLGHRTPSLEPLKGLSRGWKRYLQLKLFTIIGRYNHDLHSGCAPSLIESTQLAVERRAGNTSFVLPMTFNERGDHSVYKPNTAVRHLGCLQYIIAPPSCRQFSSLGNYVYALWATRASPKVMMREHHQSQHLITPGRNSLETGSIGLHVFTRAFGRLDVYSHLHLFSLAILRGGEGIPATILLFMMGRVRVSYISRCRMSGTALSKAYTSQSVYQTKDWSLQYPLGTPNLVIYTGTGADVQSASRDMFRTTMDWITVFSQRVLASRRRGHLFVLSATDAESLPEIRAGCRSRAVYTTQGHRPLVYEALADSCYVIGAPCRYGMAYHDGRHATVRPPRGCPYLLHVHHHLWDTSLLFGQGSGQQVERSKPGHEAVIALLAAFALAVFCTSIIPSEYANCRAVLGYIRPTANPRRVTPGPERSFNLVPVIDWPSQRQAILSQLSNAFCSGTRRVVVCSGQTYIHSFYGEMLGGSFLPCATPFNYSGRQDGTTNPVTLKWDTHLLGISTTFYGQKWAAFAEVVGPSSGTRGCPSGVSRTSASGGTGTLVRGAYRDLFGDRFSGVLLPPVEFILRSVLAQPSQQFVTASAPRGRVLSTGPFNDPAPGLQAFLAPHSDCNADCGLIYFHGMGCYVQRCCSRAGMACINGVIGIALAADQETVILGNRWPANKTELESGLISKTGKFVRLLRRPAWTSLVSRLLADPTSAIVSHRDVASKWILHQTRVLHRTEPRRRVILLKHRNLAAACLVLGRSYLAAVIGTGLGFTHVEVAPSGDLLQTSSTCHFTLDIGLPHPGLRALSTYFGPIRLFHRIAPSIVLACLRPRELGRCMPRLLTSLPWVKNVINTVKKGCPECMNTIWITMRPAQYWITNRAVLNSQSLLILLNHTSLKNGKKRRKVRPSPQKSAPTVSIRYCPAGRVALEHVGAQCYTNRLRHAQKCSLDLPSQTAYKLGHKNHKTAGLSSQGSYPRIPGCITYLPAGRSELSGRIAAWKVCTVARFSLHTRVCPDLKPRTLAPINYIPGFVIPTRYYPELTVPENYFVIGNGLSNAPSICQFPRPVKELVIMFYAAATDITMGTVGYTNWYHDVFTERAPRPLPTVADRSRSRGEYNEMYLSLTTDENSTYYAYPYRYKLLISRLTPVTPVRDRIRWHRDTLDNALTSGMCIIPPLGSMVITIELHLSARCVGRSVESQIPDP"
            for ac in aminoAcids do
                printfn "%s" ac
            Assert.Equal(expected, aminoAcids.[0])
