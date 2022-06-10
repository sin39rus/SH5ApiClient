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
            //ConnectionParamSH5 param = new("Admin", "", "192.168.200.41", 9797);
            ConnectionParamSH5 param = new("Admin", "", "127.0.0.1", 9797);
            IApiClient client = new ApiClient(param);
            var correspondents = client.LoadCorrespondentsAsync().Result;


            var doc = client.RequestGDoc0Async(gdocs0[1].Rid.GetValueOrDefault(), gdocs0[1].GUID).Result;
            //var gg2 = client.LoadCorrespondentsAsync().Result.ToList();
            //var coors = client.LoadEnumeratedAttributeValuesAsync("119", "6\\Payment_Place").Result;
        }
    }
}