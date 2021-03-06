using SH5ApiClient.Models.DTO;

namespace SH5ApiClient
{
    public interface IApiClient
    {
        Task<InfoOperation> GetSHServerInfoAsync();

        /// <summary>Создание корреспондента
        /// <para>По умолчанию внешний контрагент</para></summary>
        /// <param name="name">Наименование</param>
        /// <param name="inn">ИНН</param>
        /// <param name="bankAccount">Расчетный счет</param>
        /// <param name="bik">БИК</param>
        /// <param name="bankName">Наименование банка</param>
        /// <param name="corAccount">Корр. счет</param>
        /// <param name="corrType">Тип корреспондента SH</param>
        Task<Сorrespondent?> CreateNewCorrespondentAsync(string name, string inn, string? bankAccount, string? bik, string? bankName, string? corAccount, CorrType corrType, CorrTypeEx corrTypeEx);

        /// <summary>Запросить значения перечислимого атрибута.</summary>
        /// <param name="head">идентификатор таблицы</param>
        /// <param name="path">имя поля атрибута перечислимого типа</param>
        /// <returns></returns>
        Task<Dictionary<int, string>> LoadEnumeratedAttributeValuesAsync(string head, string path);

        /// <summary>Загрузка справочника корреспондентов</summary>
        Task<IEnumerable<Сorrespondent?>> LoadCorrespondentsAsync();

        /// <summary>Загрузка справочника внутренних корреспондентов</summary>
        Task<IEnumerable<Сorrespondent?>> LoadInternalCorrespondentsAsync();

        /// <summary>Обновление банковских реквизитов у корреспондента</summary>
        /// <param name="guid">Guid обновляемого корреспондента</param>
        /// <param name="bankName">Наименование банка</param>
        /// <param name="bankAccount">Расчетный счет</param>
        /// <param name="bik">БИК</param>
        /// <param name="corAccount">Кор. счет</param>
        Task UpdateCorrespondentAsync(string guid, string? bankName, string? bankAccount, string? bik, string? corAccount);
        /// <summary>Запросить наличие прав на выполнение процедуры</summary>
        /// <param name="procedureNames">Имена процедур для проверки</param>
        Task<AbleOperation> GetPermissionExecuteProcedure(IEnumerable<string> procedureNames);
        /// <summary>Запрос списка накладных, по умолчанию возвращает только активные накладные</summary>
        /// <param name="dateFrom">С даты включительно</param>
        /// <param name="dateTo">По дату включительно</param>
        /// <param name="ttnTypeForRequest">Типы запрашиваемых накладных</param>
        /// <param name="gDocsRequestFilter">Фильтр накладных</param>
        /// <returns></returns>
        Task<IEnumerable<GDocHeader>> LoadGDocsAsync(DateTime? dateFrom = null, DateTime? dateTo = null, TTNTypeForRequest? ttnTypeForRequest = null, GDocsRequestFilter? gDocsRequestFilter = GDocsRequestFilter.ShowActiveInvoices);

        /// <summary>Загрузка списка товарных групп</summary>
        /// <returns>Товарные группы</returns>
        Task<IEnumerable<GGroup>> LoadGGroupsAsync();

        /// <summary>Запросить приходную накладную</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GOID накладной</param>
        /// <returns>Приходная накладная</returns>
        Task<GDoc0?> GetGDoc0Async(uint rid, string guid);

        /// <summary>Запросить расходную накладную</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <returns>Расходная накладная накладная</returns>
        Task<GDoc4?> GetGDoc4Async(uint rid, string guid);

        /// <summary>Запросить внутреннее перемещение</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <returns>Внутреннее перемещение</returns>
        Task<GDoc11?> GetGDoc11Async(uint rid, string guid);
    }
}
