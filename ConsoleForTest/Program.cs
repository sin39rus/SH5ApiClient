using SH5ApiClient;
using SH5ApiClient.Models;
using System;

namespace ConsoleForTest
{
    internal class Program
    {
        static void Main()
        {
            ConnectionParamSH5 param = new("Admin", "", "127.0.0.1", 9797);
            IApiClient client = new ApiClient(param);
            var coors = client.RequestPermissionExecuteProcedure(new string[] { "InsPDoc0" }).Result;
        }
    }
}