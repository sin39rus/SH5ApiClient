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
            //ConnectionParamSH5 param = new("Admin", "", "127.0.0.1", 9797);
            IApiClient client = new ApiClient(param);
            var correspondents = client.LoadGDocsAsync(new DateTime(2020, 1, 1), new DateTime(2023, 1, 1), SH5ApiClient.Models.Enums.TTNTypeForRequest.SalesInvoice).Result;
            var gdoc0 = client.GetGDoc4Async(4, "5488EA00-2DED-F0B5-A8AD-5D5047DF5F82").Result;


            //var gg2 = client.LoadCorrespondentsAsync().Result.ToList();
            //var coors = client.LoadEnumeratedAttributeValuesAsync("119", "6\\Payment_Place").Result;
        }
    }
}