using SH5ApiClient.Data;

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
                string jsonAnswear = await _webClient.WebPostAsync(request);
                ExecOperation answear = OperationBase.Parse<ExecOperation>(jsonAnswear);
                ExecOperationContent content = answear.GetAnswearContent("111");
                return GDocHeader.GetGDocsFromSHAnswear(content)
                    .Where(t => t.TTNOptions is not null && t.TTNOptions.Value != TTNOptions.Unknown); //При создании и отправки документа через Честный знак создается документ-дубликат с опцией 32771, пока фильтруем.
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
                string jsonAnswear = await _webClient.WebPostAsync(departsRequest);
                return DataExecutable.Parse<Departs>(jsonAnswear);
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
                string jsonAnswear = await _webClient.WebPostAsync(departRequest);
                var dep = DataExecutable.Parse<Depart>(jsonAnswear);
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
            string jsonAnswear = await _webClient.WebPostAsync(corrsRequest);
            return DataExecutable.Parse<Сorrespondents>(jsonAnswear);
        }
        public async Task<AbleOperation> GetPermissionExecuteProcedure(IEnumerable<string> procedureNames)
        {
            AbleRequest ableRequest = new(_connectionParam, procedureNames);
            string jsonAnswear = await _webClient.WebPostAsync(ableRequest);
            return OperationBase.Parse<AbleOperation>(jsonAnswear);
        }
        public async Task<IEnumerable<InternalСorrespondent>> LoadInternalCorrespondentsAsync()
        {
            LEntitiesRequest corrsRequest = new(_connectionParam);
            string jsonAnswear = await _webClient.WebPostAsync(corrsRequest);
            return DataExecutable.Parse<InternalСorrespondents>(jsonAnswear);
        }
        public async Task<Dictionary<int, string>> LoadEnumeratedAttributeValuesAsync(string head, string path)
        {
            EnumValuesRequest request = new(_connectionParam, head, path);
            string jsonAnswear = await _webClient.WebPostAsync(request);
            return OperationBase.Parse<EnumOperation>(jsonAnswear).GetValues();
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
            string jsonAnswear = await _webClient.WebPostAsync(request);
            if (bankName is not null)
                jsonAnswear = ExecOperation.ChangeValue(jsonAnswear, "107", "34\\Bank_Name", bankName);
            if (bankAccount is not null)
                jsonAnswear = ExecOperation.ChangeValue(jsonAnswear, "107", "34\\Bank_PAcc", bankAccount);
            if (bik is not null)
                jsonAnswear = ExecOperation.ChangeValue(jsonAnswear, "107", "34\\Bank_BIK", bik);
            if (corAccount is not null)
                jsonAnswear = ExecOperation.ChangeValue(jsonAnswear, "107", "34\\Bank_CAcc", corAccount);
            string newRequest = ExecOperation.ConvertToRequest(jsonAnswear, "107", _connectionParam, "UpdCorr");
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
            string jsonAnswear = await _webClient.WebPostAsync(request);
            return DataExecutable.Parse<Currencies>(jsonAnswear);
        }
        public async Task<IEnumerable<MeasureGroup>> LoadMeasureGroupsAsync()
        {
            MGroupsRequest request = new(_connectionParam);
            string jsonAnswear = await _webClient.WebPostAsync(request);
            return DataExecutable.Parse<MeasureGroups>(jsonAnswear);
        }
        public async Task<IEnumerable<MeasureUnit>> LoadMeasureUnitsAsync(uint? groupRid = null)
        {
            MUnitsRequest request = new(_connectionParam, groupRid);
            string jsonAnswear = await _webClient.WebPostAsync(request);
            return DataExecutable.Parse<MeasureUnits>(jsonAnswear);
        }
        public async Task<MeasureGroup?> GetMeasureGroupAsync(uint rid)
        {
            MGroupRequest request = new(_connectionParam, rid);
            string jsonAnswear = await _webClient.WebPostAsync(request);
            return DataExecutable.Parse<MeasureGroup>(jsonAnswear);
        }
        public async Task<GDoc0?> GetGDoc0Async(uint rid, string guid)
        {
            GDocRequest request = new(_connectionParam, TTNType.PurchaseInvoice, rid, guid);
            string jsonAnswear = await _webClient.WebPostAsync(request);
            ExecOperation answear = OperationBase.Parse<ExecOperation>(jsonAnswear);
            return GDoc0.Parse(answear);
        }
        public async Task<GDoc4?> GetGDoc4Async(uint rid, string guid)
        {
            GDocRequest request = new(_connectionParam, TTNType.SalesInvoice, rid, guid);
            string jsonAnswear = await _webClient.WebPostAsync(request);
            return DataExecutable.Parse<GDoc4>(jsonAnswear);
        }
        public async Task<GDoc5?> GetGDoc5Async(uint rid, string guid)
        {
            GDocRequest request = new(_connectionParam, TTNType.ReturnSupplier, rid, guid);
            string jsonAnswear = await _webClient.WebPostAsync(request);
            return DataExecutable.Parse<GDoc5>(jsonAnswear);
        }
        public async Task<GDoc8?> GetGDoc8Async(uint rid, string guid)
        {
            GDocRequest request = new(_connectionParam, TTNType.CollationStatement, rid, guid);
            string jsonAnswear = await _webClient.WebPostAsync(request);
            return DataExecutable.Parse<GDoc8>(jsonAnswear);
        }
        public async Task<GDoc8Diffs?> GetGDoc8DiffsAsync(uint rid, string guid)
        {
            GDocRequest request = new(_connectionParam, TTNType.CollationStatementDiffs, rid, guid);
            string jsonAnswear = await _webClient.WebPostAsync(request);
            return DataExecutable.Parse<GDoc8Diffs>(jsonAnswear);
        }
        public async Task<GDoc10?> GetGDoc10Async(uint rid, string guid)
        {
            GDocRequest request = new(_connectionParam, TTNType.ActProcessing, rid, guid);
            string jsonAnswear = await _webClient.WebPostAsync(request);
            return DataExecutable.Parse<GDoc10>(jsonAnswear);
        }
        public async Task<GDoc11?> GetGDoc11Async(uint rid, string guid)
        {
            GDocRequest request = new(_connectionParam, TTNType.InternalMovement, rid, guid);
            string jsonAnswear = await _webClient.WebPostAsync(request);
            ExecOperation answear = OperationBase.Parse<ExecOperation>(jsonAnswear);
            return GDoc11.Parse(answear);
        }
        public async Task<IEnumerable<GGroup>> LoadGGroupsAsync()
        {
            GGroupsRequest request = new(_connectionParam);
            string jsonAnswear = await _webClient.WebPostAsync(request);
            var groups = DataExecutable.Parse<GGroups>(jsonAnswear);

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
            string jsonAnswear = await _webClient.WebPostAsync(request);
            ExecOperation answear = OperationBase.Parse<ExecOperation>(jsonAnswear);
            return GoodsItem.ParseGoods(answear);
        }

    }
}
