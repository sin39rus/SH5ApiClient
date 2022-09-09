using SH5ApiClient;
using SH5ApiClient.Models;

namespace WinFormsForTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            ConnectionParamSH5 param = new("Admin", "", "192.168.200.41", 9797);
            IApiClient client = new ApiClient(param);
            var doc = await client.GetGDoc4Async(56295, "AA48F554-5502-93CA-7D07-6F15E98439AD");
            var doc1 = await client.GetGDoc4Async(56295, "AA48F554-5502-93CA-7D07-6F15E98439AD");
            var doc4 = await client.GetGDoc4Async(56295, "AA48F554-5502-93CA-7D07-6F15E98439AD");
            var doc5 = await client.GetGDoc4Async(56295, "AA48F554-5502-93CA-7D07-6F15E98439AD");
            var doc2 = await client.GetGDoc4Async(56295, "AA48F554-5502-93CA-7D07-6F15E98439AD");
            var doc3 = await client.GetGDoc4Async(56295, "AA48F554-5502-93CA-7D07-6F15E98439AD");
            progressBar1.Visible = false;
        }
    }
}