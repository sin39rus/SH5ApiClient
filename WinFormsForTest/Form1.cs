using SH5ApiClient;
using SH5ApiClient.Models;
using SH5ApiClient.Models.DTO;
using System.Dynamic;

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
            try
            {
                ConnectionParamSH5 param = new("Admin", "", "127.0.0.1", 9798);
                IApiClient client = new ApiClient(param);
                var rr = await client.LoadCorrespondentsAsync();
                MessageBox.Show(rr.Count().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    public class Test
    {
        public async Task<IEnumerable<—orrespondent>> Get—orrespondents()
        {
            ConnectionParamSH5 param = new("Admin", "", "127.0.0.1", 9797);
            IApiClient client = new ApiClient(param);
            var rr = await client.LoadCorrespondentsAsync();
            return rr;
        }

    }
}