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

namespace SH5ApiClient
{
    public class ApiClient : IApiClient
    {
        private const string ErrorLoadGDocs = "Ошибка загрузки списка накладных. Подробности во внутреннем исключении.";
        private const string ErrorLoadDeparts = "Ошибка загрузки справочника подразделений. Подробности во внутреннем исключении.";
        private const string ErrorGetDepart = "Ошибка загрузки информации о подразделении. Подробности во внутреннем исключении.";
        private const string ErrorLoadCorrespondents = "Ошибка загрузки справочника корреспондентов. Подробности во внутреннем исключении.";
        private const string ErrorGetPermission = "Ошибка проверки прав на выполнение процедур. Подробности во внутреннем исключении.";
        private const string ErrorLoadInternalCorrespondents = "Ошибка загрузки справочника внутренних корреспондентов. Подробности во внутреннем исключении.";
        private const string ErrorLoadEnumAttributes = "Ошибка загрузки значений перечисляемого атрибута. Подробности во внутреннем исключении.";
        private const string ErrorUpdateCorrespondent = "Ошибка обновления корреспондента. Подробности во внутреннем исключении.";
        private const string ErrorCreateCorrespondent = "Ошибка создания корреспондента. Подробности во внутреннем исключении.";
        private const string ErrorGetServerInfo = "Ошибка получения информации о сервере SH. Подробности во внутреннем исключении.";
        private const string ErrorLoadCurrencies = "Ошибка загрузки справочника валют. Подробности во внутреннем исключении.";
        private const string ErrorLoadMeasureGroups = "Ошибка загрузки групп единиц измерения. Подробности во внутреннем исключении.";
        private const string ErrorLoadMeasureUnits = "Ошибка загрузки единиц измерения. Подробности во внутреннем исключении.";
        private const string ErrorGetMeasureGroup = "Ошибка загрузки группы единиц измерения. Подробности во внутреннем исключении.";
        private const string ErrorGetGDoc0 = "Ошибка загрузки приходной накладной. Подробности во внутреннем исключении.";
        private const string ErrorGetGDoc4 = "Ошибка загрузки расходной накладной. Подробности во внутреннем исключении.";
        private const string ErrorUpdateGDoc4 = "Ошибка обновления расходной накладной. Подробности во внутреннем исключении.";
        private const string ErrorGetGDoc5 = "Ошибка загрузки возврата поставщику. Подробности во внутреннем исключении.";
        private const string ErrorGetGDoc8 = "Ошибка загрузки акта сверки. Подробности во внутреннем исключении.";
        private const string ErrorGetGDoc8Diffs = "Ошибка загрузки расхождений акта сверки. Подробности во внутреннем исключении.";
        private const string ErrorGetGDoc10 = "Ошибка загрузки акта переработки. Подробности во внутреннем исключении.";
        private const string ErrorGetGDoc11 = "Ошибка загрузки внутреннего перемещения. Подробности во внутреннем исключении.";
        private const string ErrorLoadGGroups = "Ошибка загрузки групп товаров. Подробности во внутреннем исключении.";
        private const string ErrorLoadGoodsFromGGroup = "Ошибка загрузки товаров из группы. Подробности во внутреннем исключении.";
        private const string ErrorGetGoodsMUnits = "Ошибка загрузки единиц измерения товара. Подробности во внутреннем исключении.";
        private const string ErrorLoadGoodsTree = "Ошибка загрузки дерева товаров. Подробности во внутреннем исключении.";
        private const string ErrorCreateGood = "Ошибка создания товара. Подробности во внутреннем исключении.";
        private const string ErrorCreateMeasureUnit = "Ошибка создания единицы измерения. Подробности во внутреннем исключении.";
        private const string ErrorGetGoodsItem = "Ошибка загрузки товара. Подробности во внутреннем исключении.";
        private const string ErrorCreateIncomingTTN = "Ошибка создания приходной ТТН. Подробности во внутреннем исключении.";
        private const string ErrorGetNdsList = "Ошибка загрузки списка ставок НДС. Подробности во внутреннем исключении.";
        private const string ErrorCreateGtd = "Ошибка создания ГТД. Подробности во внутреннем исключении.";
        private const string ErrorDocsByCorrsReport = "Ошибка загрузки отчёта по документам корреспондента. Подробности во внутреннем исключении.";
        private const string ErrorGDocsExReport = "Ошибка загрузки расширенного отчёта по накладным. Подробности во внутреннем исключении.";

        /// <summary>Дубликат документа из Честного знака (TTNOptions.Unknown = 32771).</summary>
        private static readonly TTNOptions HonestSignDuplicateOption = TTNOptions.Unknown;

        private readonly ConnectionParamSH5 _connectionParam;
        private readonly IWebClient _webClient;

        public ApiClient(ConnectionParamSH5 connectionParamSH5, IWebClient webClient = null)
        {
            _connectionParam = connectionParamSH5 ?? throw new ArgumentNullException(nameof(connectionParamSH5));
            _webClient = webClient ?? new WebClient(_connectionParam);
        }

        private Task<string> PostAsync(RequestBase request, CancellationToken cancellationToken) =>
            _webClient.WebPostAsync(request, cancellationToken);

        private Task<string> PostAsync(string request, CancellationToken cancellationToken) =>
            _webClient.WebPostAsync(request, cancellationToken);

        private async Task<T> ExecuteAsync<T>(Func<Task<T>> action, string errorMessage)
        {
            try
            {
                return await action();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (ApiClientException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApiClientException(errorMessage, ex);
            }
        }

        private Task ExecuteAsync(Func<Task> action, string errorMessage) =>
            ExecuteAsync(async () => { await action(); return true; }, errorMessage);

        private static void RequireGuid(string guid, string paramName)
        {
            if (string.IsNullOrWhiteSpace(guid) || !Guid.TryParse(guid, out _))
                throw new ArgumentException($"\"{paramName}\" не может быть пустым, содержать только пробелы или иметь неверный формат.", paramName);
        }

        private static void RequireNotNullOrWhiteSpace(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"\"{paramName}\" не может быть пустым или содержать только пробелы.", paramName);
        }

        private static ExecOperationContent GetRequiredContent(ExecOperation answer, string head)
        {
            try
            {
                return answer.GetAnswearContent(head);
            }
            catch (ArgumentException ex)
            {
                throw new ApiClientException($"Ответ сервера не содержит блок данных \"{head}\".", ex);
            }
        }

        private static Dictionary<string, string> GetRequiredFirstRow(ExecOperation answer, string head)
        {
            Dictionary<string, string>[] values = GetRequiredContent(answer, head).GetValues();
            if (values == null || values.Length == 0)
                throw new ApiClientException($"Ответ сервера не содержит строк в блоке данных \"{head}\".");
            return values[0];
        }

        private static string GetRequiredField(Dictionary<string, string> row, string field, string head)
        {
            if (row == null || !row.TryGetValue(field, out string value) || value == null)
                throw new ApiClientException($"Ответ сервера не содержит поле \"{field}\" в блоке данных \"{head}\".");
            return value;
        }

        private GDocsRequest CreateGDocsRequest(DateTime? dateFrom, DateTime? dateTo, TTNTypeForRequest? ttnTypeForRequest, GDocsRequestFilter? gDocsRequestFilter)
        {
            return new GDocsRequest(_connectionParam)
            {
                DateFrom = dateFrom,
                DateTo = dateTo,
                TTNTypeForRequest = ttnTypeForRequest,
                GDocsRequestFilter = gDocsRequestFilter
            };
        }

        public Task<IEnumerable<GDocHeader>> LoadGDocsAsync(DateTime? dateFrom, DateTime? dateTo, TTNTypeForRequest? ttnTypeForRequest, GDocsRequestFilter? gDocsRequestFilter = GDocsRequestFilter.ShowActiveInvoices) =>
            LoadGDocsAsync(CancellationToken.None, dateFrom, dateTo, ttnTypeForRequest, gDocsRequestFilter);

        public Task<DataSet> LoadGDocsRawAsync(CancellationToken cancellationToken, DateTime? dateFrom, DateTime? dateTo, TTNTypeForRequest? ttnTypeForRequest, GDocsRequestFilter? gDocsRequestFilter = GDocsRequestFilter.ShowActiveInvoices)
        {
            return ExecuteAsync(async () =>
            {
                GDocsRequest request = CreateGDocsRequest(dateFrom, dateTo, ttnTypeForRequest, gDocsRequestFilter);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                return DataSet.ParseFromJson(jsonAnswer);
            }, ErrorLoadGDocs);
        }

        public Task<IEnumerable<GDocHeader>> LoadGDocsAsync(CancellationToken cancellationToken, DateTime? dateFrom, DateTime? dateTo, TTNTypeForRequest? ttnTypeForRequest, GDocsRequestFilter? gDocsRequestFilter = GDocsRequestFilter.ShowActiveInvoices)
        {
            return ExecuteAsync(async () =>
            {
                GDocsRequest request = CreateGDocsRequest(dateFrom, dateTo, ttnTypeForRequest, gDocsRequestFilter);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                var data = DataExecutable.Parse<GDocs>(jsonAnswer);
                // При создании и отправки документа через Честный знак создается документ-дубликат с опцией HonestSignDuplicateOption (32771), пока фильтруем.
                return data.Where(t => t.TTNOptions != null && t.TTNOptions.Value != HonestSignDuplicateOption);
            }, ErrorLoadGDocs);
        }

        public Task<IEnumerable<Depart>> LoadDepartsAsync() =>
            LoadDepartsAsync(CancellationToken.None);

        public Task<IEnumerable<Depart>> LoadDepartsAsync(CancellationToken cancellationToken)
        {
            return ExecuteAsync(async () =>
            {
                DepartsRequest request = new DepartsRequest(_connectionParam);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                return (IEnumerable<Depart>)DataExecutable.Parse<Departs>(jsonAnswer);
            }, ErrorLoadDeparts);
        }

        public Task<Depart> GetDepartAsync(uint rid, string guid) =>
            GetDepartAsync(rid, guid, CancellationToken.None);

        public Task<Depart> GetDepartAsync(uint rid, string guid, CancellationToken cancellationToken)
        {
            RequireGuid(guid, nameof(guid));
            return ExecuteAsync(async () =>
            {
                DepartRequest request = new DepartRequest(_connectionParam, rid, guid);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                return DataExecutable.Parse<Depart>(jsonAnswer);
            }, ErrorGetDepart);
        }

        public Task<IEnumerable<Сorrespondent>> LoadCorrespondentsAsync() =>
            LoadCorrespondentsAsync(CancellationToken.None);

        public Task<IEnumerable<Сorrespondent>> LoadCorrespondentsAsync(CancellationToken cancellationToken)
        {
            return ExecuteAsync(async () =>
            {
                CorrsRequest request = new CorrsRequest(_connectionParam);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                return (IEnumerable<Сorrespondent>)await DataExecutable.ParseAsync<Сorrespondents>(jsonAnswer, cancellationToken);
            }, ErrorLoadCorrespondents);
        }

        public Task<AbleOperation> GetPermissionExecuteProcedure(IEnumerable<string> procedureNames) =>
            GetPermissionExecuteProcedure(procedureNames, CancellationToken.None);

        public Task<AbleOperation> GetPermissionExecuteProcedure(IEnumerable<string> procedureNames, CancellationToken cancellationToken)
        {
            return ExecuteAsync(async () =>
            {
                AbleRequest request = new AbleRequest(_connectionParam, procedureNames);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                return OperationBase.Parse<AbleOperation>(jsonAnswer);
            }, ErrorGetPermission);
        }

        public Task<IEnumerable<InternalСorrespondent>> LoadInternalCorrespondentsAsync() =>
            LoadInternalCorrespondentsAsync(CancellationToken.None);

        public Task<IEnumerable<InternalСorrespondent>> LoadInternalCorrespondentsAsync(CancellationToken cancellationToken)
        {
            return ExecuteAsync(async () =>
            {
                LEntitiesRequest request = new LEntitiesRequest(_connectionParam);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                return (IEnumerable<InternalСorrespondent>)await DataExecutable.ParseAsync<InternalСorrespondents>(jsonAnswer, cancellationToken);
            }, ErrorLoadInternalCorrespondents);
        }

        public Task<Dictionary<int, string>> LoadEnumeratedAttributeValuesAsync(string head, string path) =>
            LoadEnumeratedAttributeValuesAsync(head, path, CancellationToken.None);

        public Task<Dictionary<int, string>> LoadEnumeratedAttributeValuesAsync(string head, string path, CancellationToken cancellationToken)
        {
            return ExecuteAsync(async () =>
            {
                EnumValuesRequest request = new EnumValuesRequest(_connectionParam, head, path);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                return OperationBase.Parse<EnumOperation>(jsonAnswer).GetValues();
            }, ErrorLoadEnumAttributes);
        }

        public Task UpdateCorrespondentAsync(string guid, string bankName, string bankAccount, string bik, string corAccount) =>
            UpdateCorrespondentAsync(guid, bankName, bankAccount, bik, corAccount, CancellationToken.None);

        public Task UpdateCorrespondentAsync(string guid, string bankName, string bankAccount, string bik, string corAccount, CancellationToken cancellationToken)
        {
            RequireGuid(guid, nameof(guid));
            return UpdateCorrespondentAsyncInternal(guid, bankName, bankAccount, bik, corAccount, cancellationToken);
        }

        private Task UpdateCorrespondentAsyncInternal(string guid, string bankName, string bankAccount, string bik, string corAccount, CancellationToken cancellationToken)
        {
            return ExecuteAsync(async () =>
            {
                string jsonAnswer = await PostAsync(new CorrRequest(_connectionParam, guid), cancellationToken);
                if (bankName != null)
                    jsonAnswer = ExecOperation.ChangeValue(jsonAnswer, "107", "34\\Bank_Name", bankName);
                if (bankAccount != null)
                    jsonAnswer = ExecOperation.ChangeValue(jsonAnswer, "107", "34\\Bank_PAcc", bankAccount);
                if (bik != null)
                    jsonAnswer = ExecOperation.ChangeValue(jsonAnswer, "107", "34\\Bank_BIK", bik);
                if (corAccount != null)
                    jsonAnswer = ExecOperation.ChangeValue(jsonAnswer, "107", "34\\Bank_CAcc", corAccount);
                string newRequest = ExecOperation.ConvertToRequest(jsonAnswer, "107", _connectionParam, "UpdCorr");
                string newRequestResult = await PostAsync(newRequest, cancellationToken);
                OperationBase.Parse<ExecOperation>(newRequestResult);
            }, ErrorUpdateCorrespondent);
        }

        public Task<Сorrespondent> CreateNewCorrespondentAsync(string name, string inn, string bankAccount, string bik, string bankName, string corAccount, CorrType corrType, CorrTypeEx corrTypeEx) =>
            CreateNewCorrespondentAsync(name, inn, bankAccount, bik, bankName, corAccount, corrType, corrTypeEx, CancellationToken.None);

        public Task<Сorrespondent> CreateNewCorrespondentAsync(string name, string inn, string bankAccount, string bik, string bankName, string corAccount, CorrType corrType, CorrTypeEx corrTypeEx, CancellationToken cancellationToken)
        {
            RequireNotNullOrWhiteSpace(name, nameof(name));
            return ExecuteAsync(async () =>
            {
                InsCorrRequest request = new InsCorrRequest(_connectionParam, name, inn)
                {
                    CorrType = corrType,
                    CorrTypeEx = corrTypeEx,
                    BankAccount = bankAccount,
                    BIK = bik,
                    BankName = bankName,
                    CorAccount = corAccount
                };
                string result = await PostAsync(request, cancellationToken);
                return DataExecutable.Parse<Сorrespondents>(result).First();
            }, ErrorCreateCorrespondent);
        }

        public Task<InfoOperation> GetSHServerInfoAsync() =>
            GetSHServerInfoAsync(CancellationToken.None);

        public Task<InfoOperation> GetSHServerInfoAsync(CancellationToken cancellationToken)
        {
            return ExecuteAsync(async () =>
            {
                string answer = await PostAsync(new SHInfoRequest(_connectionParam), cancellationToken);
                return OperationBase.Parse<InfoOperation>(answer);
            }, ErrorGetServerInfo);
        }

        public Task<IEnumerable<Currency>> LoadCurrenciesAsync() =>
            LoadCurrenciesAsync(CancellationToken.None);

        public Task<IEnumerable<Currency>> LoadCurrenciesAsync(CancellationToken cancellationToken)
        {
            return ExecuteAsync(async () =>
            {
                CurrenciesRequest request = new CurrenciesRequest(_connectionParam);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                return (IEnumerable<Currency>)DataExecutable.Parse<Currencies>(jsonAnswer);
            }, ErrorLoadCurrencies);
        }

        public Task<IEnumerable<MeasureGroup>> LoadMeasureGroupsAsync() =>
            LoadMeasureGroupsAsync(CancellationToken.None);

        public Task<IEnumerable<MeasureGroup>> LoadMeasureGroupsAsync(CancellationToken cancellationToken)
        {
            return ExecuteAsync(async () =>
            {
                MGroupsRequest request = new MGroupsRequest(_connectionParam);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                return (IEnumerable<MeasureGroup>)DataExecutable.Parse<MeasureGroups>(jsonAnswer);
            }, ErrorLoadMeasureGroups);
        }

        public Task<IEnumerable<MeasureUnit>> LoadMeasureUnitsAsync(uint? groupRid = null) =>
            LoadMeasureUnitsAsync(CancellationToken.None, groupRid);

        public Task<IEnumerable<MeasureUnit>> LoadMeasureUnitsAsync(CancellationToken cancellationToken, uint? groupRid = null)
        {
            return ExecuteAsync(async () =>
            {
                MUnitsRequest request = new MUnitsRequest(_connectionParam, groupRid);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                return (IEnumerable<MeasureUnit>)DataExecutable.Parse<MeasureUnits>(jsonAnswer);
            }, ErrorLoadMeasureUnits);
        }

        public Task<MeasureGroup> GetMeasureGroupAsync(uint rid) =>
            GetMeasureGroupAsync(rid, CancellationToken.None);

        public Task<MeasureGroup> GetMeasureGroupAsync(uint rid, CancellationToken cancellationToken)
        {
            return ExecuteAsync(async () =>
            {
                MGroupRequest request = new MGroupRequest(_connectionParam, rid);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                return DataExecutable.Parse<MeasureGroup>(jsonAnswer);
            }, ErrorGetMeasureGroup);
        }

        public Task<GDoc0> GetGDoc0Async(uint rid, string guid) =>
            GetGDoc0Async(rid, guid, CancellationToken.None);

        public Task<DataSet> GetGDoc0RawAsync(uint rid, string guid, CancellationToken cancellationToken)
        {
            RequireGuid(guid, nameof(guid));
            return ExecuteAsync(async () =>
            {
                GDocRequest request = new GDocRequest(_connectionParam, TTNType.PurchaseInvoice, rid, guid);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                return DataSet.ParseFromJson(jsonAnswer);
            }, ErrorGetGDoc0);
        }

        public Task<GDoc0> GetGDoc0Async(uint rid, string guid, CancellationToken cancellationToken)
        {
            RequireGuid(guid, nameof(guid));
            return ExecuteAsync(async () =>
            {
                GDocRequest request = new GDocRequest(_connectionParam, TTNType.PurchaseInvoice, rid, guid);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                ExecOperation answer = OperationBase.Parse<ExecOperation>(jsonAnswer);
                return GDoc0.Parse(answer);
            }, ErrorGetGDoc0);
        }

        public Task<GDoc4> GetGDoc4Async(uint rid, string guid) =>
            GetGDoc4Async(rid, guid, CancellationToken.None);

        public Task<DataSet> GetGDoc4RawAsync(uint rid, string guid, CancellationToken cancellationToken)
        {
            RequireGuid(guid, nameof(guid));
            return ExecuteAsync(async () =>
            {
                GDocRequest request = new GDocRequest(_connectionParam, TTNType.SalesInvoice, rid, guid);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                return DataSet.ParseFromJson(jsonAnswer);
            }, ErrorGetGDoc4);
        }

        public Task<GDoc4> GetGDoc4Async(uint rid, string guid, CancellationToken cancellationToken)
        {
            RequireGuid(guid, nameof(guid));
            return ExecuteAsync(async () =>
            {
                GDocRequest request = new GDocRequest(_connectionParam, TTNType.SalesInvoice, rid, guid);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                return DataExecutable.Parse<GDoc4>(jsonAnswer);
            }, ErrorGetGDoc4);
        }

        public Task<GDoc4> UpdateGDoc4(GDoc4 doc) =>
            UpdateGDoc4(doc, CancellationToken.None);

        public Task<GDoc4> UpdateGDoc4(GDoc4 doc, CancellationToken cancellationToken)
        {
            if (doc == null)
                throw new ArgumentNullException(nameof(doc));
            return ExecuteAsync(async () =>
            {
                UpdGDoc4Request request = new UpdGDoc4Request(_connectionParam, doc);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                return DataExecutable.Parse<GDoc4>(jsonAnswer);
            }, ErrorUpdateGDoc4);
        }

        public Task<GDoc5> GetGDoc5Async(uint rid, string guid) =>
            GetGDoc5Async(rid, guid, CancellationToken.None);

        public Task<GDoc5> GetGDoc5Async(uint rid, string guid, CancellationToken cancellationToken)
        {
            RequireGuid(guid, nameof(guid));
            return ExecuteAsync(async () =>
            {
                GDocRequest request = new GDocRequest(_connectionParam, TTNType.ReturnSupplier, rid, guid);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                return DataExecutable.Parse<GDoc5>(jsonAnswer);
            }, ErrorGetGDoc5);
        }

        public Task<GDoc8> GetGDoc8Async(uint rid, string guid) =>
            GetGDoc8Async(rid, guid, CancellationToken.None);

        public Task<GDoc8> GetGDoc8Async(uint rid, string guid, CancellationToken cancellationToken)
        {
            RequireGuid(guid, nameof(guid));
            return ExecuteAsync(async () =>
            {
                GDocRequest request = new GDocRequest(_connectionParam, TTNType.CollationStatement, rid, guid);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                return DataExecutable.Parse<GDoc8>(jsonAnswer);
            }, ErrorGetGDoc8);
        }

        public Task<GDoc8Diffs> GetGDoc8DiffsAsync(uint rid, string guid) =>
            GetGDoc8DiffsAsync(rid, guid, CancellationToken.None);

        public Task<GDoc8Diffs> GetGDoc8DiffsAsync(uint rid, string guid, CancellationToken cancellationToken)
        {
            RequireGuid(guid, nameof(guid));
            return ExecuteAsync(async () =>
            {
                GDocRequest request = new GDocRequest(_connectionParam, TTNType.CollationStatementDiffs, rid, guid);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                return DataExecutable.Parse<GDoc8Diffs>(jsonAnswer);
            }, ErrorGetGDoc8Diffs);
        }

        public Task<GDoc10> GetGDoc10Async(uint rid, string guid) =>
            GetGDoc10Async(rid, guid, CancellationToken.None);

        public Task<GDoc10> GetGDoc10Async(uint rid, string guid, CancellationToken cancellationToken)
        {
            RequireGuid(guid, nameof(guid));
            return ExecuteAsync(async () =>
            {
                GDocRequest request = new GDocRequest(_connectionParam, TTNType.ActProcessing, rid, guid);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                return DataExecutable.Parse<GDoc10>(jsonAnswer);
            }, ErrorGetGDoc10);
        }

        public Task<GDoc11> GetGDoc11Async(uint rid, string guid) =>
            GetGDoc11Async(rid, guid, CancellationToken.None);

        public Task<GDoc11> GetGDoc11Async(uint rid, string guid, CancellationToken cancellationToken)
        {
            RequireGuid(guid, nameof(guid));
            return ExecuteAsync(async () =>
            {
                GDocRequest request = new GDocRequest(_connectionParam, TTNType.InternalMovement, rid, guid);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                ExecOperation answer = OperationBase.Parse<ExecOperation>(jsonAnswer);
                return GDoc11.Parse(answer);
            }, ErrorGetGDoc11);
        }

        public Task<IEnumerable<GGroup>> LoadGGroupsAsync() =>
            LoadGGroupsAsync(CancellationToken.None);

        public Task<IEnumerable<GGroup>> LoadGGroupsAsync(CancellationToken cancellationToken)
        {
            return ExecuteAsync(async () =>
            {
                GGroupsRequest request = new GGroupsRequest(_connectionParam);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                var groups = DataExecutable.Parse<GGroups>(jsonAnswer);
                var groupsByRid = groups.Where(g => g.Rid.HasValue).ToDictionary(g => g.Rid.Value);

                foreach (GGroup group in groups)
                {
                    if (group?.Parent?.Rid is uint parentRid)
                    {
                        if (!groupsByRid.TryGetValue(parentRid, out GGroup parent))
                            throw new ApiClientException($"Не найдена родительская группа товаров с Rid={parentRid}.");
                        group.Parent = parent;
                    }
                    else
                    {
                        group.Parent = null;
                    }
                }

                return (IEnumerable<GGroup>)groups;
            }, ErrorLoadGGroups);
        }

        public Task<IEnumerable<GoodsItem>> LoadGoodsFromGGroupAsync(uint groupRid) =>
            LoadGoodsFromGGroupAsync(groupRid, CancellationToken.None);

        public Task<IEnumerable<GoodsItem>> LoadGoodsFromGGroupAsync(uint groupRid, CancellationToken cancellationToken)
        {
            return ExecuteAsync(async () =>
            {
                GoodsRequest request = new GoodsRequest(_connectionParam, groupRid);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                ExecOperation answer = OperationBase.Parse<ExecOperation>(jsonAnswer);
                return (IEnumerable<GoodsItem>)GoodsItem.ParseGoods(answer, cancellationToken);
            }, ErrorLoadGoodsFromGGroup);
        }

        public Task<IEnumerable<MeasureUnit>> GetGoodsMUnitsAsync(uint goodRid) =>
            GetGoodsMUnitsAsync(goodRid, CancellationToken.None);

        public Task<IEnumerable<MeasureUnit>> GetGoodsMUnitsAsync(uint goodRid, CancellationToken cancellationToken)
        {
            return ExecuteAsync(async () =>
            {
                GoodsMUnitsRequest request = new GoodsMUnitsRequest(_connectionParam, goodRid);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                return (IEnumerable<MeasureUnit>)DataExecutable.Parse<MeasureUnits>(jsonAnswer);
            }, ErrorGetGoodsMUnits);
        }

        public Task<IEnumerable<GoodsItem>> LoadGoodsTreeAsync() =>
            LoadGoodsTreeAsync(CancellationToken.None);

        public Task<IEnumerable<GoodsItem>> LoadGoodsTreeAsync(CancellationToken cancellationToken)
        {
            return ExecuteAsync(async () =>
            {
                GoodsTreeRequest request = new GoodsTreeRequest(_connectionParam);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                ExecOperation answer = OperationBase.Parse<ExecOperation>(jsonAnswer);
                return (IEnumerable<GoodsItem>)GoodsItem.ParseGoods(answer, cancellationToken);
            }, ErrorLoadGoodsTree);
        }

        public Task<GoodsItem> CreateGoodAsync(string name, IEnumerable<MeasureUnit> measureUnits) =>
            CreateGoodAsync(name, measureUnits, CancellationToken.None);

        public Task<GoodsItem> CreateGoodAsync(string name, IEnumerable<MeasureUnit> measureUnits, CancellationToken cancellationToken)
        {
            RequireNotNullOrWhiteSpace(name, nameof(name));
            if (measureUnits == null)
                throw new ArgumentNullException(nameof(measureUnits));
            return ExecuteAsync(async () =>
            {
                InsGoodRequest request = new InsGoodRequest(_connectionParam, name, measureUnits);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                ExecOperation answer = OperationBase.Parse<ExecOperation>(jsonAnswer);
                return GoodsItem.Parse(GetRequiredFirstRow(answer, "210"));
            }, ErrorCreateGood);
        }

        public Task<MeasureUnit> CreateMeasureUnitAsync(string name, decimal ration, uint groupRid) =>
            CreateMeasureUnitAsync(name, ration, groupRid, CancellationToken.None);

        public Task<MeasureUnit> CreateMeasureUnitAsync(string name, decimal ration, uint groupRid, CancellationToken cancellationToken)
        {
            RequireNotNullOrWhiteSpace(name, nameof(name));
            return ExecuteAsync(async () =>
            {
                InsMUnitRequest request = new InsMUnitRequest(_connectionParam, name, ration, groupRid);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                ExecOperation answer = OperationBase.Parse<ExecOperation>(jsonAnswer);
                return MeasureUnit.Parse(GetRequiredFirstRow(answer, "206"));
            }, ErrorCreateMeasureUnit);
        }

        public Task<GoodsItem> GetGoodsItemAsync(uint goodsItemRid) =>
            GetGoodsItemAsync(goodsItemRid, CancellationToken.None);

        public Task<GoodsItem> GetGoodsItemAsync(uint goodsItemRid, CancellationToken cancellationToken)
        {
            return ExecuteAsync(async () =>
            {
                GoodsItemRequest request = new GoodsItemRequest(_connectionParam, goodsItemRid);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                ExecOperation answer = OperationBase.Parse<ExecOperation>(jsonAnswer);
                var units = MeasureUnit.ParseUnits(GetRequiredContent(answer, "211#1").GetValues());
                var item = GoodsItem.Parse(GetRequiredFirstRow(answer, "210"));
                item.MeasureUnits = units;
                return item;
            }, ErrorGetGoodsItem);
        }

        private async Task<string> CreateIncomingInvoiceAsync(string rid, DateTime timeStamp, CancellationToken cancellationToken)
        {
            InsIDoc0Request request = new InsIDoc0Request(_connectionParam, rid, timeStamp);
            string jsonAnswer = await PostAsync(request, cancellationToken);
            ExecOperation answer = OperationBase.Parse<ExecOperation>(jsonAnswer);
            Dictionary<string, string> row = GetRequiredFirstRow(answer, "111");
            return GetRequiredField(row, "3", "111");
        }

        /// <inheritdoc/>
        public Task<string> CreateIncomingTTNAsync(string name, DateTime timeStamp, string number, uint supplierRid, uint consigneeRid, string comment, bool createInvoice, IEnumerable<GDoc0Item> items) =>
            CreateIncomingTTNAsync(name, timeStamp, number, supplierRid, consigneeRid, comment, createInvoice, items, CancellationToken.None);

        /// <inheritdoc/>
        public Task<string> CreateIncomingTTNAsync(string name, DateTime timeStamp, string number, uint supplierRid, uint consigneeRid, string comment, bool createInvoice, IEnumerable<GDoc0Item> items, CancellationToken cancellationToken)
        {
            RequireNotNullOrWhiteSpace(name, nameof(name));
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            return ExecuteAsync(async () =>
            {
                InsGDoc0Request request = new InsGDoc0Request(_connectionParam, name, timeStamp, number, supplierRid, consigneeRid, comment, items);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                ExecOperation answer = OperationBase.Parse<ExecOperation>(jsonAnswer);
                Dictionary<string, string> row = GetRequiredFirstRow(answer, "111");
                string newName = GetRequiredField(row, "3", "111");
                string newRid = GetRequiredField(row, "1", "111");
                if (createInvoice)
                {
                    try
                    {
                        await CreateIncomingInvoiceAsync(newRid, timeStamp, cancellationToken);
                    }
                    catch (OperationCanceledException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        throw new ApiClientException($"Ошибка создания счёта после успешного создания ТТН (Rid={newRid}, Name={newName}). ТТН уже создана на сервере.", ex);
                    }
                }
                return newName;
            }, ErrorCreateIncomingTTN);
        }

        public Task<IEnumerable<NDSInfo>> GetNdsListAsync() =>
            GetNdsListAsync(CancellationToken.None);

        public Task<IEnumerable<NDSInfo>> GetNdsListAsync(CancellationToken cancellationToken)
        {
            return ExecuteAsync(async () =>
            {
                Taxes1Request request = new Taxes1Request(_connectionParam);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                ExecOperation answer = OperationBase.Parse<ExecOperation>(jsonAnswer);
                ExecOperationContent content = GetRequiredContent(answer, "212");
                if (content.Values == null || content.Values.Length == 0 || content.Values[0] == null)
                    throw new ApiClientException("Ответ сервера не содержит значений ставок НДС в блоке данных \"212\".");
                return content.Values[0].Select(t => new NDSInfo() { Rate = Convert.ToUInt32(t) });
            }, ErrorGetNdsList);
        }

        public Task<IEnumerable<GTD>> CreateGtdAsync(params string[] gtdNumbers) =>
            CreateGtdAsync(gtdNumbers, CancellationToken.None);

        public Task<IEnumerable<GTD>> CreateGtdAsync(string[] gtdNumbers, CancellationToken cancellationToken)
        {
            if (gtdNumbers == null)
                throw new ArgumentNullException(nameof(gtdNumbers));
            return ExecuteAsync(async () =>
            {
                ModCDeclsRequest request = new ModCDeclsRequest(_connectionParam, gtdNumbers);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                ExecOperation answer = OperationBase.Parse<ExecOperation>(jsonAnswer);
                return GTD.ParseRange(GetRequiredContent(answer, "116").GetValues());
            }, ErrorCreateGtd);
        }

        ///<inheritdoc />
        public Task<DocsByCorrsReport> GetDocsByCorrsReportAsync(DateTime from, DateTime to, InternalСorrespondent correspondent, CancellationToken cancellationToken)
        {
            if (correspondent == null)
                throw new ArgumentNullException(nameof(correspondent));
            if (!correspondent.Rid.HasValue)
                throw new ArgumentException("У корреспондента должен быть задан Rid.", nameof(correspondent));
            return GetDocsByCorrsReportAsync(from, to, correspondent.Rid.Value, cancellationToken);
        }

        ///<inheritdoc />
        public Task<DocsByCorrsReport> GetDocsByCorrsReportAsync(DateTime from, DateTime to, uint correspondentRid, CancellationToken cancellationToken)
        {
            return ExecuteAsync(async () =>
            {
                DocsByCorrsRequest request = new DocsByCorrsRequest(_connectionParam, from, to, correspondentRid);
                string jsonAnswer = await PostAsync(request, cancellationToken);
                ExecOperation answer = OperationBase.Parse<ExecOperation>(jsonAnswer);
                return DocsByCorrsReport.Parse(GetRequiredContent(answer, "107").GetValues());
            }, ErrorDocsByCorrsReport);
        }

        ///<inheritdoc />
        public Task<GDocsExReport> GetGDocsExReportAsync(DateTime? from, DateTime? to, TTNTypeForRequest ttnType, GDocsRequestFilter filter, IEnumerable<Depart> departs, CancellationToken cancellationToken)
        {
            if (departs == null)
                throw new ArgumentNullException(nameof(departs));
            return ExecuteAsync(async () =>
            {
                GDocsExRequest request = new GDocsExRequest(from, to, _connectionParam, ttnType, filter, departs.ToArray());
                string jsonAnswer = await PostAsync(request, cancellationToken);
                ExecOperation answer = OperationBase.Parse<ExecOperation>(jsonAnswer);
                var report = GDocsExReport.Parse(answer);
                var headersDict = new Dictionary<uint, GDocHeader>();
                foreach (GDocHeader header in report.Headers)
                {
                    if (!header.Rid.HasValue)
                        throw new ApiClientException("В отчёте найден заголовок накладной без Rid.");
                    if (headersDict.ContainsKey(header.Rid.Value))
                        throw new ApiClientException($"В отчёте найден дубликат заголовка накладной с Rid={header.Rid.Value}.");
                    headersDict.Add(header.Rid.Value, header);
                }
                foreach (var item in report.Content)
                {
                    uint? invoiceRid = item.Invoice?.Rid;
                    if (!invoiceRid.HasValue || !headersDict.TryGetValue(invoiceRid.Value, out GDocHeader invoice))
                        throw new ApiClientException($"Не найден заголовок накладной с Rid={invoiceRid} для строки отчёта.");
                    item.Invoice = invoice;
                }
                return report;
            }, ErrorGDocsExReport);
        }
    }
}
