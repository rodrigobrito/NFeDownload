using System.Xml.Serialization;
using System;

namespace NFeDownload.NFe.SchemaXML
{
    public enum TServico
    {
        [ClasseServico(value = "RecepcaoEvento")]
        RecepcaoEvento,
        [ClasseServico(value = "NfeInutilizacao2")]
        Inutilizacao,
        [ClasseServico(value = "NfeStatusServico2")]
        Status,
        [ClasseServico(value = "NfeConsulta2")]
        Consulta,
        [ClasseServico(value = "NfeRecepcao2")]
        Recepcao,
        [ClasseServico(value = "NfeRetRecepcao2")]
        RetRecepcao,
        [ClasseServico(value = "NfeAutorizacao")]
        Autorizacao,
        [ClasseServico(value = "NfeRetAutorizacao")]
        RetAutorizacao,
    }

    public static class Factory
    {
        public const string Schema200ns = "NFeDownload.NFe.SchemaXML200.";
        public const string Schema300ns = "NFeDownload.NFe.SchemaXML300.";
        public const string Schema310ns = "NFeDownload.NFe.SchemaXML310.";


        public static Type GetXMLType(VersaoNFe version, string stType)
        {
            Type tipo = null;
            if (version == VersaoNFe.v200)
            {
                tipo = Type.GetType(Schema200ns + stType);
            }
            else if (version == VersaoNFe.v300)
            {
                tipo = Type.GetType(Schema300ns + stType);
            }
            else if (version == VersaoNFe.v310)
            {
                tipo = Type.GetType(Schema310ns + stType);
            }
            else if (version == VersaoNFe.Integracao)
            {
                tipo = Type.GetType("SchemaXML." + stType);
            }
            else
            {
                throw new Exception("Tipo não pode ser definido.");
            }
            return tipo;
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe", IncludeInSchema = false)]
    public enum ITCTypeCNPJCPFDest
    {

        /// <remarks/>
        CNPJDest,

        /// <remarks/>
        CPFDest,
    }



    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TCancNFeInfCancXServ
    {

        /// <remarks/>
        CANCELAR,
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TConsStatServXServ
    {

        /// <remarks/>
        STATUS,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TConsSitNFeXServ
    {

        /// <remarks/>
        CONSULTAR,
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe", IncludeInSchema = false)]
    public enum ITCTypeCNPJCPF
    {

        /// <remarks/>
        CNPJ,

        /// <remarks/>
        CPF,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TEventoInfEventoDetEventoXCondUso
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(@"A Carta de Correção é disciplinada pelo § 1º-A do art. 7º do Convênio S/N, de 15 de dezembro de 1970 e pode ser utilizada para regularização de erro ocorrido na emissão de documento fiscal, desde que o erro não esteja relacionado com: I - as variáveis que determinam o valor do imposto tais como: base de cálculo, alíquota, diferença de preço, quantidade, valor da operação ou da prestação; II - a correção de dados cadastrais que implique mudança do remetente ou do destinatário; III - a data de emissão ou de saída.")]
        ACartadeCorreçãoédisciplinadapelo1ºAdoart7ºdoConvênioSNde15dedezembrode1970epodeserutilizadapararegularizaçãodeerroocorridonaemissãodedocumentofiscaldesdequeoerronãoestejarelacionadocomIasvariáveisquedeterminamovalordoimpostotaiscomobasedecálculoalíquotadiferençadepreçoquantidadevalordaoperaçãooudaprestaçãoIIacorreçãodedadoscadastraisqueimpliquemudançadoremetenteoudodestinatárioIIIadatadeemissãooudesaída,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute(@"A Carta de Correcao e disciplinada pelo paragrafo 1o-A do art. 7o do Convenio S/N, de 15 de dezembro de 1970 e pode ser utilizada para regularizacao de erro ocorrido na emissao de documento fiscal, desde que o erro nao esteja relacionado com: I - as variaveis que determinam o valor do imposto tais como: base de calculo, aliquota, diferenca de preco, quantidade, valor da operacao ou da prestacao; II - a correcao de dados cadastrais que implique mudanca do remetente ou do destinatario; III - a data de emissao ou de saida.")]
        ACartadeCorrecaoedisciplinadapeloparagrafo1oAdoart7odoConvenioSNde15dedezembrode1970epodeserutilizadapararegularizacaodeerroocorridonaemissaodedocumentofiscaldesdequeoerronaoestejarelacionadocomIasvariaveisquedeterminamovalordoimpostotaiscomobasedecalculoaliquotadiferencadeprecoquantidadevalordaoperacaooudaprestacaoIIacorrecaodedadoscadastraisqueimpliquemudancadoremetenteoudodestinatarioIIIadatadeemissaooudesaida,
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TEventoInfEventoDetEventoVersao
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1.00")]
        Item100,
    }





    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TEventoInfEventoVerEvento
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1.00")]
        Item100,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TInutNFeInfInutXServ
    {

        /// <remarks/>
        INUTILIZAR,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TMod
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("55")]
        Item55,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("65")]
        Item65,
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TVerConsSitNFe
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2.01")]
        Item201,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3.00")]
        Item300,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3.10")]
        Item310,

    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TEventoInfEventoDetEventoDescEvento
    {

        [System.Xml.Serialization.XmlEnumAttribute("CT-e Autorizado")]
        CTeAutorizado,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Carta de Correção")]
        CartadeCorreção,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Carta de Correcao")]
        CartadeCorrecao,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Cancelamento")]
        Cancelamento,
    }


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TEventoInfEventoTpEvento
    {

        [System.ComponentModel.Description("CT-e Autorizado")]
        [System.Xml.Serialization.XmlEnumAttribute("610600")]
        CTeAutorizado,


        /// <remarks/>
        [System.ComponentModel.Description("Carta de Correção")]
        [System.Xml.Serialization.XmlEnumAttribute("110110")]
        CartaCorrecao,

        /// <remarks/>
        [System.ComponentModel.Description("Cancelamento")]
        [System.Xml.Serialization.XmlEnumAttribute("110111")]
        Cancelamento,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TNFeInfNFeIdeTpEmis
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        [System.ComponentModel.Description("Normal")]
        Normal = 1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        [System.ComponentModel.Description("Contigência FS")]
        ContigenciaFS = 2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        [System.ComponentModel.Description("Contigência SCAN")]
        ContigenciaSCAN = 3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        [System.ComponentModel.Description("Contigência DPEC")]
        ContigenciaDPEC = 4,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        [System.ComponentModel.Description("Contigência FS-DA")]
        ContigenciaFSDA = 5,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6")]
        [System.ComponentModel.Description("Contingência SVC-AN")]
        ContingenciaSVCAN = 6,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("7")]
        [System.ComponentModel.Description("Contingência SVC-RS")]
        ContingenciaSVCRS = 7,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("9")]
        [System.ComponentModel.Description("Contingência off-line da NFC-e")]
        ContingenciaOffLineNFCe = 9,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TAmb
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        [System.ComponentModel.Description("Produção")]
        Producao = 1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        [System.ComponentModel.Description("Homologação")]
        Homologacao = 2,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TCOrgaoIBGE
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("11")]
        Item11,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12")]
        Item12,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("13")]
        Item13,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("14")]
        Item14,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15")]
        Item15,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("16")]
        Item16,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17")]
        Item17,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("21")]
        Item21,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("22")]
        Item22,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("23")]
        Item23,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("24")]
        Item24,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("25")]
        Item25,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("26")]
        Item26,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("27")]
        Item27,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("28")]
        Item28,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("29")]
        Item29,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("31")]
        Item31,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("32")]
        Item32,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("33")]
        Item33,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("35")]
        Item35,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("41")]
        Item41,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("42")]
        Item42,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("43")]
        Item43,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("50")]
        Item50,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("51")]
        Item51,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("52")]
        Item52,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("53")]
        Item53,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("90")]
        Item90,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("91")]
        Item91,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TCodUfIBGE
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("11")]
        [System.ComponentModel.Description("Rondônia")]
        [AtendidoPor(value = "SVRS")]
        [SVC_AtendidoPor(value = "SVCAN")]
        Rondonia = 11,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12")]
        [System.ComponentModel.Description("Acre")]
        [AtendidoPor(value = "SVRS")]
        [SVC_AtendidoPor(value = "SVCAN")]
        Acre = 12,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("13")]
        [System.ComponentModel.Description("Amazonas")]
        [AtendidoPor(value = "AM")]
        [SVC_AtendidoPor(value = "SVCRS")]
        Amazonas = 13,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("14")]
        [System.ComponentModel.Description("Roraima")]
        [AtendidoPor(value = "SVRS")]
        [SVC_AtendidoPor(value = "SVCAN")]
        Roraima = 14,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("15")]
        [System.ComponentModel.Description("Pará")]
        [AtendidoPor(value = "SVAN")]
        [SVC_AtendidoPor(value = "SVCRS")]
        Para = 15,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("16")]
        [System.ComponentModel.Description("Amapá")]
        [AtendidoPor(value = "SVRS")]
        [SVC_AtendidoPor(value = "SVCAN")]
        Amapa = 16,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("17")]
        [System.ComponentModel.Description("Tocantins")]
        [AtendidoPor(value = "SVRS")]
        [SVC_AtendidoPor(value = "SVCAN")]
        Tocantins = 17,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("21")]
        [System.ComponentModel.Description("Maranhão")]
        [AtendidoPor(value = "SVAN")]
        [SVC_AtendidoPor(value = "SVCRS")]
        Maranhao = 21,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("22")]
        [System.ComponentModel.Description("Piauí")]
        [AtendidoPor(value = "SVAN")]
        [SVC_AtendidoPor(value = "SVCRS")]
        Piaui = 22,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("23")]
        [System.ComponentModel.Description("Ceará")]
        [AtendidoPor(value = "CE")]
        [SVC_AtendidoPor(value = "SVCRS")]
        Ceara = 23,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("24")]
        [System.ComponentModel.Description("Rio Grande do Norte")]
        [AtendidoPor(value = "SVRS")]
        [SVC_AtendidoPor(value = "SVCAN")]
        RioGrandedoNorte = 24,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("25")]
        [System.ComponentModel.Description("Paraíba")]
        [AtendidoPor(value = "SVRS")]
        [SVC_AtendidoPor(value = "SVCAN")]
        Paraiba = 25,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("26")]
        [System.ComponentModel.Description("Pernambuco")]
        [AtendidoPor(value = "PE")]
        [SVC_AtendidoPor(value = "SVCRS")]
        Pernambuco = 26,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("27")]
        [System.ComponentModel.Description("Alagoas")]
        [AtendidoPor(value = "SVRS")]
        [SVC_AtendidoPor(value = "SVCAN")]
        Alagoas = 27,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("28")]
        [System.ComponentModel.Description("Sergipe")]
        [AtendidoPor(value = "SVRS")]
        [SVC_AtendidoPor(value = "SVCAN")]
        Sergipe = 28,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("29")]
        [System.ComponentModel.Description("Bahia")]
        [AtendidoPor(value = "BA")]
        [SVC_AtendidoPor(value = "SVCRS")]
        Bahia = 29,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("31")]
        [System.ComponentModel.Description("Minas Gerais")]
        [AtendidoPor(value = "MG")]
        [SVC_AtendidoPor(value = "SVCAN")]
        MinasGerais = 31,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("32")]
        [System.ComponentModel.Description("Espírito Santo")]
        [AtendidoPor(value = "SVRS")]
        [SVC_AtendidoPor(value = "SVCAN")]
        EspiritoSanto = 32,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("33")]
        [System.ComponentModel.Description("Rio de Janeiro")]
        [AtendidoPor(value = "SVRS")]
        [SVC_AtendidoPor(value = "SVCAN")]
        RiodeJaneiro = 33,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("35")]
        [System.ComponentModel.Description("São Paulo")]
        [AtendidoPor(value = "SP")]
        [SVC_AtendidoPor(value = "SVCAN")]
        SaoPaulo = 35,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("41")]
        [System.ComponentModel.Description("Paraná")]
        [AtendidoPor(value = "PR")]
        [SVC_AtendidoPor(value = "SVCRS")]
        Parana = 41,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("42")]
        [System.ComponentModel.Description("Santa Catarina")]
        [AtendidoPor(value = "SVRS")]
        [SVC_AtendidoPor(value = "SVCAN")]
        SantaCatarina = 42,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("43")]
        [System.ComponentModel.Description("Rio Grande do Sul")]
        [AtendidoPor(value = "RS")]
        [SVC_AtendidoPor(value = "SVCAN")]
        RioGrandedoSul = 43,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("50")]
        [System.ComponentModel.Description("Mato Grosso do Sul")]
        [AtendidoPor(value = "MS")]
        [SVC_AtendidoPor(value = "SVCRS")]
        MatoGrossodoSul = 44,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("51")]
        [System.ComponentModel.Description("Mato Grosso")]
        [AtendidoPor(value = "MT")]
        [SVC_AtendidoPor(value = "SVCRS")]
        MatoGrosso = 51,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("52")]
        [System.ComponentModel.Description("Goiás")]
        [AtendidoPor(value = "GO")]
        [SVC_AtendidoPor(value = "SVCRS")]
        Goias = 52,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("53")]
        [System.ComponentModel.Description("Distrito Federal")]
        [AtendidoPor(value = "SVRS")]
        [SVC_AtendidoPor(value = "SVCAN")]
        DistritoFederal = 53,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TUfEmi
    {

        /// <remarks/>
        AC,

        /// <remarks/>
        AL,

        /// <remarks/>
        AM,

        /// <remarks/>
        AP,

        /// <remarks/>
        BA,

        /// <remarks/>
        CE,

        /// <remarks/>
        DF,

        /// <remarks/>
        ES,

        /// <remarks/>
        GO,

        /// <remarks/>
        MA,

        /// <remarks/>
        MG,

        /// <remarks/>
        MS,

        /// <remarks/>
        MT,

        /// <remarks/>
        PA,

        /// <remarks/>
        PB,

        /// <remarks/>
        PE,

        /// <remarks/>
        PI,

        /// <remarks/>
        PR,

        /// <remarks/>
        RJ,

        /// <remarks/>
        RN,

        /// <remarks/>
        RO,

        /// <remarks/>
        RR,

        /// <remarks/>
        RS,

        /// <remarks/>
        SC,

        /// <remarks/>
        SE,

        /// <remarks/>
        SP,

        /// <remarks/>
        TO,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public enum TUf
    {

        /// <remarks/>
        AC,

        /// <remarks/>
        AL,

        /// <remarks/>
        AM,

        /// <remarks/>
        AP,

        /// <remarks/>
        BA,

        /// <remarks/>
        CE,

        /// <remarks/>
        DF,

        /// <remarks/>
        ES,

        /// <remarks/>
        GO,

        /// <remarks/>
        MA,

        /// <remarks/>
        MG,

        /// <remarks/>
        MS,

        /// <remarks/>
        MT,

        /// <remarks/>
        PA,

        /// <remarks/>
        PB,

        /// <remarks/>
        PE,

        /// <remarks/>
        PI,

        /// <remarks/>
        PR,

        /// <remarks/>
        RJ,

        /// <remarks/>
        RN,

        /// <remarks/>
        RO,

        /// <remarks/>
        RR,

        /// <remarks/>
        RS,

        /// <remarks/>
        SC,

        /// <remarks/>
        SE,

        /// <remarks/>
        SP,

        /// <remarks/>
        TO,

        /// <remarks/>
        EX,
    }


    public enum VersaoNFe
    {
        v200 = 1,
        v300 = 2,
        v310 = 3,
        Integracao
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.rochadigital.com/opennfe")]
    public enum TIntegracaoVersao
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1.00")]
        Item100,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.rochadigital.com/opennfe")]
    public enum TSimNao
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        [System.ComponentModel.Description("Não")]
        Nao,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        [System.ComponentModel.Description("Sim")]
        Sim
    }


    /// <summary>
    /// 
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.rochadigital.com/opennfe")]
    [System.Xml.Serialization.XmlRootAttribute("Integracao", Namespace = "http://www.rochadigital.com/opennfe", IsNullable = false)]
    public partial class TIntegracao
    {

        private object itemField;
        private TIntegracaoVersao versaoField;

        [System.Xml.Serialization.XmlElementAttribute("Impressao", typeof(TImpressao))]
        [System.Xml.Serialization.XmlElementAttribute("HeartBeat", typeof(THeartBeat))]
        public object item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public TIntegracaoVersao versao
        {
            get
            {
                return this.versaoField;
            }
            set
            {
                this.versaoField = value;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.rochadigital.com/opennfe")]
    public partial class TImpressao
    {

        private string chNFeField;

        public string chNFe
        {
            get
            {
                return this.chNFeField;
            }
            set
            {
                this.chNFeField = value;
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.rochadigital.com/opennfe")]
    public partial class THeartBeat
    {
        private System.DateTime horarioField;
        private TSimNao emExecucaoField;
        private System.DateTime horarioExecucaoField;
        private bool horarioExecucaoSpecifiedField;

        public System.DateTime horario
        {
            get
            {
                return this.horarioField;
            }
            set
            {
                this.horarioField = value;
            }
        }

        public TSimNao emExecucao
        {
            get
            {
                return this.emExecucaoField;
            }
            set
            {
                this.emExecucaoField = value;
            }
        }

        public System.DateTime horarioExecucao
        {
            get
            {
                return this.horarioExecucaoField;
            }
            set
            {
                this.horarioExecucaoField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool horarioExecucaoSpecified
        {
            get
            {
                return this.horarioExecucaoSpecifiedField;
            }
            set
            {
                this.horarioExecucaoSpecifiedField = value;
            }
        }

    }
}