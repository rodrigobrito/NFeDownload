using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFeDownload.Download
{
    public class DownloadedHtmlData
    {

        public IList<PostResultItem> DadosNfe { get; set; }

        public List<IList<PostResultItem>> ScienceOperations { get; set; }

        public IList<PostResultItem> DadosEmitente { get; set; }

        public IList<PostResultItem> DadosDestinatario { get; set; }

        public IList<Produto> Products { get; set; }

        public IList<PostResultItem> Totais { get; set; }

        public IList<PostResultItem> DadosTransporte { get; set; }

        public IList<PostResultItem> DadosCobranca { get; set; }

        public IList<PostResultItem> InformacoesAdicionais { get; set; }

        public IList<PostResultItem> NotaFiscalAvulsa { get; set; }
    }
}
