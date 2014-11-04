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

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load(responseStream);
            var documentText = doc.DocumentNode.InnerText;

            var itemsForPost = new PostItems();
            itemsForPost.EventTarget = string.Empty;
            itemsForPost.EventArgument = string.Empty;
            itemsForPost.PalavraChave = string.Empty;

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

        private void Check(HtmlNode htmlNode)
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
            ((HttpWebRequest)request).UserAgent = UserAgent;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            var dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            var pageResponse = request.GetResponse();

            var docUserWebPage = new HtmlAgilityPack.HtmlDocument();
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
            ((HttpWebRequest)printRequest).UserAgent = UserAgent;
            printRequest.ContentType = "application/x-www-form-urlencoded";
            var printResponse = printRequest.GetResponse();
            var printResponseStream = printResponse.GetResponseStream();

            var printUserPage = new HtmlAgilityPack.HtmlDocument();
            printUserPage.Load(printResponseStream);

            var result = new DownloadedHtmlData();
            var spanChaveAcesso = printUserPage.GetElementbyId("lblChaveAcesso");
            Check(spanChaveAcesso);            
            result.ChaveAcessso = spanChaveAcesso.InnerText.Trim();
            result.DadosNfe = GetDataItems(printUserPage, "NFe");
            var operationScience = printUserPage.DocumentNode.Descendants().Where(e => e.Id.Contains("CienciaOperacao")).ToList();
            var scienceOperationsList = new List<IList<PostResultItem>>();
            foreach (var science in operationScience)
            {
                var dadosCiencia = GetDataItems(printUserPage, science.Id);
                scienceOperationsList.Add(dadosCiencia);
            }
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
            var postData = string.Empty;

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
            postData = outgoingQueryString.ToString();

            return postData;
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
                var legend = fieldset.Descendants().Where(d => d.Name.ToLower() == "legend").FirstOrDefault();
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
                                        var dataItem = new PostResultItem();
                                        dataItem.Fieldset = count;
                                        dataItem.Legend = legend.InnerText.Trim();
                                        dataItem.Div = div;

                                        var label = td.Descendants().Where(d => d.Name.ToLower() == "label").FirstOrDefault();
                                        var span = td.Descendants().Where(d => d.Name.ToLower() == "span").FirstOrDefault();

                                        if (label != null && span != null && !string.IsNullOrWhiteSpace(label.InnerText))
                                        {
                                            dataItem.AttributeName = label.InnerText.Trim();
                                            dataItem.AttributeValue = span.InnerText.Trim();
                                            dataItems.Add(dataItem);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 1; i < trs.Count; i++)
                                {
                                    var subNodeScience = trs[i].Descendants().Where(e => e.Id.Contains("CienciaOperacao")).FirstOrDefault();

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

                                        var span = td.Descendants().Where(d => d.Name.ToLower() == "span").FirstOrDefault();
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

            var count = 1;

            foreach (var table in tables)
            {
                if (table.GetAttributeValue("class", "novalue") == "toggle box")
                {
                    product = new Produto();
                    product.Num = table.Descendants().Where(d => d.Name.ToLower() == "td"
                        && d.GetAttributeValue("class", "novalue") == "fixo-prod-serv-numero").FirstOrDefault().InnerText.Trim();
                    product.Descricao = table.Descendants().Where(d => d.Name.ToLower() == "td"
                        && d.GetAttributeValue("class", "novalue") == "fixo-prod-serv-descricao").FirstOrDefault().InnerText.Trim();
                    product.Qtd = table.Descendants().Where(d => d.Name.ToLower() == "td"
                        && d.GetAttributeValue("class", "novalue") == "fixo-prod-serv-qtd").FirstOrDefault().InnerText.Trim();
                    product.UnidadeComercial = table.Descendants().Where(d => d.Name.ToLower() == "td"
                       && d.GetAttributeValue("class", "novalue") == "fixo-prod-serv-uc").FirstOrDefault().InnerText.Trim();
                    product.Valor = table.Descendants().Where(d => d.Name.ToLower() == "td"
                     && d.GetAttributeValue("class", "novalue") == "fixo-prod-serv-vb").FirstOrDefault().InnerText.Trim();
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
                                  foreach (var tableDetail in subDetailTables)
                                      tablesToRemove.Add(tableDetail);
                              }
                         }
                    }

                    foreach(var removeTable in tablesToRemove)
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
                                var label = td.Descendants().Where(d => d.Name.ToLower() == "label").FirstOrDefault();
                                var span = td.Descendants().Where(d => d.Name.ToLower() == "span").FirstOrDefault();

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
                                        && e.GetAttributeValue("class", "novalue") == "box").ToList();

                                foreach (var subTable in subDetailTables)
                                {
                                    var tableRows = subTable.Descendants().Where(e => e.Name.ToLower() == "tr").ToList();
                                    foreach (var tableRow in tableRows)
                                    {
                                        var tableColumns = tableRow.Descendants().Where(e => e.Name.ToLower() == "td").ToList();
                                        foreach (var tableColumn in tableColumns)
                                        {
                                            var columnLabel = tableColumn.Descendants().Where(d => d.Name.ToLower() == "label").FirstOrDefault();
                                            var columnSpan = tableColumn.Descendants().Where(d => d.Name.ToLower() == "span").FirstOrDefault();

                                            if (columnLabel != null && columnSpan != null && !string.IsNullOrWhiteSpace(columnLabel.InnerText))
                                            {
                                                switch (columnLabel.InnerText.Trim())
                                                {
                                                    case "Origem da Mercadoria":
                                                        product.OrigemMercadoria = columnSpan.InnerText.Trim();
                                                        break;
                                                    case "Tributação do ICMS":
                                                        product.TributacaoICMS = columnSpan.InnerText.Trim();
                                                        break;
                                                    case "Valor ICMS desoneração":
                                                        product.ValorICMSDesoneracao = columnSpan.InnerText.Trim();
                                                        break;
                                                    case "CST":
                                                        var legend = td.Descendants().Where(e => e.Name.ToLower() == "legend").FirstOrDefault();
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
                                                            }                                                            
                                                        }                                                        
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
                }

                count++;
            }

            return products;
        }
    }
}
