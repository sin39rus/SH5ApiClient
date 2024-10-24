﻿using SH5ApiClient.Data;
using SH5ApiClient.Models.DTO.GDoc;

namespace SH5ApiClient
{
    public class ApiClient : IApiClient
    {
        private readonly ConnectionParamSH5 _connectionParam;
        private readonly IWebClient _webClient;
        public ApiClient(ConnectionParamSH5 connectionParamSH5, IWebClient? webClient = null)
        {
            _connectionParam = connectionParamSH5 ?? throw new ArgumentNullException(nameof(connectionParamSH5));
            _webClient = webClient ?? new WebClient();
        }
        public async Task<IEnumerable<GDocHeader>> LoadGDocsAsync(DateTime? dateFrom, DateTime? dateTo, TTNTypeForRequest? ttnTypeForRequest, GDocsRequestFilter? gDocsRequestFilter = GDocsRequestFilter.ShowActiveInvoices)
        {
            try
            {
                GDocsRequest request = new(_connectionParam)
                {
                    DateFrom = dateFrom,
                    DateTo = dateTo,
                    TTNTypeForRequest = ttnTypeForRequest,
                    GDocsRequestFilter = gDocsRequestFilter
                };
                string jsonAnswer = await _webClient.WebPostAsync(request);
                var data = DataExecutable.Parse<GDocs>(jsonAnswer);
                return data.Where(t => t.TTNOptions is not null && t.TTNOptions.Value != TTNOptions.Unknown); //При создании и отправки документа через Честный знак создается документ-дубликат с опцией 32771, пока фильтруем.
            }
            catch (Exception ex)
            {
                throw new ApiClientException("Ошибка загрузки списка накладных. Подробности во внутреннем исключении.", ex);
            }
        }
        public async Task<IEnumerable<Depart>> LoadDepartsAsync()
        {
            try
            {
                DepartsRequest departsRequest = new(_connectionParam);
                string jsonAnswer = await _webClient.WebPostAsync(departsRequest);
                return DataExecutable.Parse<Departs>(jsonAnswer);
            }
            catch (Exception ex)
            {
                throw new ApiClientException("Ошибка загрузки справочника подразделений. Подробности во внутреннем исключении.", ex);
            }
        }
        public async Task<Depart?> GetDepartAsync(uint rid, string guid)
        {
            try
            {
                DepartRequest departRequest = new(_connectionParam, rid, guid);
                string jsonAnswer = await _webClient.WebPostAsync(departRequest);
                var dep = DataExecutable.Parse<Depart>(jsonAnswer);
                return dep;
            }
            catch (Exception ex)
            {
                throw new ApiClientException("Ошибка загрузки информации о подразделении. Подробности во внутреннем исключении.", ex);
            }
        }
        public async Task<IEnumerable<Сorrespondent>> LoadCorrespondentsAsync()
        {
            CorrsRequest corrsRequest = new(_connectionParam);
            string jsonAnswer = await _webClient.WebPostAsync(corrsRequest);
            return DataExecutable.Parse<Сorrespondents>(jsonAnswer);
        }
        public async Task<AbleOperation> GetPermissionExecuteProcedure(IEnumerable<string> procedureNames)
        {
            AbleRequest ableRequest = new(_connectionParam, procedureNames);
            string jsonAnswer = await _webClient.WebPostAsync(ableRequest);
            return OperationBase.Parse<AbleOperation>(jsonAnswer);
        }
        public async Task<IEnumerable<InternalСorrespondent>> LoadInternalCorrespondentsAsync()
        {
            LEntitiesRequest corrsRequest = new(_connectionParam);
            string jsonAnswer = await _webClient.WebPostAsync(corrsRequest);
            return DataExecutable.Parse<InternalСorrespondents>(jsonAnswer);
        }
        public async Task<Dictionary<int, string>> LoadEnumeratedAttributeValuesAsync(string head, string path)
        {
            EnumValuesRequest request = new(_connectionParam, head, path);
            string jsonAnswer = await _webClient.WebPostAsync(request);
            return OperationBase.Parse<EnumOperation>(jsonAnswer).GetValues();
        }
        public Task UpdateCorrespondentAsync(string guid, string? bankName, string? bankAccount, string? bik, string? corAccount)
        {
            if (string.IsNullOrWhiteSpace(guid) && !Guid.TryParse(guid, out Guid _))
                throw new ArgumentException($"\"{nameof(guid)}\" не может быть пустым или содержать только пробел.", nameof(guid));
            return UpdateCorrespondentAsyncInternal(guid, bankName, bankAccount, bik, corAccount);
        }
        private async Task UpdateCorrespondentAsyncInternal(string guid, string? bankName, string? bankAccount, string? bik, string? corAccount)
        {
            CorrRequest request = new(_connectionParam, guid);
            string jsonAnswer = await _webClient.WebPostAsync(request);
            if (bankName is not null)
                jsonAnswer = ExecOperation.ChangeValue(jsonAnswer, "107", "34\\Bank_Name", bankName);
            if (bankAccount is not null)
                jsonAnswer = ExecOperation.ChangeValue(jsonAnswer, "107", "34\\Bank_PAcc", bankAccount);
            if (bik is not null)
                jsonAnswer = ExecOperation.ChangeValue(jsonAnswer, "107", "34\\Bank_BIK", bik);
            if (corAccount is not null)
                jsonAnswer = ExecOperation.ChangeValue(jsonAnswer, "107", "34\\Bank_CAcc", corAccount);
            string newRequest = ExecOperation.ConvertToRequest(jsonAnswer, "107", _connectionParam, "UpdCorr");
            string newRequestResult = await _webClient.WebPostAsync(newRequest, _connectionParam);
            OperationBase.Parse<ExecOperation>(newRequestResult);
        }
        public async Task<Сorrespondent?> CreateNewCorrespondentAsync(string name, string inn, string? bankAccount, string? bik, string? bankName, string? corAccount, CorrType corrType, CorrTypeEx corrTypeEx)
        {
            InsCorrRequest corr = new(_connectionParam, name, inn)
            {
                CorrType = corrType,
                CorrTypeEx = corrTypeEx,
                BankAccount = bankAccount,
                BIK = bik,
                BankName = bankName,
                CorAccount = corAccount
            };
            string result = await _webClient.WebPostAsync(corr);
            return DataExecutable.Parse<Сorrespondents>(result).First();

        }
        public async Task<InfoOperation> GetSHServerInfoAsync()
        {
            string answear = await _webClient.WebPostAsync(new SHInfoRequest(_connectionParam));
            return OperationBase.Parse<InfoOperation>(answear);
        }
        public async Task<IEnumerable<Currency>> LoadCurrenciesAsync()
        {
            CurrenciesRequest request = new(_connectionParam);
            string jsonAnswer = await _webClient.WebPostAsync(request);
            return DataExecutable.Parse<Currencies>(jsonAnswer);
        }
        public async Task<IEnumerable<MeasureGroup>> LoadMeasureGroupsAsync()
        {
            MGroupsRequest request = new(_connectionParam);
            string jsonAnswer = await _webClient.WebPostAsync(request);
            return DataExecutable.Parse<MeasureGroups>(jsonAnswer);
        }
        public async Task<IEnumerable<MeasureUnit>> LoadMeasureUnitsAsync(uint? groupRid = null)
        {
            MUnitsRequest request = new(_connectionParam, groupRid);
            string jsonAnswer = await _webClient.WebPostAsync(request);
            return DataExecutable.Parse<MeasureUnits>(jsonAnswer);
        }
        public async Task<MeasureGroup?> GetMeasureGroupAsync(uint rid)
        {
            MGroupRequest request = new(_connectionParam, rid);
            string jsonAnswer = await _webClient.WebPostAsync(request);
            return DataExecutable.Parse<MeasureGroup>(jsonAnswer);
        }
        public async Task<GDoc0?> GetGDoc0Async(uint rid, string guid)
        {
            GDocRequest request = new(_connectionParam, TTNType.PurchaseInvoice, rid, guid);
            string jsonAnswer = await _webClient.WebPostAsync(request);
            ExecOperation answer = OperationBase.Parse<ExecOperation>(jsonAnswer);
            return GDoc0.Parse(answer);
        }
        public async Task<GDoc4?> GetGDoc4Async(uint rid, string guid)
        {
            GDocRequest request = new(_connectionParam, TTNType.SalesInvoice, rid, guid);
            string jsonAnswer = await _webClient.WebPostAsync(request);
            return DataExecutable.Parse<GDoc4>(jsonAnswer);
        }
        public async Task<GDoc4?> UpdateGDoc4(GDoc4 doc)
        {
            UpdGDoc4Request request = new UpdGDoc4Request(_connectionParam, doc);
            string jsonAnswer = await _webClient.WebPostAsync(request);
            return DataExecutable.Parse<GDoc4>(jsonAnswer);
        }
        public async Task<GDoc5?> GetGDoc5Async(uint rid, string guid)
        {
            GDocRequest request = new(_connectionParam, TTNType.ReturnSupplier, rid, guid);
            string jsonAnswer = await _webClient.WebPostAsync(request);
            return DataExecutable.Parse<GDoc5>(jsonAnswer);
        }
        public async Task<GDoc8?> GetGDoc8Async(uint rid, string guid)
        {
            GDocRequest request = new(_connectionParam, TTNType.CollationStatement, rid, guid);
            string jsonAnswer = await _webClient.WebPostAsync(request);
            return DataExecutable.Parse<GDoc8>(jsonAnswer);
        }
        public async Task<GDoc8Diffs?> GetGDoc8DiffsAsync(uint rid, string guid)
        {
            GDocRequest request = new(_connectionParam, TTNType.CollationStatementDiffs, rid, guid);
            string jsonAnswer = await _webClient.WebPostAsync(request);
            return DataExecutable.Parse<GDoc8Diffs>(jsonAnswer);
        }
        public async Task<GDoc10?> GetGDoc10Async(uint rid, string guid)
        {
            GDocRequest request = new(_connectionParam, TTNType.ActProcessing, rid, guid);
            string jsonAnswer = await _webClient.WebPostAsync(request);
            return DataExecutable.Parse<GDoc10>(jsonAnswer);
        }
        public async Task<GDoc11?> GetGDoc11Async(uint rid, string guid)
        {
            GDocRequest request = new(_connectionParam, TTNType.InternalMovement, rid, guid);
            string jsonAnswer = await _webClient.WebPostAsync(request);
            ExecOperation answear = OperationBase.Parse<ExecOperation>(jsonAnswer);
            return GDoc11.Parse(answear);
        }
        public async Task<IEnumerable<GGroup>> LoadGGroupsAsync()
        {
            GGroupsRequest request = new(_connectionParam);
            string jsonAnswer = await _webClient.WebPostAsync(request);
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
        public async Task<IEnumerable<GoodsItem>> LoadGoodsFromGGroupAsync(uint groupRid)
        {
            GoodsRequest request = new(_connectionParam, groupRid);
            string jsonAnswer = await _webClient.WebPostAsync(request);
            ExecOperation answear = OperationBase.Parse<ExecOperation>(jsonAnswer);
            return GoodsItem.ParseGoods(answear);
        }

    }
}
