using NFeDownload.Download;
using System;
using System.Windows.Forms;

namespace NFeDownloadForms
{
    public partial class NfeDownloadForm : Form
    {
        private readonly HtmlHelper helper = new HtmlHelper();
        private PostItems postItems;

        public NfeDownloadForm()
        {
            InitializeComponent();
        }

        private void UpdateForm(PostItems itemsForPost)
        {
            var normalizedBase64Img = itemsForPost.Base64Image.Replace("data:image/png;base64,", string.Empty);
            captchaPictureBox.Image = Helpers.Base64StringToBitmap(normalizedBase64Img);
        }

        private void InitializeForPost()
        {
            postItems = helper.GetItemsForPost();
            UpdateForm(postItems);
        }

        private void NfeDownloadForm_Load(object sender, EventArgs e)
        {
            InitializeForPost();
        }

        private void SendButtonOnClick(object sender, EventArgs e)
        {                       
            try
            {
                postItems.ChaveAcessoCompleta = nfeTextBox.Text;
                postItems.Captcha = captchaTextBox.Text;

                helper.ValidatePost(postItems);
                var postResult = helper.Post(postItems);

                InitializeForPost();
                captchaTextBox.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                nfeTextBox.Focus();
                InitializeForPost();
            }                                                             
        }        
    }
}
