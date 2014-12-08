using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace NFeDownload.Download
{
    public class HtmlHelper
    {
        private const string UserAgent = "NFe XML Generator";
        private CookieContainer sessionCookie;

        public PostItems GetItemsForPost()
        {
            ////Get items for post
            var request = (HttpWebRequest)WebRequest.Create("http://www.nfe.fazenda.gov.br/portal/consulta.aspx?tipoConsulta=completa&tipoConteudo=XbSeqxE8pl8=");
            sessionCookie = new CookieContainer();
            request.CookieContainer = sessionCookie;
            request.UserAgent = UserAgent;
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();

            var doc = new HtmlDocument();
            doc.Load(responseStream);

            var itemsForPost = new PostItems
            {
                EventTarget = string.Empty,
                EventArgument = string.Empty,
                PalavraChave = string.Empty
            };

            var formElement = doc.GetElementbyId("aspnetForm");
            if (formElement != null)
            {
                itemsForPost.FormAction = formElement.GetAttributeValue("action", "nodata");
            }
            else
            {
                throw new InvalidOperationException("Resposta sem um formulário web.");
            }

            var capcthElement = doc.GetElementbyId("ContentPlaceHolder1_imgCaptcha");
            if (capcthElement != null)
                itemsForPost.Base64Image = capcthElement.GetAttributeValue("src", "nosrc");

            var viewStateElement = doc.GetElementbyId("__VIEWSTATE");
            if (viewStateElement != null)
                itemsForPost.ViewState = viewStateElement.GetAttributeValue("value", "novalue");


            var viewStateGeneratorElement = doc.GetElementbyId("__VIEWSTATEGENERATOR");
            if (viewStateGeneratorElement != null)
                itemsForPost.ViewStateGenerator = viewStateGeneratorElement.GetAttributeValue("value", "novalue");


            var eventValidationElement = doc.GetElementbyId("__EVENTVALIDATION");
            if (eventValidationElement != null)
                itemsForPost.EventValidation = eventValidationElement.GetAttributeValue("value", "novalue");

            itemsForPost.BtnConsultar = "Continuar";

            var eventTokenElement = doc.GetElementbyId("ContentPlaceHolder1_token");
            if (eventTokenElement != null)
                itemsForPost.Token = eventTokenElement.GetAttributeValue("value", "novalue");

            var captchaSomElement = doc.GetElementbyId("ContentPlaceHolder1_captchaSom");
            if (captchaSomElement != null)
                itemsForPost.CaptchaSom = captchaSomElement.GetAttributeValue("value", "novalue");

            itemsForPost.CommomToolkitScripts = "1";

            return itemsForPost;
        }

        public void ValidatePost(PostItems itemsForPost)
        {
            if (!string.IsNullOrWhiteSpace(itemsForPost.EventTarget))
            {
                throw new InvalidOperationException("__EVENTTARGET deve ser vazio!");
            }

            if (!string.IsNullOrWhiteSpace(itemsForPost.EventArgument))
            {
                throw new InvalidOperationException("__EVENTARGUMENT deve ser vazio!");
            }

            if (string.IsNullOrWhiteSpace(itemsForPost.ViewState))
            {
                throw new InvalidOperationException("__VIEWSTATE deve possuir um valor!");
            }

            if (string.IsNullOrWhiteSpace(itemsForPost.EventValidation))
            {
                throw new InvalidOperationException("__EVENTVALIDATION deve possuir um valor!");
            }

            if (!string.IsNullOrWhiteSpace(itemsForPost.PalavraChave))
            {
                throw new InvalidOperationException("ctl00$txtPalavraChave deve ser vazia!");
            }

            if (string.IsNullOrWhiteSpace(itemsForPost.ChaveAcessoCompleta))
            {
                throw new InvalidOperationException("A chave de acesso não foi informada!");
            }

            if (string.IsNullOrWhiteSpace(itemsForPost.Captcha))
            {
                throw new InvalidOperationException("O captcha não foi informado!");
            }

            if (itemsForPost.CommomToolkitScripts != "1")
            {
                throw new InvalidOperationException("hiddenInputToUpdateATBuffer_CommonToolkitScripts possui um valor inválido.");
            }
        }
        
        private static void Check(HtmlNode htmlNode)
        {
            if (htmlNode == null)
                throw new NullReferenceException("Não foi possível obter os dados de http://www.nfe.fazenda.gov.br. Por favor, tente novamente.");
        }

        public DownloadedHtmlData Post(PostItems itemsForPost)
        {
            ValidatePost(itemsForPost);

            var postData = ComposePost(itemsForPost);
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            var request = (HttpWebRequest)WebRequest.Create("http://www.nfe.fazenda.gov.br/portal/consulta.aspx?tipoConsulta=completa&tipoConteudo=XbSeqxE8pl8=");
            request.CookieContainer = sessionCookie;
            request.UserAgent = UserAgent;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            var dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            var pageResponse = request.GetResponse();

            var docUserWebPage = new HtmlDocument();
            docUserWebPage.Load(pageResponse.GetResponseStream());
            var documentText = docUserWebPage.DocumentNode.InnerHtml;
            if (documentText.Contains("C&#243;digo da Imagem inv&#225;lido."))
            {
                throw new InvalidDataException("Código da Imagem inválido. Tente novamente.");
            }

            if (documentText.Contains("NF-e INEXISTENTE na base nacional"))
            {
                throw new InvalidDataException("NF-e INEXISTENTE na base nacional.");
            }

            var printRequest = (HttpWebRequest)WebRequest.Create("http://www.nfe.fazenda.gov.br/portal/consultaImpressao.aspx?tipoConsulta=completa");
            printRequest.CookieContainer = sessionCookie;
            printRequest.UserAgent = UserAgent;
            printRequest.ContentType = "application/x-www-form-urlencoded";
            var printResponse = printRequest.GetResponse();
            var printResponseStream = printResponse.GetResponseStream();

            var printUserPage = new HtmlDocument();
            printUserPage.Load(printResponseStream);

            var result = new DownloadedHtmlData();
            var spanChaveAcesso = printUserPage.GetElementbyId("lblChaveAcesso");
            Check(spanChaveAcesso);
            result.ChaveAcessso = spanChaveAcesso.InnerText.Trim();
            result.DadosNfe = GetDataItems(printUserPage, "NFe");
            var operationScience = printUserPage.DocumentNode.Descendants().Where(e => e.Id.Contains("CienciaOperacao")).ToList();
            var scienceOperationsList = operationScience.Select(science => GetDataItems(printUserPage, science.Id)).ToList();
            result.ScienceOperations = scienceOperationsList;
            result.DadosEmitente = GetDataItems(printUserPage, "Emitente");
            result.DadosDestinatario = GetDataItems(printUserPage, "DestRem");
            result.Products = GetProducts(printUserPage);
            result.Totais = GetDataItems(printUserPage, "Totais");
            result.DadosTransporte = GetDataItems(printUserPage, "Transporte");
            result.DadosCobranca = GetDataItems(printUserPage, "Cobranca");
            result.InformacoesAdicionais = GetDataItems(printUserPage, "Inf");
            result.NotaFiscalAvulsa = GetDataItems(printUserPage, "Avulsa");

            return result;
        }

        public string ComposePost(PostItems itemsForPost)
        {
            var outgoingQueryString = HttpUtility.ParseQueryString(String.Empty);
            outgoingQueryString.Add("__EVENTTARGET", itemsForPost.EventTarget);
            outgoingQueryString.Add("__EVENTARGUMENT", itemsForPost.EventArgument);
            outgoingQueryString.Add("__VIEWSTATE", itemsForPost.ViewState);
            outgoingQueryString.Add("__VIEWSTATEGENERATOR", itemsForPost.ViewStateGenerator);
            outgoingQueryString.Add("__EVENTVALIDATION", itemsForPost.EventValidation);
            outgoingQueryString.Add("ctl00$txtPalavraChave", itemsForPost.PalavraChave);
            outgoingQueryString.Add("ctl00$ContentPlaceHolder1$txtChaveAcessoCompleta", itemsForPost.ChaveAcessoCompleta);
            outgoingQueryString.Add("ctl00$ContentPlaceHolder1$txtCaptcha", itemsForPost.Captcha);
            outgoingQueryString.Add("ctl00$ContentPlaceHolder1$btnConsultar", itemsForPost.BtnConsultar);
            outgoingQueryString.Add("ctl00$ContentPlaceHolder1$token", itemsForPost.Token);
            outgoingQueryString.Add("ctl00$ContentPlaceHolder1$captchaSom", itemsForPost.CaptchaSom);
            outgoingQueryString.Add("hiddenInputToUpdateATBuffer_CommonToolkitScripts", itemsForPost.CommomToolkitScripts);
            return outgoingQueryString.ToString();
        }

        public IList<PostResultItem> GetDataItems(HtmlDocument doc, string div)
        {
            var dataItems = new List<PostResultItem>();
            var tablesWorked = new List<HtmlNode>();
            var nfeDiv = doc.GetElementbyId(div);
            Check(nfeDiv);

            var count = 1;
            foreach (var fieldset in nfeDiv.Descendants().Where(d => d.Name.ToLower() == "fieldset").ToList())
            {
                var legend = fieldset.Descendants().FirstOrDefault(d => d.Name.ToLower() == "legend");
                var tables = fieldset.Descendants().Where(d => d.Name.ToLower() == "table").ToList();
                var eEventosNfe = false;
                if (legend != null)
                    eEventosNfe = legend.InnerText.Trim().Contains("Situação Atual:");

                foreach (var table in tables)
                {
                    if (!tablesWorked.Contains(table))
                    {
                        if (table != null)
                        {
                            var trs = table.Descendants().Where(e => e.Name.ToLower() == "tr").ToList();

                            if (!eEventosNfe)
                            {
                                foreach (var tr in trs)
                                {
                                    var tds = tr.Descendants().Where(e => e.Name.ToLower() == "td").ToList();
                                    foreach (var td in tds)
                                    {
                                        if (legend != null)
                                        {
                                            var dataItem = new PostResultItem
                                            {
                                                Fieldset = count,
                                                Legend = legend.InnerText.Trim(),
                                                Div = div
                                            };

                                            var label = td.Descendants().FirstOrDefault(d => d.Name.ToLower() == "label");
                                            var span = td.Descendants().FirstOrDefault(d => d.Name.ToLower() == "span");

                                            if (label != null && span != null && !string.IsNullOrWhiteSpace(label.InnerText))
                                            {
                                                dataItem.AttributeName = label.InnerText.Trim();
                                                dataItem.AttributeValue = span.InnerText.Trim();
                                                dataItems.Add(dataItem);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 1; i < trs.Count; i++)
                                {
                                    var subNodeScience = trs[i].Descendants().FirstOrDefault(e => e.Id.Contains("CienciaOperacao"));

                                    if (subNodeScience != null)
                                        subNodeScience.Remove();

                                    var tds = trs[i].Descendants().Where(e => e.Name.ToLower() == "td").ToList();
                                    var position = 0;
                                    foreach (var td in tds)
                                    {
                                        var dataItem = new PostResultItem();
                                        switch (position)
                                        {
                                            case 0:
                                                dataItem.AttributeName = "Eventos da NF-e";
                                                break;
                                            case 1:
                                                dataItem.AttributeName = "Protocolo";
                                                break;
                                            case 2:
                                                dataItem.AttributeName = "Data / Hora";
                                                break;
                                            case 3:
                                                dataItem.AttributeName = "Data / Hora AN";
                                                break;
                                        }

                                        var span = td.Descendants().FirstOrDefault(d => d.Name.ToLower() == "span");
                                        if (span != null)
                                            dataItem.AttributeValue = span.InnerText.Trim();
                                        dataItem.Fieldset = count;
                                        dataItem.Legend = legend.InnerText.Trim();
                                        dataItem.Div = div;
                                        dataItems.Add(dataItem);
                                        position++;
                                    }
                                }
                                eEventosNfe = false;
                            }
                            tablesWorked.Add(table);
                        }
                    }
                }
                count++;
            }

            return dataItems.OrderBy(d => d.Fieldset).Distinct().ToList();
        }

        public IList<Produto> GetProducts(HtmlDocument doc)
        {
            var products = new List<Produto>();
            Produto product = null;

            var prdDiv = doc.GetElementbyId("Prod");
            var tables = prdDiv.Descendants().Where(d => d.Name.ToLower() == "table"
                && d.GetAttributeValue("class", "novalue").Contains("toggle box")
                || d.GetAttributeValue("class", "novalue").Contains("toggable box")).ToList();

            foreach (var table in tables)
            {
                if (table.GetAttributeValue("class", "novalue") == "toggle box")
                {
                    product = new Produto
                    {
                        Num = table.Descendants().FirstOrDefault(d => d.Name.ToLower() == "td" && d.GetAttributeValue("class", "novalue") == "fixo-prod-serv-numero").InnerText.Trim(),
                        Descricao = table.Descendants().FirstOrDefault(d => d.Name.ToLower() == "td" && d.GetAttributeValue("class", "novalue") == "fixo-prod-serv-descricao").InnerText.Trim(),
                        Qtd = table.Descendants().FirstOrDefault(d => d.Name.ToLower() == "td" && d.GetAttributeValue("class", "novalue") == "fixo-prod-serv-qtd").InnerText.Trim(),
                        UnidadeComercial = table.Descendants().FirstOrDefault(d => d.Name.ToLower() == "td" && d.GetAttributeValue("class", "novalue") == "fixo-prod-serv-uc").InnerText.Trim(),
                        Valor = table.Descendants().FirstOrDefault(d => d.Name.ToLower() == "td" && d.GetAttributeValue("class", "novalue") == "fixo-prod-serv-vb").InnerText.Trim()
                    };
                }

                if (table.GetAttributeValue("class", "novalue") == "toggable box" && product != null)
                {
                    var detailTables = table.Descendants().Where(e => e.Name.ToLower() == "table"
                        && e.GetAttributeValue("class", "novalue") == "box").ToList();

                    var tablesToRemove = new List<HtmlNode>();
                    foreach (var detailTable in detailTables)
                    {
                        var trs = detailTable.Descendants().Where(e => e.Name.ToLower() == "tr").ToList();
                        foreach (var tr in trs)
                        {
                            var tds = tr.Descendants().Where(e => e.Name.ToLower() == "td").ToList();
                            foreach (var td in tds)
                            {
                                var subDetailTables = td.Descendants().Where(e => e.Name.ToLower() == "table"
                                && e.GetAttributeValue("class", "novalue") == "box").ToList();
                                tablesToRemove.AddRange(subDetailTables);
                            }
                        }
                    }

                    foreach (var removeTable in tablesToRemove)
                    {
                        detailTables.Remove(removeTable);
                    }

                    foreach (var detailTable in detailTables)
                    {
                        var trs = detailTable.Descendants().Where(e => e.Name.ToLower() == "tr").ToList();
                        foreach (var tr in trs)
                        {
                            var tds = tr.Descendants().Where(e => e.Name.ToLower() == "td").ToList();
                            foreach (var td in tds)
                            {
                                var label = td.Descendants().FirstOrDefault(d => d.Name.ToLower() == "label");
                                var span = td.Descendants().FirstOrDefault(d => d.Name.ToLower() == "span");

                                if (label != null && span != null && !string.IsNullOrWhiteSpace(label.InnerText))
                                {
                                    switch (label.InnerText.Trim())
                                    {
                                        case "Código do Produto":
                                            product.CodigoProduto = span.InnerText.Trim();
                                            break;
                                        case "Código NCM":
                                            product.CodigoNCM = span.InnerText.Trim();
                                            break;
                                        case "Código EX da TIPI":
                                            product.CodigoExDaTipi = span.InnerText.Trim();
                                            break;
                                        case "CFOP":
                                            product.CFOP = span.InnerText.Trim();
                                            break;
                                        case "Outras Despesas Acessórias":
                                            product.OutrasDespesasAcessorias = span.InnerText.Trim();
                                            break;
                                        case "Valor do Desconto":
                                            product.ValorDesconto = span.InnerText.Trim();
                                            break;
                                        case "Valor Total do Frete":
                                            product.ValorTotalFrete = span.InnerText.Trim();
                                            break;
                                        case "Valor do Seguro":
                                            product.ValorSeguro = span.InnerText.Trim();
                                            break;
                                        case "Indicador de Composição do Valor Total da NF-e":
                                            product.IndicadorComposicaoValorTotalNFe = span.InnerText.Trim();
                                            break;
                                        case "Código EAN Comercial":
                                            product.CodigoEANComercial = span.InnerText.Trim();
                                            break;
                                        case "Quantidade Comercial":
                                            product.QuantidadeComercial = span.InnerText.Trim();
                                            break;
                                        case "Código EAN Tributável":
                                            product.CodigoEANTributavel = span.InnerText.Trim();
                                            break;
                                        case "Unidade Tributável":
                                            product.UnidadeTributavel = span.InnerText.Trim();
                                            break;
                                        case "Quantidade Tributável":
                                            product.QuantidadeTributavel = span.InnerText.Trim();
                                            break;
                                        case "Valor unitário de comercialização":
                                            product.ValorUnitarioComercializacao = span.InnerText.Trim();
                                            break;
                                        case "Valor unitário de tributação":
                                            product.ValorUnitarioTributacao = span.InnerText.Trim();
                                            break;
                                        case "Número do pedido de compra":
                                            product.NumeroPedidoDeCompra = span.InnerText.Trim();
                                            break;
                                        case "Item do pedido de compra":
                                            product.ItemPedidoCompra = span.InnerText.Trim();
                                            break;
                                        case "Valor Aproximado dos Tributos":
                                            product.ValorAproximadoTributos = span.InnerText.Trim();
                                            break;
                                        case "Número da FCI":
                                            product.NumeroFCI = span.InnerText.Trim();
                                            break;
                                    }
                                }

                                var subDetailTables = td.Descendants().Where(e => e.Name.ToLower() == "table"
                                        && (e.GetAttributeValue("class", "novalue") == "box" || 
                                            e.GetAttributeValue("class", "novalue") ==  "toggable box")).ToList();

                                foreach (var subTable in subDetailTables)
                                {
                                    var tableRows = subTable.Descendants().Where(e => e.Name.ToLower() == "tr").ToList();
                                    foreach (var tableRow in tableRows)
                                    {
                                        var tableColumns = tableRow.Descendants().Where(e => e.Name.ToLower() == "td").ToList();
                                        foreach (var tableColumn in tableColumns)
                                        {
                                            var columnLabel = tableColumn.Descendants().FirstOrDefault(d => d.Name.ToLower() == "label");
                                            var columnSpan = tableColumn.Descendants().FirstOrDefault(d => d.Name.ToLower() == "span");

                                            if (columnLabel != null && columnSpan != null && !string.IsNullOrWhiteSpace(columnLabel.InnerText))
                                            {
                                                var legend = td.Descendants().FirstOrDefault(e => e.Name.ToLower() == "legend");
                                                switch (columnLabel.InnerText.Trim())
                                                {
                                                    case "Origem da Mercadoria":
                                                        product.OrigemMercadoria = columnSpan.InnerText.Trim();
                                                        break;
                                                    case "Tributação do ICMS":
                                                        product.TributacaoICMS = columnSpan.InnerText.Trim();
                                                        break;
                                                    case "Modalidade Definição da BC ICMS NORMAL":
                                                        product.modBC = columnSpan.InnerText.Trim();
                                                        break;
                                                    case "Base de Cálculo do ICMS Normal":
                                                        product.vBC = columnSpan.InnerText.Trim();
                                                        break;
                                                    case "Alíquota do ICMS Normal":
                                                        product.pICMS = string.IsNullOrWhiteSpace(columnSpan.InnerText.Trim()) ? "0" : columnSpan.InnerText.Trim();
                                                        break;
                                                    case "Valor do ICMS Normal":
                                                        product.vICMS = string.IsNullOrWhiteSpace(columnSpan.InnerText.Trim()) ? "0" : columnSpan.InnerText.Trim();
                                                        break;
                                                    case "Valor ICMS desoneração":
                                                        product.ValorICMSDesoneracao = columnSpan.InnerText.Trim();
                                                        break;
                                                    case "Código de Enquadramento":
                                                        product.cEnq = columnSpan.InnerText.Trim();
                                                        break;
                                                    case "Base de Cálculo":
                                                        if (legend.InnerText.Trim() == "ICMS Normal e ST")
                                                            product.IPI_vBC = string.IsNullOrWhiteSpace(columnSpan.InnerText.Trim()) ? "0" : columnSpan.InnerText.Trim();
                                                        break;
                                                    case "Alíquota":
                                                        if (legend.InnerText.Trim() == "ICMS Normal e ST")
                                                            product.IPI_pIpi = string.IsNullOrWhiteSpace(columnSpan.InnerText.Trim()) ? "0" : columnSpan.InnerText.Trim();
                                                        break;
                                                    case "Valor IPI":
                                                        product.IPI_vIpi = string.IsNullOrWhiteSpace(columnSpan.InnerText.Trim()) ? "0" : columnSpan.InnerText.Trim();
                                                        break;
                                                    case "CST":
                                                        if (legend != null)
                                                        {
                                                            switch (legend.InnerText.Trim())
                                                            {
                                                                case "PIS":
                                                                    product.PIS_CST = columnSpan.InnerText.Trim();
                                                                    break;
                                                                case "COFINS":
                                                                    product.COFINS_CST = columnSpan.InnerText.Trim();
                                                                    break;
                                                                case "ICMS Normal e ST":
                                                                    product.IPI_CST = columnSpan.InnerText.Trim();
                                                                    break;
                                                            }
                                                        }
                                                        break;
                                                    case "Número":                                                        
                                                        if (legend.InnerText.Trim() == "ICMS Normal e ST")
                                                            product.nDI = columnSpan.InnerText.Trim();
                                                        break;
                                                    case "Data de registro":
                                                        if (legend.InnerText.Trim() == "ICMS Normal e ST")
                                                            product.dDI = columnSpan.InnerText.Trim();
                                                        break;
                                                    case "Local do desembaraço aduaneiro":
                                                        if (legend.InnerText.Trim() == "ICMS Normal e ST")
                                                            product.xLocDesemb = columnSpan.InnerText.Trim();
                                                        break;
                                                    case "UF do desembaraço":
                                                        if (legend.InnerText.Trim() == "ICMS Normal e ST")
                                                            product.UFDesemb = columnSpan.InnerText.Trim();
                                                        break;
                                                    case "Data do desembaraço":
                                                        if (legend.InnerText.Trim() == "ICMS Normal e ST")
                                                            product.dDesemb = columnSpan.InnerText.Trim();
                                                        break;
                                                    case "Código do Exportador":
                                                        if (legend.InnerText.Trim() == "ICMS Normal e ST")
                                                            product.cExportador = columnSpan.InnerText.Trim();
                                                        break;                                                
                                                    case "Adição":
                                                         if (legend.InnerText.Trim() == "ICMS Normal e ST")
                                                             product.nAdicao = columnSpan.InnerText.Trim();
                                                        break;
                                                    case "Item":
                                                        if (legend.InnerText.Trim() == "ICMS Normal e ST")
                                                            product.nSeqAdic = columnSpan.InnerText.Trim();
                                                        break;
                                                    case "Código Fabricante Estrangeiro":
                                                        if (legend.InnerText.Trim() == "ICMS Normal e ST")
                                                            product.cFabricante = columnSpan.InnerText.Trim();
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                    subTable.Remove();
                                }
                            }
                        }
                    }
                    products.Add(product);
                    product = null;
                }
            }

            return products;
        }
    }
}
