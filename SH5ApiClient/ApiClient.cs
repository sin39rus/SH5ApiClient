using SH5ApiClient.Core.Requests;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Data;
using SH5ApiClient.Infrastructure.Exceptions;
using SH5ApiClient.Infrastructure.Helpers;
using SH5ApiClient.Models;
using SH5ApiClient.Models.DTO;
using SH5ApiClient.Models.DTO.GDoc;
using SH5ApiClient.Models.DTO.Reports;
using SH5ApiClient.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static SH5ApiClient.Core.Requests.InsGDoc0Request;

namespace SH5ApiClient
{
    public class ApiClient : IApiClient
    {
        private readonly ConnectionParamSH5 _connectionParam;
        private readonly IWebClient _webClient;
        public ApiClient(ConnectionParamSH5 connectionParamSH5, IWebClient webClient = null)
        {
            _connectionParam = connectionParamSH5 ?? throw new ArgumentNullException(nameof(connectionParamSH5));
            _webClient = webClient ?? new WebClient();
        }
        public Task<IEnumerable<GDocHeader>> LoadGDocsAsync(DateTime? dateFrom, DateTime? dateTo, TTNTypeForRequest? ttnTypeForRequest, GDocsRequestFilter? gDocsRequestFilter = GDocsRequestFilter.ShowActiveInvoices) =>
            LoadGDocsAsync(new CancellationToken(), dateFrom, dateTo, ttnTypeForRequest, gDocsRequestFilter);
        public async Task<DataSet> LoadGDocsRawAsync(CancellationToken cancellationToken, DateTime? dateFrom, DateTime? dateTo, TTNTypeForRequest? ttnTypeForRequest, GDocsRequestFilter? gDocsRequestFilter = GDocsRequestFilter.ShowActiveInvoices)
        {
            try
            {
                GDocsRequest request = new GDocsRequest(_connectionParam)
                {
                    DateFrom = dateFrom,
                    DateTo = dateTo,
                    TTNTypeForRequest = ttnTypeForRequest,
                    GDocsRequestFilter = gDocsRequestFilter
                };
                string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
                return DataSet.ParseFromJson(jsonAnswer);
            }
            catch (Exception ex)
            {
                throw new ApiClientException("Ошибка загрузки списка накладных. Подробности во внутреннем исключении.", ex);
            }
        }
        public async Task<IEnumerable<GDocHeader>> LoadGDocsAsync(CancellationToken cancellationToken, DateTime? dateFrom, DateTime? dateTo, TTNTypeForRequest? ttnTypeForRequest, GDocsRequestFilter? gDocsRequestFilter = GDocsRequestFilter.ShowActiveInvoices)
        {
            try
            {
                GDocsRequest request = new GDocsRequest(_connectionParam)
                {
                    DateFrom = dateFrom,
                    DateTo = dateTo,
                    TTNTypeForRequest = ttnTypeForRequest,
                    GDocsRequestFilter = gDocsRequestFilter
                };
                string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
                var data = DataExecutable.Parse<GDocs>(jsonAnswer);
                return data.Where(t => t.TTNOptions != null && t.TTNOptions.Value != TTNOptions.Unknown); //При создании и отправки документа через Честный знак создается документ-дубликат с опцией 32771, пока фильтруем.
            }
            catch (Exception ex)
            {
                throw new ApiClientException("Ошибка загрузки списка накладных. Подробности во внутреннем исключении.", ex);
            }
        }
        public Task<IEnumerable<Depart>> LoadDepartsAsync() =>
            LoadDepartsAsync(new CancellationToken());
        public async Task<IEnumerable<Depart>> LoadDepartsAsync(CancellationToken cancellationToken)
        {
            try
            {
                DepartsRequest departsRequest = new DepartsRequest(_connectionParam);
                string jsonAnswer = await _webClient.WebPostAsync(departsRequest, cancellationToken);
                return DataExecutable.Parse<Departs>(jsonAnswer);
            }
            catch (Exception ex)
            {
                throw new ApiClientException("Ошибка загрузки справочника подразделений. Подробности во внутреннем исключении.", ex);
            }
        }
        public Task<Depart> GetDepartAsync(uint rid, string guid) =>
            GetDepartAsync(rid, guid, new CancellationToken());
        public async Task<Depart> GetDepartAsync(uint rid, string guid, CancellationToken cancellationToken)
        {
            try
            {
                DepartRequest departRequest = new DepartRequest(_connectionParam, rid, guid);
                string jsonAnswer = await _webClient.WebPostAsync(departRequest, cancellationToken);
                var dep = DataExecutable.Parse<Depart>(jsonAnswer);
                return dep;
            }
            catch (Exception ex)
            {
                throw new ApiClientException("Ошибка загрузки информации о подразделении. Подробности во внутреннем исключении.", ex);
            }
        }
        public Task<IEnumerable<Сorrespondent>> LoadCorrespondentsAsync() =>
            LoadCorrespondentsAsync(new CancellationToken());
        public async Task<IEnumerable<Сorrespondent>> LoadCorrespondentsAsync(CancellationToken cancellationToken)
        {
            CorrsRequest corrsRequest = new CorrsRequest(_connectionParam);
            string jsonAnswer = await _webClient.WebPostAsync(corrsRequest, cancellationToken);
            return await DataExecutable.ParseAsync<Сorrespondents>(jsonAnswer, cancellationToken);
        }
        public Task<AbleOperation> GetPermissionExecuteProcedure(IEnumerable<string> procedureNames) =>
            GetPermissionExecuteProcedure(procedureNames, new CancellationToken());
        public async Task<AbleOperation> GetPermissionExecuteProcedure(IEnumerable<string> procedureNames, CancellationToken cancellationToken)
        {
            AbleRequest ableRequest = new AbleRequest(_connectionParam, procedureNames);
            string jsonAnswer = await _webClient.WebPostAsync(ableRequest, cancellationToken);
            return OperationBase.Parse<AbleOperation>(jsonAnswer);
        }
        public Task<IEnumerable<InternalСorrespondent>> LoadInternalCorrespondentsAsync() =>
            LoadInternalCorrespondentsAsync(new CancellationToken());
        public async Task<IEnumerable<InternalСorrespondent>> LoadInternalCorrespondentsAsync(CancellationToken cancellationToken)
        {
            LEntitiesRequest corrsRequest = new LEntitiesRequest(_connectionParam);
            string jsonAnswer = await _webClient.WebPostAsync(corrsRequest, cancellationToken);
            return await DataExecutable.ParseAsync<InternalСorrespondents>(jsonAnswer, cancellationToken);
        }
        public Task<Dictionary<int, string>> LoadEnumeratedAttributeValuesAsync(string head, string path) =>
            LoadEnumeratedAttributeValuesAsync(head, path, new CancellationToken());
        public async Task<Dictionary<int, string>> LoadEnumeratedAttributeValuesAsync(string head, string path, CancellationToken cancellationToken)
        {
            EnumValuesRequest request = new EnumValuesRequest(_connectionParam, head, path);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            return OperationBase.Parse<EnumOperation>(jsonAnswer).GetValues();
        }
        public Task UpdateCorrespondentAsync(string guid, string bankName, string bankAccount, string bik, string corAccount)
            => UpdateCorrespondentAsync(guid, bankName, bankAccount, bik, corAccount, new CancellationToken());
        public Task UpdateCorrespondentAsync(string guid, string bankName, string bankAccount, string bik, string corAccount, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(guid) && !Guid.TryParse(guid, out Guid _))
                throw new ArgumentException($"\"{nameof(guid)}\" не может быть пустым или содержать только пробел.", nameof(guid));
            return UpdateCorrespondentAsyncInternal(guid, bankName, bankAccount, bik, corAccount, cancellationToken);
        }
        private async Task UpdateCorrespondentAsyncInternal(string guid, string bankName, string bankAccount, string bik, string corAccount, CancellationToken cancellationToken)
        {
            CorrRequest request = new CorrRequest(_connectionParam, guid);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            if (bankName != null)
                jsonAnswer = ExecOperation.ChangeValue(jsonAnswer, "107", "34\\Bank_Name", bankName);
            if (bankAccount != null)
                jsonAnswer = ExecOperation.ChangeValue(jsonAnswer, "107", "34\\Bank_PAcc", bankAccount);
            if (bik != null)
                jsonAnswer = ExecOperation.ChangeValue(jsonAnswer, "107", "34\\Bank_BIK", bik);
            if (corAccount != null)
                jsonAnswer = ExecOperation.ChangeValue(jsonAnswer, "107", "34\\Bank_CAcc", corAccount);
            string newRequest = ExecOperation.ConvertToRequest(jsonAnswer, "107", _connectionParam, "UpdCorr");
            string newRequestResult = await _webClient.WebPostAsync(newRequest, _connectionParam, cancellationToken);
            OperationBase.Parse<ExecOperation>(newRequestResult);
        }
        public Task<Сorrespondent> CreateNewCorrespondentAsync(string name, string inn, string bankAccount, string bik, string bankName, string corAccount, CorrType corrType, CorrTypeEx corrTypeEx) =>
            CreateNewCorrespondentAsync(name, inn, bankAccount, bik, bankName, corAccount, corrType, corrTypeEx, new CancellationToken());
        public async Task<Сorrespondent> CreateNewCorrespondentAsync(string name, string inn, string bankAccount, string bik, string bankName, string corAccount, CorrType corrType, CorrTypeEx corrTypeEx, CancellationToken cancellationToken)
        {
            InsCorrRequest corr = new InsCorrRequest(_connectionParam, name, inn)
            {
                CorrType = corrType,
                CorrTypeEx = corrTypeEx,
                BankAccount = bankAccount,
                BIK = bik,
                BankName = bankName,
                CorAccount = corAccount
            };
            string result = await _webClient.WebPostAsync(corr, cancellationToken);
            return DataExecutable.Parse<Сorrespondents>(result).First();

        }
        public Task<InfoOperation> GetSHServerInfoAsync() =>
            GetSHServerInfoAsync(new CancellationToken());
        public async Task<InfoOperation> GetSHServerInfoAsync(CancellationToken cancellationToken)
        {
            string answer = await _webClient.WebPostAsync(new SHInfoRequest(_connectionParam), cancellationToken);
            return OperationBase.Parse<InfoOperation>(answer);
        }
        public Task<IEnumerable<Currency>> LoadCurrenciesAsync() =>
            LoadCurrenciesAsync(new CancellationToken());
        public async Task<IEnumerable<Currency>> LoadCurrenciesAsync(CancellationToken cancellationToken)
        {
            CurrenciesRequest request = new CurrenciesRequest(_connectionParam);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            return DataExecutable.Parse<Currencies>(jsonAnswer);
        }
        public Task<IEnumerable<MeasureGroup>> LoadMeasureGroupsAsync() =>
            LoadMeasureGroupsAsync(new CancellationToken());
        public async Task<IEnumerable<MeasureGroup>> LoadMeasureGroupsAsync(CancellationToken cancellationToken)
        {
            MGroupsRequest request = new MGroupsRequest(_connectionParam);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            return DataExecutable.Parse<MeasureGroups>(jsonAnswer);
        }
        public Task<IEnumerable<MeasureUnit>> LoadMeasureUnitsAsync(uint? groupRid = null) =>
            LoadMeasureUnitsAsync(new CancellationToken(), groupRid);
        public async Task<IEnumerable<MeasureUnit>> LoadMeasureUnitsAsync(CancellationToken cancellationToken, uint? groupRid = null)
        {
            MUnitsRequest request = new MUnitsRequest(_connectionParam, groupRid);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            return DataExecutable.Parse<MeasureUnits>(jsonAnswer);
        }
        public Task<MeasureGroup> GetMeasureGroupAsync(uint rid) =>
            GetMeasureGroupAsync(rid, new CancellationToken());
        public async Task<MeasureGroup> GetMeasureGroupAsync(uint rid, CancellationToken cancellationToken)
        {
            MGroupRequest request = new MGroupRequest(_connectionParam, rid);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            return DataExecutable.Parse<MeasureGroup>(jsonAnswer);
        }
        public Task<GDoc0> GetGDoc0Async(uint rid, string guid) =>
            GetGDoc0Async(rid, guid, new CancellationToken());
        public async Task<DataSet> GetGDoc0RawAsync(uint rid, string guid, CancellationToken cancellationToken)
        {
            GDocRequest request = new GDocRequest(_connectionParam, TTNType.PurchaseInvoice, rid, guid);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            return DataSet.ParseFromJson(jsonAnswer);
        }
        public async Task<GDoc0> GetGDoc0Async(uint rid, string guid, CancellationToken cancellationToken)
        {
            GDocRequest request = new GDocRequest(_connectionParam, TTNType.PurchaseInvoice, rid, guid);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            ExecOperation answer = OperationBase.Parse<ExecOperation>(jsonAnswer);
            return GDoc0.Parse(answer);
        }
        public Task<GDoc4> GetGDoc4Async(uint rid, string guid) =>
            GetGDoc4Async(rid, guid, new CancellationToken());
        public async Task<DataSet> GetGDoc4RawAsync(uint rid, string guid, CancellationToken cancellationToken)
        {
            GDocRequest request = new GDocRequest(_connectionParam, TTNType.SalesInvoice, rid, guid);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            return DataSet.ParseFromJson(jsonAnswer);
        }
        public async Task<GDoc4> GetGDoc4Async(uint rid, string guid, CancellationToken cancellationToken)
        {
            GDocRequest request = new GDocRequest(_connectionParam, TTNType.SalesInvoice, rid, guid);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            return DataExecutable.Parse<GDoc4>(jsonAnswer);
        }
        public Task<GDoc4> UpdateGDoc4(GDoc4 doc) =>
            UpdateGDoc4(doc, new CancellationToken());
        public async Task<GDoc4> UpdateGDoc4(GDoc4 doc, CancellationToken cancellationToken)
        {
            UpdGDoc4Request request = new UpdGDoc4Request(_connectionParam, doc);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            return DataExecutable.Parse<GDoc4>(jsonAnswer);
        }
        public Task<GDoc5> GetGDoc5Async(uint rid, string guid) =>
            GetGDoc5Async(rid, guid, new CancellationToken());
        public async Task<GDoc5> GetGDoc5Async(uint rid, string guid, CancellationToken cancellationToken)
        {
            GDocRequest request = new GDocRequest(_connectionParam, TTNType.ReturnSupplier, rid, guid);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            return DataExecutable.Parse<GDoc5>(jsonAnswer);
        }
        public Task<GDoc8> GetGDoc8Async(uint rid, string guid) =>
            GetGDoc8Async(rid, guid, new CancellationToken());
        public async Task<GDoc8> GetGDoc8Async(uint rid, string guid, CancellationToken cancellationToken)
        {
            GDocRequest request = new GDocRequest(_connectionParam, TTNType.CollationStatement, rid, guid);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            return DataExecutable.Parse<GDoc8>(jsonAnswer);
        }
        public Task<GDoc8Diffs> GetGDoc8DiffsAsync(uint rid, string guid) =>
            GetGDoc8DiffsAsync(rid, guid, new CancellationToken());
        public async Task<GDoc8Diffs> GetGDoc8DiffsAsync(uint rid, string guid, CancellationToken cancellationToken)
        {
            GDocRequest request = new GDocRequest(_connectionParam, TTNType.CollationStatementDiffs, rid, guid);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            return DataExecutable.Parse<GDoc8Diffs>(jsonAnswer);
        }
        public Task<GDoc10> GetGDoc10Async(uint rid, string guid) =>
            GetGDoc10Async(rid, guid, new CancellationToken());
        public async Task<GDoc10> GetGDoc10Async(uint rid, string guid, CancellationToken cancellationToken)
        {
            GDocRequest request = new GDocRequest(_connectionParam, TTNType.ActProcessing, rid, guid);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            return DataExecutable.Parse<GDoc10>(jsonAnswer);
        }
        public Task<GDoc11> GetGDoc11Async(uint rid, string guid)
            => GetGDoc11Async(rid, guid, new CancellationToken());
        public async Task<GDoc11> GetGDoc11Async(uint rid, string guid, CancellationToken cancellationToken)
        {
            GDocRequest request = new GDocRequest(_connectionParam, TTNType.InternalMovement, rid, guid);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            ExecOperation answer = OperationBase.Parse<ExecOperation>(jsonAnswer);
            return GDoc11.Parse(answer);
        }
        public Task<IEnumerable<GGroup>> LoadGGroupsAsync() =>
            LoadGGroupsAsync(new CancellationToken());
        public async Task<IEnumerable<GGroup>> LoadGGroupsAsync(CancellationToken cancellationToken)
        {
            GGroupsRequest request = new GGroupsRequest(_connectionParam);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            var groups = DataExecutable.Parse<GGroups>(jsonAnswer);

            foreach (GGroup group in groups)
            {
                if (group?.Parent?.Rid != null)
                    group.Parent = groups.Single(t => t.Rid == group.Parent.Rid);
                else
                    group.Parent = null;
            }

            return groups;

        }
        public Task<IEnumerable<GoodsItem>> LoadGoodsFromGGroupAsync(uint groupRid) =>
            LoadGoodsFromGGroupAsync(groupRid, new CancellationToken());
        public async Task<IEnumerable<GoodsItem>> LoadGoodsFromGGroupAsync(uint groupRid, CancellationToken cancellationToken)
        {
            GoodsRequest request = new GoodsRequest(_connectionParam, groupRid);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            ExecOperation answer = OperationBase.Parse<ExecOperation>(jsonAnswer);
            return GoodsItem.ParseGoods(answer, cancellationToken);
        }
        public Task<IEnumerable<MeasureUnit>> GetGoodsMUnitsAsync(uint goodRid) =>
            GetGoodsMUnitsAsync(goodRid, new CancellationToken());
        public async Task<IEnumerable<MeasureUnit>> GetGoodsMUnitsAsync(uint goodRid, CancellationToken cancellationToken)
        {
            GoodsMUnitsRequest request = new GoodsMUnitsRequest(_connectionParam, goodRid);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            ExecOperation answer = OperationBase.Parse<ExecOperation>(jsonAnswer);
            return DataExecutable.Parse<MeasureUnits>(jsonAnswer);
        }
        public Task<IEnumerable<GoodsItem>> LoadGoodsTreeAsync() =>
            LoadGoodsTreeAsync(new CancellationToken());
        public async Task<IEnumerable<GoodsItem>> LoadGoodsTreeAsync(CancellationToken cancellationToken)
        {
            GoodsTreeRequest request = new GoodsTreeRequest(_connectionParam);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            ExecOperation answer = await Task.Run(() => { return OperationBase.Parse<ExecOperation>(jsonAnswer); });
            return await Task.Run(() => { return GoodsItem.ParseGoods(answer, cancellationToken); });
        }
        public Task<GoodsItem> CreateGoodAsync(string name, IEnumerable<MeasureUnit> measureUnits) =>
            CreateGoodAsync(name, measureUnits, new CancellationToken());
        public async Task<GoodsItem> CreateGoodAsync(string name, IEnumerable<MeasureUnit> measureUnits, CancellationToken cancellationToken)
        {
            InsGoodRequest request = new InsGoodRequest(_connectionParam, name, measureUnits);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            ExecOperation answer = OperationBase.Parse<ExecOperation>(jsonAnswer);
            return GoodsItem.Parse(answer.GetAnswearContent("210").GetValues()[0]);
        }
        public Task<MeasureUnit> CreateMeasureUnitAsync(string name, decimal ration, uint groupRid) =>
            CreateMeasureUnitAsync(name, ration, groupRid, new CancellationToken());
        public async Task<MeasureUnit> CreateMeasureUnitAsync(string name, decimal ration, uint groupRid, CancellationToken cancellationToken)
        {
            InsMUnitRequest request = new InsMUnitRequest(_connectionParam, name, ration, groupRid);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            ExecOperation answer = OperationBase.Parse<ExecOperation>(jsonAnswer);
            return MeasureUnit.Parse(answer.GetAnswearContent("206").GetValues()[0]);
        }

        public Task<GoodsItem> GetGoodsItemAsync(uint goodsItemRid) =>
            GetGoodsItemAsync(goodsItemRid, new CancellationToken());
        public async Task<GoodsItem> GetGoodsItemAsync(uint goodsItemRid, CancellationToken cancellationToken)
        {
            GoodsItemRequest request = new GoodsItemRequest(_connectionParam, goodsItemRid);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            ExecOperation answer = OperationBase.Parse<ExecOperation>(jsonAnswer);
            var units = MeasureUnit.ParseUnits(answer.GetAnswearContent("211#1").GetValues());
            var item = GoodsItem.Parse(answer.GetAnswearContent("210").GetValues()[0]);
            item.MeasureUnits = units;
            return item;
        }
        private async Task<string> CreateIncomingInvoiceAsync(string rid, DateTime timeStamp, CancellationToken cancellationToken)
        {
            InsIDoc0Request request = new InsIDoc0Request(_connectionParam, rid, timeStamp);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            ExecOperation answer = OperationBase.Parse<ExecOperation>(jsonAnswer);
            return answer.GetAnswearContent("111").GetValues()[0]["3"];
        }
        /// <inheritdoc/>
        public Task<string> CreateIncomingTTNAsync(string name, DateTime timeStamp, string number, uint supplierRid, uint consigneeRid, string comment, bool createInvoice, IEnumerable<GDoc0Item> items) =>
            CreateIncomingTTNAsync(name, timeStamp, number, supplierRid, consigneeRid, comment, createInvoice, items, new CancellationToken());

        /// <inheritdoc/>
        public async Task<string> CreateIncomingTTNAsync(string name, DateTime timeStamp, string number, uint supplierRid, uint consigneeRid, string comment, bool createInvoice, IEnumerable<GDoc0Item> items, CancellationToken cancellationToken)
        {
            InsGDoc0Request request = new InsGDoc0Request(_connectionParam, name, timeStamp, number, supplierRid, consigneeRid, comment, items);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            ExecOperation answer = OperationBase.Parse<ExecOperation>(jsonAnswer);
            var newName = answer.GetAnswearContent("111").GetValues()[0]["3"];
            var newRid = answer.GetAnswearContent("111").GetValues()[0]["1"];
            if (createInvoice)
                await CreateIncomingInvoiceAsync(newRid, timeStamp, cancellationToken);
            return newName;
        }
        public Task<IEnumerable<NDSInfo>> GetNdsListAsync() =>
            GetNdsListAsync(new CancellationToken());
        public async Task<IEnumerable<NDSInfo>> GetNdsListAsync(CancellationToken cancellationToken)
        {
            Taxes1Request request = new Taxes1Request(_connectionParam);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            ExecOperation answer = OperationBase.Parse<ExecOperation>(jsonAnswer);
            return answer.GetAnswearContent("212").Values[0]
                .Select(t => new NDSInfo() { Rate = Convert.ToUInt32(t) });
        }
        public async Task<IEnumerable<GTD>> CreateGtdAsync(params string[] gtdNumbers)
        {
            ModCDeclsRequest request = new ModCDeclsRequest(_connectionParam, gtdNumbers);
            string jsonAnswer = await _webClient.WebPostAsync(request, new CancellationToken());
            ExecOperation answer = OperationBase.Parse<ExecOperation>(jsonAnswer);
            return GTD.ParseRange(answer.GetAnswearContent("116").GetValues());
        }
        ///<inheritdoc />
        public async Task GetDocsByCorrsReportAsync(DateTime from, DateTime to, InternalСorrespondent correspondent, CancellationToken cancellationToken)
        {
            await GetDocsByCorrsReportAsync(from, to, correspondent, cancellationToken);
        }
        ///<inheritdoc />
        public async Task<DocsByCorrsReport> GetDocsByCorrsReportAsync(DateTime from, DateTime to, uint correspondentRid, CancellationToken cancellationToken)
        {
            DocsByCorrsRequest request = new DocsByCorrsRequest(_connectionParam, from, to, correspondentRid);
            string jsonAnswer = await _webClient.WebPostAsync(request, cancellationToken);
            ExecOperation answer = OperationBase.Parse<ExecOperation>(jsonAnswer);
            var report = DocsByCorrsReport.Parse(answer.GetAnswearContent("107").GetValues());
            return report;
        }
    }
}
