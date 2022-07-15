using SH5ApiClient;
using SH5ApiClient.Models;
using SH5ApiClient.Models.DTO;
using System;

namespace ConsoleForTest
{
    internal class Program
    {
        static void Main()
        {
            //var dd = ModelSHBase.Parse<InternalСorrespondent>(null);
            ConnectionParamSH5 param = new("Admin", "", "192.168.200.41", 9797);
            //ConnectionParamSH5 param = new("Admin", "776417", "127.0.0.1", 9797);
            IApiClient client = new ApiClient(param);
            var correspondents = client.LoadGoodsFromGGroup(2).Result;
            //var gdoc11 = client.GetGDoc11Async(10, "B32CCD68-8979-0177-BAB3-68ED6C31E5AA").Result;


            //var gg2 = client.LoadCorrespondentsAsync().Result.ToList();
            //var coors = client.LoadEnumeratedAttributeValuesAsync("119", "6\\Payment_Place").Result;
        }
    }
}