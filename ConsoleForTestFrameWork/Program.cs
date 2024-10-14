

using SH5ApiClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleForTestFrameWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {

                ConnectionParamSH5 connectionParam = new ConnectionParamSH5("Admin", "776417", "office.ctsassar.ru", 17772);
                SH5ApiClient.ApiClient _client = new SH5ApiClient.ApiClient(connectionParam);

                var groups2 = _client.InsGoodRequest().Result;



                //var groups = _client.LoadGGroupsAsync().Result;
                //var measureUnits = _client.LoadMeasureUnitsAsync().Result;
                //Console.WriteLine($"Groups count: {groups.Count()}");
                //foreach (var group in groups)
                //{
                //    var goods = _client.LoadGoodsFromGGroupAsync(group.Rid.GetValueOrDefault()).Result;
                //    Console.WriteLine($"Goods count: {goods.Count()}");
                //    foreach (var good in goods)
                //    {
                //        var ggg = _client.GetGoodsMUnitsAsync(good.Rid.GetValueOrDefault()).Result;
                //        Console.WriteLine(good.Name);
                //    }
                //}
            }
            catch (System.Exception ex)
            {

            }
        }
    }
}
