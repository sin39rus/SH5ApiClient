﻿namespace SH5ApiClient
{
    public class SH5ApiClient : ISH5ApiClient
    {
        private readonly ConnectionParamSH5 _connectionParamSH5;

        public SH5ApiClient(ConnectionParamSH5 connectionParamSH5)
        {
            _connectionParamSH5 = connectionParamSH5 ?? throw new ArgumentNullException(nameof(connectionParamSH5));
        }

        public async Task<IEnumerable<СorrespondentSH>> LoadCorrespondentsAsync()
        {
            try
            {
                CorrsRequest corrsRequest = new(_connectionParamSH5);
                string jsonAnswear = await WebClient.WebPostAsync(corrsRequest);
                SHExecAnswear answear = SHExecAnswear.Parse(jsonAnswear);
                SHExecAnswearContent content = answear.GetAnswearContent("107");
                return СorrespondentSH.GetСorrespondentsFromSHAnswear(content);
            }
            catch (Exception ex)
            {
                throw new SH5ApiClientException("Ошибка загрузки справочника корреспондентов из SH.", ex);
            }
        }

        public async Task<IEnumerable<InternalСorrespondentSH>> LoadInternalСorrespondentsAsync()
        {
            try
            {
                LEntitiesRequest corrsRequest = new(_connectionParamSH5);
                string jsonAnswear = await WebClient.WebPostAsync(corrsRequest);
                SHExecAnswear answear = SHExecAnswear.Parse(jsonAnswear);
                SHExecAnswearContent content = answear.GetAnswearContent("102");
                return InternalСorrespondentSH.GetСorrespondentsFromSHAnswear(content);
            }
            catch (Exception ex)
            {
                throw new SH5ApiClientException("Ошибка загрузки справочника внутренних корреспондентов из SH.", ex);
            }
        }

        public async Task<Dictionary<string, int>> LoadBankAccountsAsync()
        {
            EnumValuesRequest request = new(_connectionParamSH5, "119", "6\\Payment_Place");
            string jsonAnswear = await WebClient.WebPostAsync(request);
            SHEnumAnswear answear = SHEnumAnswear.Parse(jsonAnswear);
            return answear.GetBankAccounts('#');
        }

        public Task UpdateCorrespondentAsync(string guid, string? bankName, string? bankAccount, string? bik, string? corAccount)
        {
            if (string.IsNullOrWhiteSpace(guid) && !Guid.TryParse(guid, out Guid _))
                throw new ArgumentException($"\"{nameof(guid)}\" не может быть пустым или содержать только пробел.", nameof(guid));
            return UpdateCorrespondentAsyncInternal(guid, bankName, bankAccount, bik, corAccount);
        }

        private async Task UpdateCorrespondentAsyncInternal(string guid, string? bankName, string? bankAccount, string? bik, string? corAccount)
        {
            CorrRequest request = new(_connectionParamSH5, guid);
            string jsonAnswear = await WebClient.WebPostAsync(request);
            if (bankName is not null)
                jsonAnswear = SHExecAnswear.ChangeValue(jsonAnswear, "107", "34\\Bank_Name", bankName);
            if (bankAccount is not null)
                jsonAnswear = SHExecAnswear.ChangeValue(jsonAnswear, "107", "34\\Bank_PAcc", bankAccount);
            if (bik is not null)
                jsonAnswear = SHExecAnswear.ChangeValue(jsonAnswear, "107", "34\\Bank_BIK", bik);
            if (corAccount is not null)
                jsonAnswear = SHExecAnswear.ChangeValue(jsonAnswear, "107", "34\\Bank_CAcc", corAccount);
            string newRequest = SHExecAnswear.ConvertToRequest(jsonAnswear, "107", _connectionParamSH5, "UpdCorr");
            string newRequestResult = await WebClient.WebPostAsync(newRequest, _connectionParamSH5, ServerOperationType.sh5exec);
            SHExecAnswear.Parse(newRequestResult);
        }

        public async Task<СorrespondentSH> CreateNewCorrespondentAsync(string name, string inn, string? bankAccount, string? bik, string? bankName, string? corAccount, CorrType corrType, CorrTypeEx corrTypeEx)
        {
            InsCorrRequest corr = new(_connectionParamSH5, name, inn)
            {
                CorrType = corrType,
                CorrTypeEx = corrTypeEx,
                BankAccount = bankAccount,
                BIK = bik,
                BankName = bankName,
                CorAccount = corAccount
            };
            string result = await WebClient.WebPostAsync(corr);
            var answear = SHExecAnswear.Parse(result);
            return СorrespondentSH.Parse(answear.GetAnswearContent("107").GetValues()[0]);

        }
    }
}
