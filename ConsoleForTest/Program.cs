﻿using SH5ApiClient;
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
            var ff = client.LoadGDocsAsync(DateTime.Now, DateTime.Now, SH5ApiClient.Models.Enums.TTNTypeForRequest.CollationStatement).Result;
            var doc = client.GetGDoc8DiffsAsync(58884, "24081730-6AA9-E77C-4C76-AD930C119F3F").Result;
        }
    }
}