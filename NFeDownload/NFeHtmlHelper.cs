using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;

namespace NFeDownload
{
    public class NFeHtmlHelper
    {
        private string userAgent = "NFe XML Generator";
        private CookieContainer cookies;

        public ItemsForPost GetItemsForPost()
        {
            ////Get items for post
            var request = (HttpWebRequest)WebRequest.Create("http://www.nfe.fazenda.gov.br/portal/consulta.aspx?tipoConsulta=completa&tipoConteudo=XbSeqxE8pl8=");
            cookies = new CookieContainer();
            request.CookieContainer = cookies;
            request.UserAgent = userAgent;
            //request.Method = "POST";
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load(responseStream);
            var documentText = doc.DocumentNode.InnerText;

            var itemsForPost = new ItemsForPost();
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

        public bool ValidatePost(ItemsForPost itemsForPost)
        {
            if (!string.IsNullOrWhiteSpace(itemsForPost.EventTarget))
            {
                MessageBox.Show("__EVENTTARGET deve ser vazio!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(itemsForPost.EventArgument))
            {
                MessageBox.Show("__EVENTARGUMENT deve ser vazio!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(itemsForPost.ViewState))
            {
                MessageBox.Show("__VIEWSTATE deve possuir um valor!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            //if (string.IsNullOrWhiteSpace(itemsForPost.ViewStateGenerator))
            //{
            //    MessageBox.Show("__VIEWSTATEGENERATOR deve possuir um valor!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}

            if (string.IsNullOrWhiteSpace(itemsForPost.EventValidation))
            {
                MessageBox.Show("__EVENTVALIDATION deve possuir um valor!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(itemsForPost.PalavraChave))
            {
                MessageBox.Show("ctl00$txtPalavraChave deve ser vazia!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(itemsForPost.ChaveAcessoCompleta))
            {
                MessageBox.Show("A chave de acesso não foi informada!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(itemsForPost.Captcha))
            {
                MessageBox.Show("O captcha não foi informado!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (itemsForPost.CommomToolkitScripts != "1")
            {
                MessageBox.Show("hiddenInputToUpdateATBuffer_CommonToolkitScripts possui um valor inválido.", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        public bool Post(ItemsForPost itemsForPost)
        {
            if (ValidatePost(itemsForPost))
            {
                var postData = ComposePost(itemsForPost);
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);


                var request = (HttpWebRequest)WebRequest.Create("http://www.nfe.fazenda.gov.br/portal/consulta.aspx?tipoConsulta=completa&tipoConteudo=XbSeqxE8pl8=");
                request.CookieContainer = cookies;
                ((HttpWebRequest)request).UserAgent = userAgent;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                var dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                var response = request.GetResponse();

                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.Load(response.GetResponseStream());
                var documentText = doc.DocumentNode.InnerHtml;
                if (documentText.Contains("C&#243;digo da Imagem inv&#225;lido."))
                {
                    MessageBox.Show("Código da Imagem inválido. Tente novamente.", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                var dataItems = GetDataItems(doc, "NFe");

                return true;
            }

            return false;
        }   

        public string ComposePost(ItemsForPost itemsForPost)
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

        public IList<DataItem> GetDataItems(HtmlAgilityPack.HtmlDocument doc, string div)
        {
            var dataItems = new List<DataItem>();

            var nfeDiv = doc.GetElementbyId(div);
            var count = 1;
            foreach (var fieldset in nfeDiv.Descendants().Where(d => d.Name.ToLower() == "fieldset").ToList())
            {
                var legend = fieldset.Descendants().Where(d => d.Name.ToLower() == "legend").FirstOrDefault();
                var tables = fieldset.Descendants().Where(d => d.Name.ToLower() == "table").ToList();
                foreach (var table in tables)
                {
                    if (table != null)
                    {
                        var trs = table.Descendants().Where(e => e.Name.ToLower() == "tr").ToList();
                        foreach (var tr in trs)
                        {                           
                            var tds = tr.Descendants().Where(e => e.Name.ToLower() == "td").ToList();
                            foreach (var td in tds)
                            {
                                var dataItem = new DataItem();
                                dataItem.Fieldset = count;
                                dataItem.Legend = legend.InnerText;
                                dataItem.Div = div;

                                var label = td.Descendants().Where(d => d.Name.ToLower() == "label").FirstOrDefault();
                                if (label != null)
                                    dataItem.AttributeName = label.InnerText.Trim();

                                var span = td.Descendants().Where(d => d.Name.ToLower() == "span").FirstOrDefault();
                                if (span != null)
                                    dataItem.AttributeValue = span.InnerText.Trim();

                                dataItems.Add(dataItem);
                            }                            
                        }

                    }
                }
                count++;
            }

            return dataItems.OrderBy(d => d.Fieldset).ToList();
        }
    }
}
