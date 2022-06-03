namespace SH5ApiClient
{
    public interface IApiClient
    {
        Task<SHInfoAnswear> GetSHServerInfoAsync();

        /// <summary>Создание корреспондента
        /// <para>По умолчанию внешний контрагент</para></summary>
        /// <param name="name">Наименование</param>
        /// <param name="inn">ИНН</param>
        /// <param name="bankAccount">Расчетный счет</param>
        /// <param name="bik">БИК</param>
        /// <param name="bankName">Наименование банка</param>
        /// <param name="corAccount">Корр. счет</param>
        /// <param name="corrType">Тип корреспондента SH</param>
        Task<СorrespondentSH> CreateNewCorrespondentAsync(string name, string inn, string? bankAccount, string? bik, string? bankName, string? corAccount, CorrType corrType, CorrTypeEx corrTypeEx);

        /// <summary>Загрузка справочника расчетных счетов</summary>
        /// <param name="connectionParamSH5">Параметры подключения к SH</param>
        /// <returns>Словарь Расчетный счет - индекс</returns>
        Task<Dictionary<string, int>> LoadBankAccountsAsync();

        /// <summary>Загрузка справочника корреспондентов</summary>
        Task<IEnumerable<СorrespondentSH>> LoadCorrespondentsAsync();

        /// <summary>Загрузка справочника внутренних корреспондентов</summary>
        Task<IEnumerable<InternalСorrespondentSH>> LoadInternalСorrespondentsAsync();

        /// <summary>Обновление банковских реквизитов у корреспондента</summary>
        /// <param name="guid">Guid обновляемого корреспондента</param>
        /// <param name="bankName">Наименование банка</param>
        /// <param name="bankAccount">Расчетный счет</param>
        /// <param name="bik">БИК</param>
        /// <param name="corAccount">Кор. счет</param>
        Task UpdateCorrespondentAsync(string guid, string? bankName, string? bankAccount, string? bik, string? corAccount);
        /// <summary>Запросить наличие прав на выполнение процедуры</summary>
        /// <param name="procedureNames">Имена процедур для проверки</param>
        Task<SHAbleAnswear> RequestPermissionExecuteProcedure(IEnumerable<string> procedureNames);
    }
}
