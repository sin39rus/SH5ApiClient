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
            //ConnectionParamSH5 param = new("Admin", "776417", "127.0.0.1", 9797);
            ApiClient client = new ApiClient(param);

            //client.LoadGDocsAsync(from, to, SH5ApiClient.Models.Enums.TTNTypeForRequest.SalesInvoice, SH5ApiClient.Models.Enums.GDocsRequestFilter.ShowActiveInvoices | SH5ApiClient.Models.Enums.GDocsRequestFilter.CalculateSums)
            var ff = client.LoadGDocsAsync(new DateTime(2023, 06, 01), DateTime.Now, SH5ApiClient.Models.Enums.TTNTypeForRequest.SalesInvoice).Result.ToArray().Single(t=>t.Name == "4332");

            var doc = client.GetGDoc4Async(ff.Rid.GetValueOrDefault(), ff.GUID).Result;
            doc.ChangeValue("111", "Rid", 71018, "6\\DocumentReceived", 1);
            var fff = client.UpdateGDoc4(doc).Result;

        }
    }
}