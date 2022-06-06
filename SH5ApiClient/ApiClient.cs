namespace SH5ApiClient
{
    public class ApiClient : IApiClient
    {
        private readonly ConnectionParamSH5 _connectionParam;

        public ApiClient(ConnectionParamSH5 connectionParamSH5)
        {
            _connectionParam = connectionParamSH5 ?? throw new ArgumentNullException(nameof(connectionParamSH5));
        }

        public async Task<IEnumerable<СorrespondentSH>> LoadCorrespondentsAsync()
        {
            try
            {
                CorrsRequest corrsRequest = new(_connectionParam);
                string jsonAnswear = await WebClient.WebPostAsync(corrsRequest);
                ExecOperation answear = ExecOperation.Parse(jsonAnswear);
                ExecOperationContent content = answear.GetAnswearContent("107");
                return СorrespondentSH.GetСorrespondentsFromSHAnswear(content);
            }
            catch (Exception ex)
            {
                throw new ApiClientException("Ошибка загрузки справочника корреспондентов из SH.", ex);
            }
        }
        public async Task<AbleOperation> RequestPermissionExecuteProcedure(IEnumerable<string> procedureNames)
        {
            AbleRequest ableRequest = new(_connectionParam, procedureNames);
            string jsonAnswear = await WebClient.WebPostAsync(ableRequest);
            AbleOperation answear = AbleOperation.Parse(jsonAnswear);
            return answear;
        }
        public async Task<IEnumerable<InternalСorrespondentSH>> LoadInternalСorrespondentsAsync()
        {
            try
            {
                LEntitiesRequest corrsRequest = new(_connectionParam);
                string jsonAnswear = await WebClient.WebPostAsync(corrsRequest);
                ExecOperation answear = ExecOperation.Parse(jsonAnswear);
                ExecOperationContent content = answear.GetAnswearContent("102");
                return InternalСorrespondentSH.GetСorrespondentsFromSHAnswear(content);
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
            return EnumOperation.Parse(jsonAnswear).GetValues();
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
            string newRequestResult = await WebClient.WebPostAsync(newRequest, _connectionParam, ServerOperationType.sh5exec);
            ExecOperation.Parse(newRequestResult);
        }

        public async Task<СorrespondentSH> CreateNewCorrespondentAsync(string name, string inn, string? bankAccount, string? bik, string? bankName, string? corAccount, CorrType corrType, CorrTypeEx corrTypeEx)
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
            var answear = ExecOperation.Parse(result);
            return СorrespondentSH.Parse(answear.GetAnswearContent("107").GetValues()[0]);

        }
        public async Task<InfoOperation> GetSHServerInfoAsync()
        {
            string answear = await WebClient.WebPostAsync(new SHInfoRequest(_connectionParam));
            return InfoOperation.Parse(answear);
        }
    }
}
