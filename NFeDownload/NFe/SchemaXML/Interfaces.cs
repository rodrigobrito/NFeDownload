using System.Xml.Serialization;

namespace NFeDownload.NFe.SchemaXML
{
    public interface ITEventoInfEventoDetEvento
    {

        TEventoInfEventoDetEventoDescEvento descEvento { get; set; }

        TEventoInfEventoDetEventoVersao versao { get; set; }

        string nProt { get; set; }

        string xJust { get; set; }

        string xCorrecao { get; set; }

        bool xCondUsoSpecified { get; set; }

        TEventoInfEventoDetEventoXCondUso xCondUso { get; set; }
    }

    public interface ITRetInutNFeInfInut
    {

        string cStat { get; set; }

        string xMotivo { get; set; }

        string nProt { get; set; }
    }

    public interface ITProcCancNFe
    {

        string versao { get; set; }

        ITCancNFe cancNFe { get; set; }

        ITRetCancNFe retCancNFe { get; set; }
    }

    public interface ITCancNFe
    {

    }

    public interface ITRetCancNFe
    {

    }



    public interface IReferenceType
    {
        byte[] DigestValue { get; }
    }

    public interface ISignedInfoType
    {

        IReferenceType Reference { get; }
    }

    public interface ISignatureType
    {
        ISignedInfoType SignedInfo { get; }
    }

    public interface ITNFe
    {
        ITNFeInfNFe infNFe { get; }
        ISignatureType Signature { get; }
    }

    public interface ITNFeInfNFe
    {
        string Id { get; }
        string versao { get; }
        ITNFeInfNFeIde ide { get; }
        ITNFeInfNFeDest dest { get; }
        ITNFeInfNFeEmit emit { get; }
    }

    public interface ITNFeInfNFeIde
    {
        //        string cDV { get; set; }
        //        string cMunFG { get; set; }
        //        string cNF { get; set; }
        ////        NFeDownload.NFe.SchemaXML200.TCodUfIBGE cUF { get; set; }
        string dEmi { get; }
        //        string dhCont { get; set; }
        //        string dSaiEnt { get; set; }
        //        string hSaiEnt { get; set; }
        ////        NFeDownload.NFe.SchemaXML200.TMod mod { get; set; }
        //        string natOp { get; set; }
        string nNF { get; }
        ////        NFeDownload.NFe.SchemaXML200.TProcEmi procEmi { get; set; }
        string serie { get; }
        ////        NFeDownload.NFe.SchemaXML200.TAmb tpAmb { get; set; }
        ////        NFeDownload.NFe.SchemaXML200.TNFeInfNFeIdeTpEmis tpEmis { get; set; }
        ////        NFeDownload.NFe.SchemaXML200.TNFeInfNFeIdeTpImp tpImp { get; set; }
        ////        NFeDownload.NFe.SchemaXML200.TNFeInfNFeIdeTpNF tpNF { get; set; }
        //        string verProc { get; set; }
        //        string xJust { get; set; }
    }

    public interface ITNFeInfNFeDest
    {
        string email { get; set; }
        ITEndereco enderDest { get; }
        string IE { get; }
        string xNome { get; }
    }

    public interface ITNFeInfNFeEmit
    {
        ITEnderEmi enderEmit { get; }
        string IE { get; }
        string xNome { get; }
    }


    public interface ITEndereco
    {
        string CEP { get; }
        string cMun { get; }
        string fone { get; }
        string nro { get; }
        TUf UF { get; set; }
        string xBairro { get; }
        string xCpl { get; }
        string xLgr { get; }
        string xMun { get; }
    }

    public interface ITEnderEmi
    {
        string CEP { get; }
        string cMun { get; }
        string fone { get; }
        string nro { get; }
        TUfEmi UF { get; set; }
        string xBairro { get; }
        string xCpl { get; }
        string xLgr { get; }
        string xMun { get; }
    }

    public interface ITNfeProc
    {
        string versao { get; set; }
        ITNFe NFe { get; set; }
        ITProtNFe protNFe { get; set; }
    }

    public interface ITProtNFe
    {
        ITProtNFeInfProt infProt { get; }

    }

    public interface ITProcEvento
    {
        ITEvento evento { get; set; }
        string versao { get; set; }
        ITretEvento retEvento { get; set; }
    }

    public interface ITProtNFeInfProt
    {
        string nProt { get; }
        byte[] digVal { get; }


        string chNFe { get; set; }

        string cStat { get; set; }

        string xMotivo { get; set; }

        TAmb tpAmb { get; set; }
    }

    //interfaces de comunicacao

    //só existe na versao 2.00
    public interface ITEnvEvento
    {
        //string versao { get; set; }
        //string idLote { get; set; }
        ITEvento[] evento { get; set; }

        string idLote { get; set; }

        string versao { get; set; }
    }
    //só existe na versao 2.00
    public interface ITRetEnvEvento
    {
        //string cStat { get; }
        //string xMotivo { get; }
        string cStat { get; set; }

        TAmb tpAmb { get; set; }

        string xMotivo { get; set; }

        ITretEvento[] retEvento { get; set; }
    }


    public interface ITEnviNFe
    {
        string idLote { get; set; }
        string versao { get; set; }

        ITNFe[] NFe { get; set; }
    }
    public interface ITRetEnviNFe
    {

        string cStat { get; }
        string xMotivo { get; }

        TAmb tpAmb { get; set; }

        ITRetEnviNFeInfRec infRec { get; set; }
    }
    public interface ITConsReciNFe
    {
        string nRec { get; set; }
        TAmb tpAmb { get; set; }
        string versao { get; set; }
    }
    public interface ITRetConsReciNFe
    {
        string cStat { get; }
        string xMotivo { get; }
        ITProtNFe[] protNFe { get; }
    }
    public interface ITConsStatServ
    {
        string versao { get; set; }
        TAmb tpAmb { get; set; }
        TCodUfIBGE cUF { get; set; }
    }
    public interface ITRetConsStatServ
    {
        string cStat { get; set; }
        string xMotivo { get; set; }
    }
    public interface ITInutNFe
    {
        ITInutNFeInfInut infInut { get; set; }

        string versao { get; set; }
    }
    public interface ITRetInutNFe
    {
        ITRetInutNFeInfInut infInut { get; }
    }
    public interface ITConsSitNFe
    {
        string chNFe { get; set; }
        TAmb tpAmb { get; set; }
        TVerConsSitNFe versao { get; set; }
    }
    public interface ITRetConsSitNFe
    {
        string cStat { get; }
        string xMotivo { get; }
        ITProtNFe protNFe { get; }
        ITProcEvento[] procEventoNFe { get; }

        ITRetCancNFe_v200107 retCancNFe { get; set; }
    }

    public interface ITEvento
    {
        ITEventoInfEvento infEvento { get; set; }
        ISignatureType Signature { get; }

        string versao { get; set; }
    }


    public interface ITEventoInfEvento
    {
        TEventoInfEventoTpEvento tpEvento { get; set; }

        string Id { get; set; }

        string chNFe { get; set; }

        TCOrgaoIBGE cOrgao { get; set; }

        string Item { get; set; }

        ITEventoInfEventoDetEvento detEvento { get; set; }

        string dhEvento { get; set; }

        ITCTypeCNPJCPF ItemElementName { get; set; }

        TAmb tpAmb { get; set; }

        string nSeqEvento { get; set; }

        TEventoInfEventoVerEvento verEvento { get; set; }
    }



    public interface ITInutNFeInfInut
    {
        string Id { get; set; }

        string nNFIni { get; set; }

        string nNFFin { get; set; }

        string serie { get; set; }

        string ano { get; set; }

        string CNPJ { get; set; }

        TCodUfIBGE cUF { get; set; }

        TMod mod { get; set; }

        TAmb tpAmb { get; set; }

        string xJust { get; set; }

        TInutNFeInfInutXServ xServ { get; set; }
    }

    public interface ITretEvento
    {
        ITretEventoInfEvento infEvento { get; set; }
    }

    public interface ITRetCancNFe_v200107
    {
        ITRetCancNFe_v200107InfCanc infCanc { get; }
    }

    public interface ITRetCancNFe_v200107InfCanc
    {
        string nProt { get; set; }
    }

    public interface ITRetEnviNFeInfRec
    {
        string nRec { get; set; }
    }

    public interface ITretEventoInfEvento
    {
        string cStat { get; set; }

        string nProt { get; set; }
    }
}

#region Aplicações Interfaces

namespace NFeDownload.NFe.SchemaXML200
{

    public partial class TEventoInfEventoDetEvento : NFeDownload.NFe.SchemaXML.ITEventoInfEventoDetEvento
    {

    }

    public partial class TretEventoInfEvento : NFeDownload.NFe.SchemaXML.ITretEventoInfEvento
    {

    }

    public partial class TRetEnviNFeInfRec : NFeDownload.NFe.SchemaXML.ITRetEnviNFeInfRec
    {

    }

    public partial class TRetCancNFe_v200107InfCanc : NFeDownload.NFe.SchemaXML.ITRetCancNFe_v200107InfCanc
    {

    }



    public partial class TRetCancNFe_v200107 : NFeDownload.NFe.SchemaXML.ITRetCancNFe_v200107
    {

        SchemaXML.ITRetCancNFe_v200107InfCanc SchemaXML.ITRetCancNFe_v200107.infCanc
        {
            get
            {
                return this.infCanc;
            }

        }
    }

    public partial class TretEvento : NFeDownload.NFe.SchemaXML.ITretEvento
    {

        SchemaXML.ITretEventoInfEvento SchemaXML.ITretEvento.infEvento
        {
            get
            {
                return this.infEvento;
            }

            set
            {
                this.infEvento = (TretEventoInfEvento)value;
            }
        }
    }

    public partial class TRetCancNFe : NFeDownload.NFe.SchemaXML.ITRetCancNFe
    {

    }


    public partial class TCancNFe : NFeDownload.NFe.SchemaXML.ITCancNFe
    {

    }


    public partial class TInutNFeInfInut : NFeDownload.NFe.SchemaXML.ITInutNFeInfInut
    {

    }

    public partial class ReferenceType : NFeDownload.NFe.SchemaXML.IReferenceType
    {

    }

    public partial class SignedInfoType : NFeDownload.NFe.SchemaXML.ISignedInfoType
    {
        SchemaXML.IReferenceType SchemaXML.ISignedInfoType.Reference
        {
            get { return this.Reference; }
        }
    }

    public partial class SignatureType : NFeDownload.NFe.SchemaXML.ISignatureType
    {

        SchemaXML.ISignedInfoType SchemaXML.ISignatureType.SignedInfo
        {
            get
            {
                return this.SignedInfo;
            }

        }
    }

    public partial class TEnvEvento : NFeDownload.NFe.SchemaXML.ITEnvEvento
    {
        SchemaXML.ITEvento[] SchemaXML.ITEnvEvento.evento
        {
            get
            {
                return this.evento;
            }
            set
            {
                this.evento = (TEvento[])value;
            }
        }
    }
    public partial class TRetEnvEvento : NFeDownload.NFe.SchemaXML.ITRetEnvEvento
    {
        SchemaXML.ITretEvento[] SchemaXML.ITRetEnvEvento.retEvento
        {
            get
            {
                return this.retEvento;
            }
            set
            {
                this.retEvento = (TretEvento[])value;
            }
        }
    }

    public partial class TEnviNFe : NFeDownload.NFe.SchemaXML.ITEnviNFe
    {
        SchemaXML.ITNFe[] SchemaXML.ITEnviNFe.NFe
        {
            get
            {
                return this.NFe;
            }
            set
            {
                this.NFe = (TNFe[])value;
            }
        }
    }
    public partial class TRetEnviNFe : NFeDownload.NFe.SchemaXML.ITRetEnviNFe
    {
        SchemaXML.ITRetEnviNFeInfRec SchemaXML.ITRetEnviNFe.infRec
        {
            get
            {
                return this.infRec;
            }
            set
            {
                this.infRec = (TRetEnviNFeInfRec)value;
            }
        }
    }
    public partial class TConsReciNFe : NFeDownload.NFe.SchemaXML.ITConsReciNFe { }
    public partial class TRetConsReciNFe : NFeDownload.NFe.SchemaXML.ITRetConsReciNFe
    {
        SchemaXML.ITProtNFe[] SchemaXML.ITRetConsReciNFe.protNFe
        {
            get
            {
                return this.protNFe;
            }
        }
    }
    public partial class TConsStatServ : NFeDownload.NFe.SchemaXML.ITConsStatServ { }
    public partial class TRetConsStatServ : NFeDownload.NFe.SchemaXML.ITRetConsStatServ { }
    public partial class TInutNFe : NFeDownload.NFe.SchemaXML.ITInutNFe
    {
        SchemaXML.ITInutNFeInfInut SchemaXML.ITInutNFe.infInut
        {
            get { return this.infInut; }
            set { this.infInut = (TInutNFeInfInut)value; }
        }
    }
    public partial class TRetInutNFe : NFeDownload.NFe.SchemaXML.ITRetInutNFe
    {
        SchemaXML.ITRetInutNFeInfInut SchemaXML.ITRetInutNFe.infInut
        {
            get { return this.infInut; }
        }
    }

    public partial class TRetInutNFeInfInut : NFeDownload.NFe.SchemaXML.ITRetInutNFeInfInut
    {

    }


    public partial class TConsSitNFe : NFeDownload.NFe.SchemaXML.ITConsSitNFe { }
    public partial class TRetConsSitNFe : NFeDownload.NFe.SchemaXML.ITRetConsSitNFe
    {
        SchemaXML.ITProtNFe SchemaXML.ITRetConsSitNFe.protNFe
        {
            get { return this.protNFe; }
        }

        SchemaXML.ITProcEvento[] SchemaXML.ITRetConsSitNFe.procEventoNFe
        {
            get { return this.procEventoNFe; }
        }

        SchemaXML.ITRetCancNFe_v200107 SchemaXML.ITRetConsSitNFe.retCancNFe
        {
            get
            {
                return this.retCancNFe;
            }
            set
            {
                retCancNFe = (TRetCancNFe_v200107)value;
            }
        }
    }

    public partial class TEvento : NFeDownload.NFe.SchemaXML.ITEvento
    {
        SchemaXML.ITEventoInfEvento SchemaXML.ITEvento.infEvento
        {
            get { return this.infEvento; }
            set { this.infEvento = (TEventoInfEvento)value; }
        }

        SchemaXML.ISignatureType SchemaXML.ITEvento.Signature
        {
            get { return this.Signature; }
        }
    }
    public partial class TEventoInfEvento : NFeDownload.NFe.SchemaXML.ITEventoInfEvento
    {

        SchemaXML.ITEventoInfEventoDetEvento SchemaXML.ITEventoInfEvento.detEvento
        {
            get
            {
                return this.detEvento;
            }
            set
            {
                this.detEvento = (TEventoInfEventoDetEvento)value;
            }
        }
    }

    public partial class TNfeProc : NFeDownload.NFe.SchemaXML.ITNfeProc
    {
        SchemaXML.ITNFe SchemaXML.ITNfeProc.NFe
        {
            get
            {
                return this.NFe;
            }
            set
            {
                this.NFe = (TNFe)value;
            }
        }

        SchemaXML.ITProtNFe SchemaXML.ITNfeProc.protNFe
        {
            get
            {
                return this.protNFe;
            }
            set
            {
                this.protNFe = (TProtNFe)value;
            }
        }
    }
    public partial class TProtNFe : NFeDownload.NFe.SchemaXML.ITProtNFe
    {
        SchemaXML.ITProtNFeInfProt SchemaXML.ITProtNFe.infProt
        {
            get { return this.infProt; }
        }
    }

    public partial class TProtNFeInfProt : NFeDownload.NFe.SchemaXML.ITProtNFeInfProt
    {

    }

    public partial class TProcEvento : NFeDownload.NFe.SchemaXML.ITProcEvento
    {

        SchemaXML.ITEvento SchemaXML.ITProcEvento.evento
        {
            get { return this.evento; }
            set { this.evento = (TEvento)value; }
        }

        SchemaXML.ITretEvento SchemaXML.ITProcEvento.retEvento
        {
            get
            {
                return this.retEvento;
            }
            set
            {
                this.retEvento = (TretEvento)value;
            }
        }
    }


    public partial class TNFe : NFeDownload.NFe.SchemaXML.ITNFe
    {
        NFeDownload.NFe.SchemaXML.ITNFeInfNFe NFeDownload.NFe.SchemaXML.ITNFe.infNFe
        {
            get
            {
                return this.infNFe;
            }
        }

        SchemaXML.ISignatureType SchemaXML.ITNFe.Signature
        {
            get { return this.Signature; }
        }
    }

    public partial class TNFeInfNFe : NFeDownload.NFe.SchemaXML.ITNFeInfNFe
    {
        SchemaXML.ITNFeInfNFeIde SchemaXML.ITNFeInfNFe.ide
        {
            get { return this.ide; }
        }

        SchemaXML.ITNFeInfNFeDest SchemaXML.ITNFeInfNFe.dest
        {
            get { return this.dest; }
        }

        SchemaXML.ITNFeInfNFeEmit SchemaXML.ITNFeInfNFe.emit
        {
            get { return this.emit; }
        }
    }

    public partial class TNFeInfNFeIde : NFeDownload.NFe.SchemaXML.ITNFeInfNFeIde
    {

    }

    public partial class TNFeInfNFeDest : NFeDownload.NFe.SchemaXML.ITNFeInfNFeDest
    {

        SchemaXML.ITEndereco SchemaXML.ITNFeInfNFeDest.enderDest
        {
            get
            {
                return this.enderDest;
            }
        }
    }

    public partial class TNFeInfNFeEmit : NFeDownload.NFe.SchemaXML.ITNFeInfNFeEmit
    {
        SchemaXML.ITEnderEmi SchemaXML.ITNFeInfNFeEmit.enderEmit
        {
            get
            {
                return this.enderEmit;
            }
        }
    }

    public partial class TEndereco : NFeDownload.NFe.SchemaXML.ITEndereco
    {

    }

    public partial class TEnderEmi : NFeDownload.NFe.SchemaXML.ITEnderEmi
    {

    }
}


namespace NFeDownload.NFe.SchemaXML300
{

    public partial class TEventoInfEventoDetEvento : NFeDownload.NFe.SchemaXML.ITEventoInfEventoDetEvento
    {

    }

    public partial class TretEventoInfEvento : NFeDownload.NFe.SchemaXML.ITretEventoInfEvento
    {

    }

    public partial class TRetEnviNFeInfRec : NFeDownload.NFe.SchemaXML.ITRetEnviNFeInfRec
    {

    }

    public partial class TRetCancNFe_v200107InfCanc : NFeDownload.NFe.SchemaXML.ITRetCancNFe_v200107InfCanc
    {

        string SchemaXML.ITRetCancNFe_v200107InfCanc.nProt
        {
            get
            {
                return null;
            }
            set
            {
                //throw new System.NotImplementedException();
            }
        }
    }



    public partial class TRetCancNFe_v200107 : NFeDownload.NFe.SchemaXML.ITRetCancNFe_v200107
    {


        SchemaXML.ITRetCancNFe_v200107InfCanc SchemaXML.ITRetCancNFe_v200107.infCanc
        {
            get { return null; }
        }
    }

    public partial class TretEvento : NFeDownload.NFe.SchemaXML.ITretEvento
    {

        SchemaXML.ITretEventoInfEvento SchemaXML.ITretEvento.infEvento
        {
            get
            {
                return this.infEvento;
            }

            set
            {
                this.infEvento = (TretEventoInfEvento)value;
            }
        }
    }

    public partial class TRetCancNFe : NFeDownload.NFe.SchemaXML.ITRetCancNFe
    {

    }


    public partial class TCancNFe : NFeDownload.NFe.SchemaXML.ITCancNFe
    {

    }


    public partial class TInutNFeInfInut : NFeDownload.NFe.SchemaXML.ITInutNFeInfInut
    {

    }

    public partial class ReferenceType : NFeDownload.NFe.SchemaXML.IReferenceType
    {

    }

    public partial class SignedInfoType : NFeDownload.NFe.SchemaXML.ISignedInfoType
    {
        SchemaXML.IReferenceType SchemaXML.ISignedInfoType.Reference
        {
            get { return this.Reference; }
        }
    }

    public partial class SignatureType : NFeDownload.NFe.SchemaXML.ISignatureType
    {

        SchemaXML.ISignedInfoType SchemaXML.ISignatureType.SignedInfo
        {
            get
            {
                return this.SignedInfo;
            }

        }
    }

    public partial class TEnvEvento : NFeDownload.NFe.SchemaXML.ITEnvEvento
    {
        SchemaXML.ITEvento[] SchemaXML.ITEnvEvento.evento
        {
            get
            {
                return this.evento;
            }
            set
            {
                this.evento = (TEvento[])value;
            }
        }
    }
    public partial class TRetEnvEvento : NFeDownload.NFe.SchemaXML.ITRetEnvEvento
    {
        SchemaXML.ITretEvento[] SchemaXML.ITRetEnvEvento.retEvento
        {
            get
            {
                return this.retEvento;
            }
            set
            {
                this.retEvento = (TretEvento[])value;
            }
        }
    }

    public partial class TEnviNFe : NFeDownload.NFe.SchemaXML.ITEnviNFe
    {
        SchemaXML.ITNFe[] SchemaXML.ITEnviNFe.NFe
        {
            get
            {
                return this.NFe;
            }
            set
            {
                this.NFe = (TNFe[])value;
            }
        }
    }
    public partial class TRetEnviNFe : NFeDownload.NFe.SchemaXML.ITRetEnviNFe
    {
        SchemaXML.ITRetEnviNFeInfRec SchemaXML.ITRetEnviNFe.infRec
        {
            get
            {
                if (typeof(TRetEnviNFeInfRec) == this.Item.GetType())
                {
                    return (TRetEnviNFeInfRec)this.Item;
                }
                else
                {
                    throw new System.NotImplementedException();
                }
            }
            set
            {
                if (typeof(TRetEnviNFeInfRec) == this.Item.GetType())
                {
                    this.Item = (TRetEnviNFeInfRec)value;
                }
                else
                {
                    throw new System.NotImplementedException();
                }
            }
        }
    }
    public partial class TConsReciNFe : NFeDownload.NFe.SchemaXML.ITConsReciNFe { }
    public partial class TRetConsReciNFe : NFeDownload.NFe.SchemaXML.ITRetConsReciNFe
    {
        SchemaXML.ITProtNFe[] SchemaXML.ITRetConsReciNFe.protNFe
        {
            get
            {
                return this.protNFe;
            }
        }
    }
    public partial class TConsStatServ : NFeDownload.NFe.SchemaXML.ITConsStatServ { }
    public partial class TRetConsStatServ : NFeDownload.NFe.SchemaXML.ITRetConsStatServ { }
    public partial class TInutNFe : NFeDownload.NFe.SchemaXML.ITInutNFe
    {
        SchemaXML.ITInutNFeInfInut SchemaXML.ITInutNFe.infInut
        {
            get { return this.infInut; }
            set { this.infInut = (TInutNFeInfInut)value; }
        }
    }
    public partial class TRetInutNFe : NFeDownload.NFe.SchemaXML.ITRetInutNFe
    {
        SchemaXML.ITRetInutNFeInfInut SchemaXML.ITRetInutNFe.infInut
        {
            get { return this.infInut; }
        }
    }

    public partial class TRetInutNFeInfInut : NFeDownload.NFe.SchemaXML.ITRetInutNFeInfInut
    {

    }


    public partial class TConsSitNFe : NFeDownload.NFe.SchemaXML.ITConsSitNFe { }
    public partial class TRetConsSitNFe : NFeDownload.NFe.SchemaXML.ITRetConsSitNFe
    {
        SchemaXML.ITProtNFe SchemaXML.ITRetConsSitNFe.protNFe
        {
            get { return this.protNFe; }
        }

        SchemaXML.ITProcEvento[] SchemaXML.ITRetConsSitNFe.procEventoNFe
        {
            get { return this.procEventoNFe; }
        }

        SchemaXML.ITRetCancNFe_v200107 SchemaXML.ITRetConsSitNFe.retCancNFe
        {
            get
            {
                return null;
            }
            set
            {
                //throw new System.NotImplementedException();
            }
        }
    }

    public partial class TEvento : NFeDownload.NFe.SchemaXML.ITEvento
    {
        SchemaXML.ITEventoInfEvento SchemaXML.ITEvento.infEvento
        {
            get { return this.infEvento; }
            set { this.infEvento = (TEventoInfEvento)value; }
        }

        SchemaXML.ISignatureType SchemaXML.ITEvento.Signature
        {
            get { return this.Signature; }
        }
    }
    public partial class TEventoInfEvento : NFeDownload.NFe.SchemaXML.ITEventoInfEvento
    {

        SchemaXML.ITEventoInfEventoDetEvento SchemaXML.ITEventoInfEvento.detEvento
        {
            get
            {
                return this.detEvento;
            }
            set
            {
                this.detEvento = (TEventoInfEventoDetEvento)value;
            }
        }
    }

    public partial class TNfeProc : NFeDownload.NFe.SchemaXML.ITNfeProc
    {
        SchemaXML.ITNFe SchemaXML.ITNfeProc.NFe
        {
            get
            {
                return this.NFe;
            }
            set
            {
                this.NFe = (TNFe)value;
            }
        }

        SchemaXML.ITProtNFe SchemaXML.ITNfeProc.protNFe
        {
            get
            {
                return this.protNFe;
            }
            set
            {
                this.protNFe = (TProtNFe)value;
            }
        }
    }
    public partial class TProtNFe : NFeDownload.NFe.SchemaXML.ITProtNFe
    {
        SchemaXML.ITProtNFeInfProt SchemaXML.ITProtNFe.infProt
        {
            get { return this.infProt; }
        }
    }

    public partial class TProtNFeInfProt : NFeDownload.NFe.SchemaXML.ITProtNFeInfProt
    {

    }

    public partial class TProcEvento : NFeDownload.NFe.SchemaXML.ITProcEvento
    {

        SchemaXML.ITEvento SchemaXML.ITProcEvento.evento
        {
            get { return this.evento; }
            set { this.evento = (TEvento)value; }
        }

        SchemaXML.ITretEvento SchemaXML.ITProcEvento.retEvento
        {
            get
            {
                return this.retEvento;
            }
            set
            {
                this.retEvento = (TretEvento)value;
            }
        }
    }


    public partial class TNFe : NFeDownload.NFe.SchemaXML.ITNFe
    {
        NFeDownload.NFe.SchemaXML.ITNFeInfNFe NFeDownload.NFe.SchemaXML.ITNFe.infNFe
        {
            get
            {
                return this.infNFe;
            }
        }

        SchemaXML.ISignatureType SchemaXML.ITNFe.Signature
        {
            get { return this.Signature; }
        }
    }

    public partial class TNFeInfNFe : NFeDownload.NFe.SchemaXML.ITNFeInfNFe
    {
        SchemaXML.ITNFeInfNFeIde SchemaXML.ITNFeInfNFe.ide
        {
            get { return this.ide; }
        }

        SchemaXML.ITNFeInfNFeDest SchemaXML.ITNFeInfNFe.dest
        {
            get { return this.dest; }
        }

        SchemaXML.ITNFeInfNFeEmit SchemaXML.ITNFeInfNFe.emit
        {
            get { return this.emit; }
        }
    }

    public partial class TNFeInfNFeIde : NFeDownload.NFe.SchemaXML.ITNFeInfNFeIde
    {
        string SchemaXML.ITNFeInfNFeIde.dEmi
        {
            get { return this.dhEmi; }
        }

    }

    public partial class TNFeInfNFeDest : NFeDownload.NFe.SchemaXML.ITNFeInfNFeDest
    {

        SchemaXML.ITEndereco SchemaXML.ITNFeInfNFeDest.enderDest
        {
            get
            {
                return this.enderDest;
            }
        }
    }

    public partial class TNFeInfNFeEmit : NFeDownload.NFe.SchemaXML.ITNFeInfNFeEmit
    {
        SchemaXML.ITEnderEmi SchemaXML.ITNFeInfNFeEmit.enderEmit
        {
            get
            {
                return this.enderEmit;
            }
        }
    }

    public partial class TEndereco : NFeDownload.NFe.SchemaXML.ITEndereco
    {

    }

    public partial class TEnderEmi : NFeDownload.NFe.SchemaXML.ITEnderEmi
    {

    }
}


namespace NFeDownload.NFe.SchemaXML310
{

    public partial class TEventoInfEventoDetEvento : NFeDownload.NFe.SchemaXML.ITEventoInfEventoDetEvento
    {

    }

    public partial class TretEventoInfEvento : NFeDownload.NFe.SchemaXML.ITretEventoInfEvento
    {

    }

    public partial class TRetEnviNFeInfRec : NFeDownload.NFe.SchemaXML.ITRetEnviNFeInfRec
    {

    }

    public partial class TRetCancNFe_v200107InfCanc : NFeDownload.NFe.SchemaXML.ITRetCancNFe_v200107InfCanc
    {

        string SchemaXML.ITRetCancNFe_v200107InfCanc.nProt
        {
            get
            {
                return null;
            }
            set
            {
                //throw new System.NotImplementedException();
            }
        }
    }



    public partial class TRetCancNFe_v200107 : NFeDownload.NFe.SchemaXML.ITRetCancNFe_v200107
    {

        SchemaXML.ITRetCancNFe_v200107InfCanc SchemaXML.ITRetCancNFe_v200107.infCanc
        {
            get
            {
                return null;
            }
        }
    }

    public partial class TretEvento : NFeDownload.NFe.SchemaXML.ITretEvento
    {

        SchemaXML.ITretEventoInfEvento SchemaXML.ITretEvento.infEvento
        {
            get
            {
                return this.infEvento;
            }

            set
            {
                this.infEvento = (TretEventoInfEvento)value;
            }
        }
    }

    public partial class TRetCancNFe : NFeDownload.NFe.SchemaXML.ITRetCancNFe
    {

    }


    public partial class TCancNFe : NFeDownload.NFe.SchemaXML.ITCancNFe
    {

    }


    public partial class TInutNFeInfInut : NFeDownload.NFe.SchemaXML.ITInutNFeInfInut
    {

    }

    public partial class ReferenceType : NFeDownload.NFe.SchemaXML.IReferenceType
    {

    }

    public partial class SignedInfoType : NFeDownload.NFe.SchemaXML.ISignedInfoType
    {
        SchemaXML.IReferenceType SchemaXML.ISignedInfoType.Reference
        {
            get { return this.Reference; }
        }
    }

    public partial class SignatureType : NFeDownload.NFe.SchemaXML.ISignatureType
    {

        SchemaXML.ISignedInfoType SchemaXML.ISignatureType.SignedInfo
        {
            get
            {
                return this.SignedInfo;
            }

        }
    }

    public partial class TEnvEvento : NFeDownload.NFe.SchemaXML.ITEnvEvento
    {
        SchemaXML.ITEvento[] SchemaXML.ITEnvEvento.evento
        {
            get
            {
                return this.evento;
            }
            set
            {
                this.evento = (TEvento[])value;
            }
        }
    }
    public partial class TRetEnvEvento : NFeDownload.NFe.SchemaXML.ITRetEnvEvento
    {
        SchemaXML.ITretEvento[] SchemaXML.ITRetEnvEvento.retEvento
        {
            get
            {
                return this.retEvento;
            }
            set
            {
                this.retEvento = (TretEvento[])value;
            }
        }
    }

    public partial class TEnviNFe : NFeDownload.NFe.SchemaXML.ITEnviNFe
    {
        SchemaXML.ITNFe[] SchemaXML.ITEnviNFe.NFe
        {
            get
            {
                return this.NFe;
            }
            set
            {
                this.NFe = (TNFe[])value;
            }
        }
    }
    public partial class TRetEnviNFe : NFeDownload.NFe.SchemaXML.ITRetEnviNFe
    {
        SchemaXML.ITRetEnviNFeInfRec SchemaXML.ITRetEnviNFe.infRec
        {
            get
            {
                if (typeof(TRetEnviNFeInfRec) == this.Item.GetType())
                {
                    return (TRetEnviNFeInfRec)this.Item;
                }
                else
                {
                    throw new System.NotImplementedException();
                }
            }
            set
            {
                if (typeof(TRetEnviNFeInfRec) == this.Item.GetType())
                {
                    this.Item = (TRetEnviNFeInfRec)value;
                }
                else
                {
                    throw new System.NotImplementedException();
                }
            }
        }
    }
    public partial class TConsReciNFe : NFeDownload.NFe.SchemaXML.ITConsReciNFe { }
    public partial class TRetConsReciNFe : NFeDownload.NFe.SchemaXML.ITRetConsReciNFe
    {
        SchemaXML.ITProtNFe[] SchemaXML.ITRetConsReciNFe.protNFe
        {
            get
            {
                return this.protNFe;
            }
        }
    }
    public partial class TConsStatServ : NFeDownload.NFe.SchemaXML.ITConsStatServ { }
    public partial class TRetConsStatServ : NFeDownload.NFe.SchemaXML.ITRetConsStatServ { }
    public partial class TInutNFe : NFeDownload.NFe.SchemaXML.ITInutNFe
    {
        SchemaXML.ITInutNFeInfInut SchemaXML.ITInutNFe.infInut
        {
            get { return this.infInut; }
            set { this.infInut = (TInutNFeInfInut)value; }
        }
    }
    public partial class TRetInutNFe : NFeDownload.NFe.SchemaXML.ITRetInutNFe
    {
        SchemaXML.ITRetInutNFeInfInut SchemaXML.ITRetInutNFe.infInut
        {
            get { return this.infInut; }
        }
    }

    public partial class TRetInutNFeInfInut : NFeDownload.NFe.SchemaXML.ITRetInutNFeInfInut
    {

    }


    public partial class TConsSitNFe : NFeDownload.NFe.SchemaXML.ITConsSitNFe { }
    public partial class TRetConsSitNFe : NFeDownload.NFe.SchemaXML.ITRetConsSitNFe
    {
        SchemaXML.ITProtNFe SchemaXML.ITRetConsSitNFe.protNFe
        {
            get { return this.protNFe; }
        }

        SchemaXML.ITProcEvento[] SchemaXML.ITRetConsSitNFe.procEventoNFe
        {
            get { return this.procEventoNFe; }
        }

        SchemaXML.ITRetCancNFe_v200107 SchemaXML.ITRetConsSitNFe.retCancNFe
        {
            get
            {
                return null;
            }
            set
            {
                //throw new System.NotImplementedException();
            }
        }
    }

    public partial class TEvento : NFeDownload.NFe.SchemaXML.ITEvento
    {
        SchemaXML.ITEventoInfEvento SchemaXML.ITEvento.infEvento
        {
            get { return this.infEvento; }
            set { this.infEvento = (TEventoInfEvento)value; }
        }

        SchemaXML.ISignatureType SchemaXML.ITEvento.Signature
        {
            get { return this.Signature; }
        }
    }
    public partial class TEventoInfEvento : NFeDownload.NFe.SchemaXML.ITEventoInfEvento
    {

        SchemaXML.ITEventoInfEventoDetEvento SchemaXML.ITEventoInfEvento.detEvento
        {
            get
            {
                return this.detEvento;
            }
            set
            {
                this.detEvento = (TEventoInfEventoDetEvento)value;
            }
        }
    }

    public partial class TNfeProc : NFeDownload.NFe.SchemaXML.ITNfeProc
    {
        SchemaXML.ITNFe SchemaXML.ITNfeProc.NFe
        {
            get
            {
                return this.NFe;
            }
            set
            {
                this.NFe = (TNFe)value;
            }
        }

        SchemaXML.ITProtNFe SchemaXML.ITNfeProc.protNFe
        {
            get
            {
                return this.protNFe;
            }
            set
            {
                this.protNFe = (TProtNFe)value;
            }
        }
    }
    public partial class TProtNFe : NFeDownload.NFe.SchemaXML.ITProtNFe
    {
        SchemaXML.ITProtNFeInfProt SchemaXML.ITProtNFe.infProt
        {
            get { return this.infProt; }
        }
    }

    public partial class TProtNFeInfProt : NFeDownload.NFe.SchemaXML.ITProtNFeInfProt
    {

    }

    public partial class TProcEvento : NFeDownload.NFe.SchemaXML.ITProcEvento
    {

        SchemaXML.ITEvento SchemaXML.ITProcEvento.evento
        {
            get { return this.evento; }
            set { this.evento = (TEvento)value; }
        }

        SchemaXML.ITretEvento SchemaXML.ITProcEvento.retEvento
        {
            get
            {
                return this.retEvento;
            }
            set
            {
                this.retEvento = (TretEvento)value;
            }
        }
    }


    public partial class TNFe : NFeDownload.NFe.SchemaXML.ITNFe
    {
        NFeDownload.NFe.SchemaXML.ITNFeInfNFe NFeDownload.NFe.SchemaXML.ITNFe.infNFe
        {
            get
            {
                return this.infNFe;
            }
        }

        SchemaXML.ISignatureType SchemaXML.ITNFe.Signature
        {
            get { return this.Signature; }
        }
    }

    public partial class TNFeInfNFe : NFeDownload.NFe.SchemaXML.ITNFeInfNFe
    {
        SchemaXML.ITNFeInfNFeIde SchemaXML.ITNFeInfNFe.ide
        {
            get { return this.ide; }
        }

        SchemaXML.ITNFeInfNFeDest SchemaXML.ITNFeInfNFe.dest
        {
            get { return this.dest; }
        }

        SchemaXML.ITNFeInfNFeEmit SchemaXML.ITNFeInfNFe.emit
        {
            get { return this.emit; }
        }
    }

    public partial class TNFeInfNFeIde : NFeDownload.NFe.SchemaXML.ITNFeInfNFeIde
    {

        string SchemaXML.ITNFeInfNFeIde.dEmi
        {
            get { return this.dhEmi; }
        }

    }

    public partial class TNFeInfNFeDest : NFeDownload.NFe.SchemaXML.ITNFeInfNFeDest
    {

        SchemaXML.ITEndereco SchemaXML.ITNFeInfNFeDest.enderDest
        {
            get
            {
                return this.enderDest;
            }
        }
    }

    public partial class TNFeInfNFeEmit : NFeDownload.NFe.SchemaXML.ITNFeInfNFeEmit
    {
        SchemaXML.ITEnderEmi SchemaXML.ITNFeInfNFeEmit.enderEmit
        {
            get
            {
                return this.enderEmit;
            }
        }
    }

    public partial class TEndereco : NFeDownload.NFe.SchemaXML.ITEndereco
    {

    }

    public partial class TEnderEmi : NFeDownload.NFe.SchemaXML.ITEnderEmi
    {

    }
}

#endregion
