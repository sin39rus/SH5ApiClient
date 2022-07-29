namespace SH5ApiClient
{
    public class ApiClient : IApiClient
    {
        private readonly ConnectionParamSH5 _connectionParam;
        public ApiClient(ConnectionParamSH5 connectionParamSH5)
        {
            _connectionParam = connectionParamSH5 ?? throw new ArgumentNullException(nameof(connectionParamSH5));
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
                string jsonAnswear = await WebClient.WebPostAsync(request);
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
        public async Task<IEnumerable<Depart>> LoadDeparts()
        {
            try
            {
                DepartsRequest departsRequest = new DepartsRequest(_connectionParam);
                string jsonAnswear = await WebClient.WebPostAsync(departsRequest);
                ExecOperation answear = OperationBase.Parse<ExecOperation>(jsonAnswear);
                ExecOperationContent content = answear.GetAnswearContent("106");
                return Depart.GetDepartsFromSHAnswear(content);
            }
            catch (Exception ex)
            {
                throw new ApiClientException("Ошибка загрузки справочника подразделений. Подробности во внутреннем исключении.", ex);
            }
        }
        public async Task<Depart> GetDepart(uint rid, string guid)
        {
            try
            {
                DepartRequest departRequest = new DepartRequest(_connectionParam, rid, guid);
                string jsonAnswear = await WebClient.WebPostAsync(departRequest);
                ExecOperation answear = OperationBase.Parse<ExecOperation>(jsonAnswear);
                ExecOperationContent content = answear.GetAnswearContent("107");
                return null;
            }
            catch (Exception ex)
            {
                throw new ApiClientException("Ошибка загрузки информации о подразделении. Подробности во внутреннем исключении.", ex);
            }
        }
        public async Task<IEnumerable<Сorrespondent?>> LoadCorrespondentsAsync()
        {
            try
            {
                CorrsRequest corrsRequest = new(_connectionParam);
                string jsonAnswear = await WebClient.WebPostAsync(corrsRequest);
                ExecOperation answear = OperationBase.Parse<ExecOperation>(jsonAnswear);
                ExecOperationContent content = answear.GetAnswearContent("107");
                return Сorrespondent.GetСorrespondentsFromSHAnswear(content);
            }
            catch (Exception ex)
            {
                throw new ApiClientException("Ошибка загрузки справочника корреспондентов. Подробности во внутреннем исключении.", ex);
            }
        }
        public async Task<AbleOperation> GetPermissionExecuteProcedure(IEnumerable<string> procedureNames)
        {
            AbleRequest ableRequest = new(_connectionParam, procedureNames);
            string jsonAnswear = await WebClient.WebPostAsync(ableRequest);
            return OperationBase.Parse<AbleOperation>(jsonAnswear);
        }
        public async Task<GDoc0?> GetGDoc0Async(uint rid, string guid)
        {
            GDocRequest request = new(_connectionParam, TTNType.PurchaseInvoice, rid, guid);
            string jsonAnswear = await WebClient.WebPostAsync(request);
            ExecOperation answear = OperationBase.Parse<ExecOperation>(jsonAnswear);
            return GDoc0.Parse(answear);
        }
        public async Task<IEnumerable<Сorrespondent?>> LoadInternalCorrespondentsAsync()
        {
            try
            {
                LEntitiesRequest corrsRequest = new(_connectionParam);
                string jsonAnswear = await WebClient.WebPostAsync(corrsRequest);
                ExecOperation answear = OperationBase.Parse<ExecOperation>(jsonAnswear);
                ExecOperationContent content = answear.GetAnswearContent("102");
                return Сorrespondent.GetСorrespondentsFromSHAnswear(content);
            }
            catch (Exception ex)
            {
                throw new ApiClientException("Ошибка загрузки справочника внутренних корреспондентов из SH.", ex);
            }
        }
        public async Task<Dictionary<int, string>> LoadEnumeratedAttributeValuesAsync(string head, string path)
        {
            EnumValuesRequest request = new(_connectionParam, head, path);
            string jsonAnswear = await WebClient.WebPostAsync(request);
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
            string jsonAnswear = await WebClient.WebPostAsync(request);
            if (bankName is not null)
                jsonAnswear = ExecOperation.ChangeValue(jsonAnswear, "107", "34\\Bank_Name", bankName);
            if (bankAccount is not null)
                jsonAnswear = ExecOperation.ChangeValue(jsonAnswear, "107", "34\\Bank_PAcc", bankAccount);
            if (bik is not null)
                jsonAnswear = ExecOperation.ChangeValue(jsonAnswear, "107", "34\\Bank_BIK", bik);
            if (corAccount is not null)
                jsonAnswear = ExecOperation.ChangeValue(jsonAnswear, "107", "34\\Bank_CAcc", corAccount);
            string newRequest = ExecOperation.ConvertToRequest(jsonAnswear, "107", _connectionParam, "UpdCorr");
            string newRequestResult = await WebClient.WebPostAsync(newRequest, _connectionParam);
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
            string result = await WebClient.WebPostAsync(corr);
            var answear = OperationBase.Parse<ExecOperation>(result);
            return Сorrespondent.Parse(answear.GetAnswearContent("107").GetValues()[0]);

        }
        public async Task<InfoOperation> GetSHServerInfoAsync()
        {
            string answear = await WebClient.WebPostAsync(new SHInfoRequest(_connectionParam));
            return OperationBase.Parse<InfoOperation>(answear);
        }

        public async Task<GDoc4?> GetGDoc4Async(uint rid, string guid)
        {
            GDocRequest request = new(_connectionParam, TTNType.SalesInvoice, rid, guid);
            string jsonAnswear = await WebClient.WebPostAsync(request);
            ExecOperation answear = OperationBase.Parse<ExecOperation>(jsonAnswear);
            return GDoc4.Parse(answear);
        }
        public async Task<GDoc11?> GetGDoc11Async(uint rid, string guid)
        {
            GDocRequest request = new(_connectionParam, TTNType.InternalMovement, rid, guid);
            string jsonAnswear = await WebClient.WebPostAsync(request);
            ExecOperation answear = OperationBase.Parse<ExecOperation>(jsonAnswear);
            return GDoc11.Parse(answear);
        }

        public async Task<IEnumerable<GGroup>> LoadGGroupsAsync()
        {
            GGroupsRequest request = new(_connectionParam);
            string jsonAnswear = await WebClient.WebPostAsync(request);
            ExecOperation answear = OperationBase.Parse<ExecOperation>(jsonAnswear);
            return GGroup.ParseGGroups(answear);
        }

        public async Task<IEnumerable<GoodsItem>> LoadGoodsFromGGroup(uint groupRid)
        {
            GoodsRequest request = new(_connectionParam, groupRid);
            string jsonAnswear = await WebClient.WebPostAsync(request);
            ExecOperation answear = OperationBase.Parse<ExecOperation>(jsonAnswear);
            return GoodsItem.ParseGoods(answear);
        }
    }
}
