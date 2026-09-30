using Microsoft.VisualStudio.TestTools.UnitTesting;
using SH5ApiClient.Core.Requests;
using SH5ApiClient.Infrastructure.Exceptions;
using SH5ApiClient.Infrastructure.Helpers;
using SH5ApiClient.Models;
using SH5ApiClient.Models.DTO;
using SH5ApiClient.Models.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SH5ApiClientTests
{
    [TestClass]
    public class ApiClientTests
    {
        private static readonly ConnectionParamSH5 Connection = new ConnectionParamSH5("Admin", "123456", "127.0.0.1", 9191);

        private static SH5ApiClient.ApiClient CreateClient(IWebClient webClient) =>
            new SH5ApiClient.ApiClient(Connection, webClient);

        private static string ReadTestData(string fileName) =>
            File.ReadAllText(Path.Combine("Models", "DataForTests", fileName), Encoding.UTF8);

        [TestMethod]
        public void UpdateCorrespondentAsync_InvalidGuid_ThrowsArgumentException()
        {
            var client = CreateClient(new FakeWebClient());
            Assert.ThrowsExceptionAsync<ArgumentException>(() => client.UpdateCorrespondentAsync("not-a-guid", null, null, null, null)).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void GetDocsByCorrsReportAsync_NullCorrespondent_ThrowsArgumentNullException()
        {
            var client = CreateClient(new FakeWebClient());
            InternalСorrespondent correspondent = null!;
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => client.GetDocsByCorrsReportAsync(DateTime.Today, DateTime.Today, correspondent, CancellationToken.None)).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void GetDocsByCorrsReportAsync_CorrespondentWithoutRid_ThrowsArgumentException()
        {
            var client = CreateClient(new FakeWebClient());
            var correspondent = new InternalСorrespondent { Rid = null };
            Assert.ThrowsExceptionAsync<ArgumentException>(() => client.GetDocsByCorrsReportAsync(DateTime.Today, DateTime.Today, correspondent, CancellationToken.None)).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void GetGDocsExReportAsync_NullDeparts_ThrowsArgumentNullException()
        {
            var client = CreateClient(new FakeWebClient());
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => client.GetGDocsExReportAsync(null, null, TTNTypeForRequest.PurchaseInvoice, GDocsRequestFilter.ShowActiveInvoices, null, CancellationToken.None)).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void CreateNewCorrespondentAsync_EmptyName_ThrowsArgumentException()
        {
            var client = CreateClient(new FakeWebClient());
            Assert.ThrowsExceptionAsync<ArgumentException>(() => client.CreateNewCorrespondentAsync(" ", "inn", null, null, null, null, CorrType.OutsideCorrespondent, CorrTypeEx.Organization)).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void CreateIncomingTTNAsync_NullItems_ThrowsArgumentNullException()
        {
            var client = CreateClient(new FakeWebClient());
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => client.CreateIncomingTTNAsync("name", DateTime.Today, "1", 1, 2, null, false, null)).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void GetGDoc0Async_InvalidGuid_ThrowsArgumentException()
        {
            var client = CreateClient(new FakeWebClient());
            Assert.ThrowsExceptionAsync<ArgumentException>(() => client.GetGDoc0Async(1, "bad")).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void CreateGoodAsync_EmptyResponse_ThrowsApiClientException()
        {
            var webClient = new FakeWebClient
            {
                PostRequestHandler = (_, __) => Task.FromResult(@"{""Version"":""1"",""UserName"":""Admin"",""actionName"":""InsGood"",""actionType"":""Execute"",""shTable"":[{""head"":""210"",""recCount"":0,""original"":[],""fields"":[],""values"":[]}]}")
            };
            var client = CreateClient(webClient);
            var ex = Assert.ThrowsExceptionAsync<ApiClientException>(() => client.CreateGoodAsync("test", Array.Empty<MeasureUnit>())).GetAwaiter().GetResult();
            Assert.IsNotNull(ex);
            StringAssert.Contains(ex.Message, "210");
        }

        [TestMethod]
        public void CreateGoodAsync_BrokenJson_ThrowsApiClientException()
        {
            var webClient = new FakeWebClient
            {
                PostRequestHandler = (_, __) => Task.FromResult("{ not-json")
            };
            var client = CreateClient(webClient);
            Assert.ThrowsExceptionAsync<ApiClientException>(() => client.CreateGoodAsync("test", Array.Empty<MeasureUnit>())).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void LoadDepartsAsync_HappyPath_ReturnsDepartments()
        {
            var webClient = new FakeWebClient
            {
                PostRequestHandler = (request, _) =>
                {
                    Assert.IsInstanceOfType(request, typeof(DepartsRequest));
                    return Task.FromResult(ReadTestData("Departs.json"));
                }
            };
            var client = CreateClient(webClient);
            IEnumerable<Depart> departs = client.LoadDepartsAsync().GetAwaiter().GetResult();
            Assert.AreEqual(2, departs.Count());
            Assert.AreEqual("Склад 2", departs.First().Name);
        }

        [TestMethod]
        public void GetGoodsMUnitsAsync_HappyPath_ReturnsUnits()
        {
            var webClient = new FakeWebClient
            {
                PostRequestHandler = (request, _) =>
                {
                    Assert.AreEqual("GoodsMUnits", request.ProcName);
                    return Task.FromResult(ReadTestData("MUnits.json"));
                }
            };
            var client = CreateClient(webClient);
            IEnumerable<MeasureUnit> units = client.GetGoodsMUnitsAsync(1).GetAwaiter().GetResult();
            Assert.IsTrue(units.Any());
        }

        private sealed class FakeWebClient : IWebClient
        {
            public Func<RequestBase, CancellationToken, Task<string>>? PostRequestHandler { get; set; }

            public Task<string> WebGetAsync(string url, CancellationToken cancellationToken) =>
                throw new NotImplementedException();

            public Task<string> WebPostAsync(RequestBase request, CancellationToken cancellationToken) =>
                PostRequestHandler != null ? PostRequestHandler(request, cancellationToken) : throw new NotImplementedException();

            public Task<string> WebPostAsync(string request, CancellationToken cancellationToken) =>
                throw new NotImplementedException();
        }
    }
}
