using SH5ApiClient;
using SH5ApiClient.Data;
using SH5ApiClient.Models;
using SH5ApiClient.Models.DTO;
using System;
using System.Text;

namespace ConsoleForTest
{
    internal class Program
    {
        static void Main()
        {
            ////var dd = ModelSHBase.Parse<InternalСorrespondent>(null);
            ConnectionParamSH5 param = new("Admin", "", "192.168.200.41", 9798);
            //ConnectionParamSH5 param = new("Admin", "776417", "192.168.200.5", 9797);
            ApiClient client = new ApiClient(param);

            // var gg = client.LoadGGroupsAsync().Result;
            var ggg = client.LoadGoodsTreeAsync().Result;
            client.CreateGoodAsync("Тестовый товар", new MeasureUnit[] { new MeasureUnit { Rid = 3 } }).Wait();
        }
    }
}