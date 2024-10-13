

using SH5ApiClient.Models;

namespace ConsoleForTestFrameWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {

                ConnectionParamSH5 connectionParam = new ConnectionParamSH5("Admin", "776417", "office.ctsassar.ru", 17772);
                SH5ApiClient.ApiClient client = new SH5ApiClient.ApiClient(connectionParam);
                var groups = client.LoadGGroupsAsync().Result;
                foreach (var group in groups)
                {
                    var ggg = client.LoadGoodsFromGGroupAsync((uint)group.Rid).Result;
                }
            }
            catch (System.Exception ex)
            {

            }
        }
    }

    internal enum TTNType
    {
        None = 0,
        First = 1,
    }
}
