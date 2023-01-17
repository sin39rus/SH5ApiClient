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
            ConnectionParamSH5 param = new("Admin", "", "192.168.200.41", 9797);
            //ConnectionParamSH5 param = new("Admin", "776417", "127.0.0.1", 9797);
            IApiClient client =  new ApiClient(param);
            var ff = client.LoadGDocsAsync(new DateTime(2022, 12, 01), DateTime.Now).Result.ToList();

            string jsonAnswear = File.ReadAllText(@"..\..\..\..\SH5ApiClientTests\Models\DataForTests\Gdoc4.json", Encoding.UTF8);
            var result = DataExecutable.Parse<GDoc4>(jsonAnswear);
        }
    }
}