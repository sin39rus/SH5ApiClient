using SH5ApiClient;
using SH5ApiClient.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
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
                ConnectionParamSH5 param = new ConnectionParamSH5("Admin", "", "127.0.0.1", 9798);
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
}
