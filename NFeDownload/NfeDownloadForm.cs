using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NFeDownload
{
    public partial class NfeDownloadForm : Form
    {
        private readonly NFeHtmlHelper helper = new NFeHtmlHelper();
        private ItemsForPost itemsForPost;

        public NfeDownloadForm()
        {
            InitializeComponent();
        }

        private void NfeDownloadForm_Load(object sender, EventArgs e)
        {
            InitializeForPost();
        }

        private void SendButtonOnClick(object sender, EventArgs e)
        {           
            itemsForPost.ChaveAcessoCompleta = nfeTextBox.Text;
            itemsForPost.Captcha = captchaTextBox.Text;            
            if (helper.ValidatePost(itemsForPost))
            {
                helper.Post(itemsForPost);
                InitializeForPost();
            }
            else
            {
                nfeTextBox.Focus();
                InitializeForPost();
            }
        }

        private void UpdateForm(ItemsForPost itemsForPost)
        {
            var normalizedBase64Img = itemsForPost.Base64Image.Replace("data:image/png;base64,", string.Empty);
            captchaPictureBox.Image = Helpers.Base64StringToBitmap(normalizedBase64Img);
        }

        private void InitializeForPost()
        {
            itemsForPost = helper.GetItemsForPost();
            UpdateForm(itemsForPost);
        }
    }
}
