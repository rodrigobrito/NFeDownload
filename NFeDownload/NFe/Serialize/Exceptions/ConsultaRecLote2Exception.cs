using System.Security.Cryptography.X509Certificates;

namespace NFeDownload.NFe.Serialize.Exceptions
{
    public class ConsultaRecLote2Exception: System.Exception
    {
        TConsReciNFe consReciNFe;
        TRetEnviNFe retEnvNFe;
        X509Certificate2 cert;

        public TConsReciNFe ConsReciNFe
        {
            get { return consReciNFe; }
            set { consReciNFe = value; }
        }        

        public TRetEnviNFe RetEnvNFe
        {
            get { return retEnvNFe; }
            set { retEnvNFe = value; }
        }        

        public X509Certificate2 Cert
        {
            get { return cert; }
            set { cert = value; }
        }

        public ConsultaRecLote2Exception(string message, TConsReciNFe consReciNFe, TRetEnviNFe retEnvNFe, X509Certificate2 cert): base(message)
        {       
            this.consReciNFe = consReciNFe;
            this.retEnvNFe = retEnvNFe;
            this.cert = cert;
        }
    }
}
