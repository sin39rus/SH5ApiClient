using SH5ApiClient.Data;
using SH5ApiClient.Models;
using SH5ApiClient.Models.DTO;
using SH5ApiClient.Models.DTO.Reports;
using SH5ApiClient.Models.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SH5ApiClient
{
    public interface IApiClient
    {
        /// <summary>Получение настроек сервера и информации о БД.</summary>
        /// <returns>Информация о сервере SH</returns>
        Task<ServerInfo> GetSHServerInfoAsync(CancellationToken cancellationToken);
        /// <summary>Получение настроек сервера и информации о БД.</summary>
        /// <returns>Информация о сервере SH</returns>
        Task<ServerInfo> GetSHServerInfoAsync();

        /// <summary>Создание корреспондента.
        /// <para>По умолчанию внешний контрагент</para></summary>
        /// <param name="name">Наименование</param>
        /// <param name="inn">ИНН</param>
        /// <param name="bankAccount">Расчетный счет</param>
        /// <param name="bik">БИК</param>
        /// <param name="bankName">Наименование банка</param>
        /// <param name="corAccount">Корр. счет</param>
        /// <param name="corrType">Тип корреспондента (реализация, потери, внутренний/внешний контрагент)</param>
        /// <param name="corrTypeEx">Расширенный тип корреспондента (юр. лицо, физ. лицо, спец. корреспондент)</param>
        /// <returns>Созданный корреспондент</returns>
        Task<Correspondent> CreateNewCorrespondentAsync(string name, string inn, string bankAccount, string bik, string bankName, string corAccount, CorrType corrType, CorrTypeEx corrTypeEx);
        /// <summary>Создание корреспондента.
        /// <para>По умолчанию внешний контрагент</para></summary>
        /// <param name="name">Наименование</param>
        /// <param name="inn">ИНН</param>
        /// <param name="bankAccount">Расчетный счет</param>
        /// <param name="bik">БИК</param>
        /// <param name="bankName">Наименование банка</param>
        /// <param name="corAccount">Корр. счет</param>
        /// <param name="corrType">Тип корреспондента (реализация, потери, внутренний/внешний контрагент)</param>
        /// <param name="corrTypeEx">Расширенный тип корреспондента (юр. лицо, физ. лицо, спец. корреспондент)</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Созданный корреспондент</returns>
        Task<Correspondent> CreateNewCorrespondentAsync(string name, string inn, string bankAccount, string bik, string bankName, string corAccount, CorrType corrType, CorrTypeEx corrTypeEx, CancellationToken cancellationToken);

        /// <summary>Запросить значения перечислимого атрибута.</summary>
        /// <param name="head">Идентификатор таблицы</param>
        /// <param name="path">Имя поля атрибута перечислимого типа</param>
        /// <returns>Словарь значений перечислимого атрибута (ключ — код, значение — наименование)</returns>
        Task<Dictionary<int, string>> LoadEnumeratedAttributeValuesAsync(string head, string path);
        /// <summary>Запросить значения перечислимого атрибута.</summary>
        /// <param name="head">Идентификатор таблицы</param>
        /// <param name="path">Имя поля атрибута перечислимого типа</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Словарь значений перечислимого атрибута (ключ — код, значение — наименование)</returns>
        Task<Dictionary<int, string>> LoadEnumeratedAttributeValuesAsync(string head, string path, CancellationToken cancellationToken);

        /// <summary>Загрузка справочника корреспондентов.</summary>
        /// <returns>Список корреспондентов</returns>
        Task<IEnumerable<Correspondent>> LoadCorrespondentsAsync();
        /// <summary>Загрузка справочника корреспондентов.</summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список корреспондентов</returns>
        Task<IEnumerable<Correspondent>> LoadCorrespondentsAsync(CancellationToken cancellationToken);

        /// <summary>Загрузка справочника внутренних корреспондентов.</summary>
        /// <returns>Список внутренних корреспондентов</returns>
        Task<IEnumerable<InternalCorrespondent>> LoadInternalCorrespondentsAsync();
        /// <summary>Загрузка справочника внутренних корреспондентов.</summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список внутренних корреспондентов</returns>
        Task<IEnumerable<InternalCorrespondent>> LoadInternalCorrespondentsAsync(CancellationToken cancellationToken);

        /// <summary>Обновление банковских реквизитов у корреспондента.</summary>
        /// <param name="guid">GUID обновляемого корреспондента</param>
        /// <param name="bankName">Наименование банка</param>
        /// <param name="bankAccount">Расчетный счет</param>
        /// <param name="bik">БИК</param>
        /// <param name="corAccount">Корр. счет</param>
        Task UpdateCorrespondentAsync(string guid, string bankName, string bankAccount, string bik, string corAccount);
        /// <summary>Обновление банковских реквизитов у корреспондента.</summary>
        /// <param name="guid">GUID обновляемого корреспондента</param>
        /// <param name="bankName">Наименование банка</param>
        /// <param name="bankAccount">Расчетный счет</param>
        /// <param name="bik">БИК</param>
        /// <param name="corAccount">Корр. счет</param>
        /// <param name="cancellationToken">Токен отмены</param>
        Task UpdateCorrespondentAsync(string guid, string bankName, string bankAccount, string bik, string corAccount, CancellationToken cancellationToken);

        /// <summary>Запросить наличие прав на выполнение процедуры.</summary>
        /// <param name="procedureNames">Имена процедур для проверки</param>
        /// <returns>Результат проверки прав</returns>
        Task<ProcedurePermissions> GetPermissionExecuteProcedureAsync(IEnumerable<string> procedureNames);
        /// <summary>Запросить наличие прав на выполнение процедуры.</summary>
        /// <param name="procedureNames">Имена процедур для проверки</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Результат проверки прав</returns>
        Task<ProcedurePermissions> GetPermissionExecuteProcedureAsync(IEnumerable<string> procedureNames, CancellationToken cancellationToken);

        /// <summary>Загрузка списка товарных групп.</summary>
        /// <returns>Товарные группы</returns>
        Task<IEnumerable<GGroup>> LoadGGroupsAsync();
        /// <summary>Загрузка списка товарных групп.</summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Товарные группы</returns>
        Task<IEnumerable<GGroup>> LoadGGroupsAsync(CancellationToken cancellationToken);

        /// <summary>Запрос списка товаров в группе.</summary>
        /// <param name="groupRid">RID товарной группы</param>
        /// <returns>Список товаров в группе</returns>
        Task<IEnumerable<GoodsItem>> LoadGoodsFromGGroupAsync(uint groupRid); //ToDo реализовать расчет себестоимости, для этого надо запросить подразделение и дату https://docs.rkeeper.ru/sh5/api/protsedury-servera/slovari/tovary/goods-spisok-tovarov-v-gruppe
        /// <summary>Запрос списка товаров в группе.</summary>
        /// <param name="groupRid">RID товарной группы</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список товаров в группе</returns>
        Task<IEnumerable<GoodsItem>> LoadGoodsFromGGroupAsync(uint groupRid, CancellationToken cancellationToken); //ToDo реализовать расчет себестоимости, для этого надо запросить подразделение и дату https://docs.rkeeper.ru/sh5/api/protsedury-servera/slovari/tovary/goods-spisok-tovarov-v-gruppe

        /// <summary>Запрос полного списка товаров.</summary>
        /// <returns>Полный список товаров</returns>
        Task<IEnumerable<GoodsItem>> LoadGoodsTreeAsync();
        /// <summary>Запрос полного списка товаров.</summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Полный список товаров</returns>
        Task<IEnumerable<GoodsItem>> LoadGoodsTreeAsync(CancellationToken cancellationToken);

        /// <summary>Загрузка списка подразделений.</summary>
        /// <returns>Список подразделений</returns>
        Task<IEnumerable<Depart>> LoadDepartsAsync();
        /// <summary>Загрузка списка подразделений.</summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список подразделений</returns>
        Task<IEnumerable<Depart>> LoadDepartsAsync(CancellationToken cancellationToken);

        /// <summary>Загрузка информации о подразделении.</summary>
        /// <param name="rid">RID подразделения</param>
        /// <param name="guid">GUID подразделения</param>
        /// <returns>Подразделение</returns>
        Task<Depart> GetDepartAsync(uint rid, string guid);
        /// <summary>Загрузка информации о подразделении.</summary>
        /// <param name="rid">RID подразделения</param>
        /// <param name="guid">GUID подразделения</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Подразделение</returns>
        Task<Depart> GetDepartAsync(uint rid, string guid, CancellationToken cancellationToken);

        /// <summary>Загрузка списка валют.</summary>
        /// <returns>Список валют</returns>
        Task<IEnumerable<Currency>> LoadCurrenciesAsync();
        /// <summary>Загрузка списка валют.</summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список валют</returns>
        Task<IEnumerable<Currency>> LoadCurrenciesAsync(CancellationToken cancellationToken);

        /// <summary>Создание товара.</summary>
        /// <param name="name">Наименование товара</param>
        /// <param name="measureUnits">Список единиц измерения</param>
        /// <returns>Созданный товар</returns>
        Task<GoodsItem> CreateGoodAsync(string name, IEnumerable<MeasureUnit> measureUnits);
        /// <summary>Создание товара.</summary>
        /// <param name="name">Наименование товара</param>
        /// <param name="measureUnits">Список единиц измерения</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Созданный товар</returns>
        Task<GoodsItem> CreateGoodAsync(string name, IEnumerable<MeasureUnit> measureUnits, CancellationToken cancellationToken);


        #region Единицы измерения
        /// <summary>Запрос списка групп единиц измерения.</summary>
        /// <returns>Список групп единиц измерения</returns>
        Task<IEnumerable<MeasureGroup>> LoadMeasureGroupsAsync();
        /// <summary>Запрос списка групп единиц измерения.</summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список групп единиц измерения</returns>
        Task<IEnumerable<MeasureGroup>> LoadMeasureGroupsAsync(CancellationToken cancellationToken);

        /// <summary>Загрузить группу единиц измерения.</summary>
        /// <param name="groupRid">RID группы ед.изм.</param>
        /// <returns>Группа единиц измерения</returns>
        Task<MeasureGroup> GetMeasureGroupAsync(uint groupRid);
        /// <summary>Загрузить группу единиц измерения.</summary>
        /// <param name="groupRid">RID группы ед.изм.</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Группа единиц измерения</returns>
        Task<MeasureGroup> GetMeasureGroupAsync(uint groupRid, CancellationToken cancellationToken);

        /// <summary>Запрос списка единиц измерения в группе.</summary>
        /// <param name="groupRid">RID группы единиц измерения; если null — по всем группам</param>
        /// <returns>Список единиц измерения в группе</returns>
        Task<IEnumerable<MeasureUnit>> LoadMeasureUnitsAsync(uint? groupRid = null);
        /// <summary>Запрос списка единиц измерения в группе.</summary>
        /// <param name="groupRid">RID группы единиц измерения; если null — по всем группам</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список единиц измерения в группе</returns>
        Task<IEnumerable<MeasureUnit>> LoadMeasureUnitsAsync(uint? groupRid, CancellationToken cancellationToken);

        /// <summary>Запрос списка единиц измерения товара.</summary>
        /// <param name="goodRid">Идентификатор товара</param>
        /// <returns>Список единиц измерения товара</returns>
        Task<IEnumerable<MeasureUnit>> GetGoodsMUnitsAsync(uint goodRid);
        /// <summary>Запрос списка единиц измерения товара.</summary>
        /// <param name="goodRid">Идентификатор товара</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список единиц измерения товара</returns>
        Task<IEnumerable<MeasureUnit>> GetGoodsMUnitsAsync(uint goodRid, CancellationToken cancellationToken);

        /// <summary>Создать единицу измерения.</summary>
        /// <param name="name">Наименование единицы измерения</param>
        /// <param name="ratio">Коэффициент к базовой единице измерения</param>
        /// <param name="groupRid">RID группы единиц измерения</param>
        /// <returns>Созданная единица измерения</returns>
        Task<MeasureUnit> CreateMeasureUnitAsync(string name, decimal ratio, uint groupRid);
        /// <summary>Создать единицу измерения.</summary>
        /// <param name="name">Наименование единицы измерения</param>
        /// <param name="ratio">Коэффициент к базовой единице измерения</param>
        /// <param name="groupRid">RID группы единиц измерения</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Созданная единица измерения</returns>
        Task<MeasureUnit> CreateMeasureUnitAsync(string name, decimal ratio, uint groupRid, CancellationToken cancellationToken);
        #endregion

        #region Работа с документами GDoc
        /// <summary>Запрос списка накладных; по умолчанию возвращает только активные накладные.</summary>
        /// <param name="dateFrom">С даты включительно</param>
        /// <param name="dateTo">По дату включительно</param>
        /// <param name="ttnTypeForRequest">Типы запрашиваемых накладных</param>
        /// <param name="gDocsRequestFilter">Фильтр накладных</param>
        /// <returns>Список заголовков накладных</returns>
        Task<IEnumerable<GDocHeader>> LoadGDocsAsync(DateTime? dateFrom = null, DateTime? dateTo = null, TTNTypeForRequest? ttnTypeForRequest = null, GDocsRequestFilter? gDocsRequestFilter = GDocsRequestFilter.ShowActiveInvoices);
        /// <summary>Запрос списка накладных; по умолчанию возвращает только активные накладные.</summary>
        /// <param name="dateFrom">С даты включительно</param>
        /// <param name="dateTo">По дату включительно</param>
        /// <param name="ttnTypeForRequest">Типы запрашиваемых накладных</param>
        /// <param name="gDocsRequestFilter">Фильтр накладных</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список заголовков накладных</returns>
        Task<IEnumerable<GDocHeader>> LoadGDocsAsync(DateTime? dateFrom, DateTime? dateTo, TTNTypeForRequest? ttnTypeForRequest, GDocsRequestFilter? gDocsRequestFilter, CancellationToken cancellationToken);

        /// <summary>Запрос списка накладных в сыром виде (DataSet); по умолчанию возвращает только активные накладные.</summary>
        /// <param name="dateFrom">С даты включительно</param>
        /// <param name="dateTo">По дату включительно</param>
        /// <param name="ttnTypeForRequest">Типы запрашиваемых накладных</param>
        /// <param name="gDocsRequestFilter">Фильтр накладных</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Сырой набор данных ответа сервера</returns>
        Task<DataSet> LoadGDocsRawAsync(DateTime? dateFrom, DateTime? dateTo, TTNTypeForRequest? ttnTypeForRequest, GDocsRequestFilter? gDocsRequestFilter, CancellationToken cancellationToken);

        /// <summary>Запросить приходную накладную.</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <returns>Приходная накладная</returns>
        Task<GDoc0> GetGDoc0Async(uint rid, string guid);
        /// <summary>Запросить приходную накладную.</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Приходная накладная</returns>
        Task<GDoc0> GetGDoc0Async(uint rid, string guid, CancellationToken cancellationToken);

        /// <summary>Запросить расходную накладную.</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <returns>Расходная накладная</returns>
        Task<GDoc4> GetGDoc4Async(uint rid, string guid);
        /// <summary>Запросить расходную накладную.</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Расходная накладная</returns>
        Task<GDoc4> GetGDoc4Async(uint rid, string guid, CancellationToken cancellationToken);

        /// <summary>Запросить возврат поставщику.</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <returns>Возврат поставщику</returns>
        Task<GDoc5> GetGDoc5Async(uint rid, string guid);
        /// <summary>Запросить возврат поставщику.</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Возврат поставщику</returns>
        Task<GDoc5> GetGDoc5Async(uint rid, string guid, CancellationToken cancellationToken);

        /// <summary>Запросить сличительную ведомость.</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <returns>Сличительная ведомость</returns>
        Task<GDoc8> GetGDoc8Async(uint rid, string guid);
        /// <summary>Запросить сличительную ведомость.</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Сличительная ведомость</returns>
        Task<GDoc8> GetGDoc8Async(uint rid, string guid, CancellationToken cancellationToken);

        /// <summary>Запросить сличительную ведомость (излишки/недостачи).</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <returns>Сличительная ведомость (излишки/недостачи)</returns>
        Task<GDoc8Diffs> GetGDoc8DiffsAsync(uint rid, string guid);
        /// <summary>Запросить сличительную ведомость (излишки/недостачи).</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Сличительная ведомость (излишки/недостачи)</returns>
        Task<GDoc8Diffs> GetGDoc8DiffsAsync(uint rid, string guid, CancellationToken cancellationToken);

        /// <summary>Запросить акт переработки.</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <returns>Акт переработки</returns>
        Task<GDoc10> GetGDoc10Async(uint rid, string guid);
        /// <summary>Запросить акт переработки.</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Акт переработки</returns>
        Task<GDoc10> GetGDoc10Async(uint rid, string guid, CancellationToken cancellationToken);

        /// <summary>Запросить внутреннее перемещение.</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <returns>Внутреннее перемещение</returns>
        Task<GDoc11> GetGDoc11Async(uint rid, string guid);
        /// <summary>Запросить внутреннее перемещение.</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Внутреннее перемещение</returns>
        Task<GDoc11> GetGDoc11Async(uint rid, string guid, CancellationToken cancellationToken);

        /// <summary>Обновление расходной накладной.</summary>
        /// <param name="doc">Расходная накладная</param>
        /// <returns>Обновленная расходная накладная</returns>
        Task<GDoc4> UpdateGDoc4Async(GDoc4 doc);
        /// <summary>Обновление расходной накладной.</summary>
        /// <param name="doc">Расходная накладная</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Обновленная расходная накладная</returns>
        Task<GDoc4> UpdateGDoc4Async(GDoc4 doc, CancellationToken cancellationToken);
        #endregion

        /// <summary>Создание приходной накладной.</summary>
        /// <param name="name">Наименование (номер) накладной</param>
        /// <param name="timeStamp">Дата накладной</param>
        /// <param name="number">Внешний номер ТТН</param>
        /// <param name="supplierRid">RID поставщика</param>
        /// <param name="consigneeRid">RID получателя (склада)</param>
        /// <param name="comment">Примечание</param>
        /// <param name="createInvoice">Создавать счет-фактуру после создания накладной</param>
        /// <param name="items">Содержимое накладной</param>
        /// <returns>Наименование (номер) созданной накладной</returns>
        Task<string> CreateIncomingTTNAsync(string name, DateTime timeStamp, string number, uint supplierRid, uint consigneeRid, string comment, bool createInvoice, IEnumerable<GDoc0Item> items);
        /// <summary>Создание приходной накладной.</summary>
        /// <param name="name">Наименование (номер) накладной</param>
        /// <param name="timeStamp">Дата накладной</param>
        /// <param name="number">Внешний номер ТТН</param>
        /// <param name="supplierRid">RID поставщика</param>
        /// <param name="consigneeRid">RID получателя (склада)</param>
        /// <param name="comment">Примечание</param>
        /// <param name="createInvoice">Создавать счет-фактуру после создания накладной</param>
        /// <param name="items">Содержимое накладной</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Наименование (номер) созданной накладной</returns>
        Task<string> CreateIncomingTTNAsync(string name, DateTime timeStamp, string number, uint supplierRid, uint consigneeRid, string comment, bool createInvoice, IEnumerable<GDoc0Item> items, CancellationToken cancellationToken);

        /// <summary>Запрос списка ставок НДС.</summary>
        /// <returns>Ставки НДС</returns>
        Task<IEnumerable<NDSInfo>> GetNdsListAsync();
        /// <summary>Запрос списка ставок НДС.</summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Ставки НДС</returns>
        Task<IEnumerable<NDSInfo>> GetNdsListAsync(CancellationToken cancellationToken);

        /// <summary>Создание ГТД по номерам.</summary>
        /// <param name="gtdNumbers">Номера ГТД</param>
        /// <returns>Созданные ГТД</returns>
        Task<IEnumerable<GTD>> CreateGtdAsync(params string[] gtdNumbers);
        /// <summary>Создание ГТД по номерам.</summary>
        /// <param name="gtdNumbers">Номера ГТД</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Созданные ГТД</returns>
        Task<IEnumerable<GTD>> CreateGtdAsync(string[] gtdNumbers, CancellationToken cancellationToken);

        /// <summary>Отчет «Баланс по корреспондентам».</summary>
        /// <param name="from">Дата начала периода</param>
        /// <param name="to">Дата окончания периода</param>
        /// <param name="correspondent">Собственное юридическое лицо</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Отчет «Баланс по корреспондентам»</returns>
        Task<DocsByCorrsReport> GetDocsByCorrsReportAsync(DateTime from, DateTime to, InternalCorrespondent correspondent, CancellationToken cancellationToken);

        /// <summary>Отчет «Баланс по корреспондентам».</summary>
        /// <param name="from">Дата начала периода</param>
        /// <param name="to">Дата окончания периода</param>
        /// <param name="correspondentRid">RID собственного юридического лица</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Отчет «Баланс по корреспондентам»</returns>
        Task<DocsByCorrsReport> GetDocsByCorrsReportAsync(DateTime from, DateTime to, uint correspondentRid, CancellationToken cancellationToken);

        /// <summary>Отчет «Расширенный список накладных».</summary>
        /// <param name="from">Дата начала периода; если null — без ограничения по началу</param>
        /// <param name="to">Дата окончания периода; если null — без ограничения по концу</param>
        /// <param name="ttnType">Тип накладных</param>
        /// <param name="filter">Фильтр накладных</param>
        /// <param name="departs">Фильтр по подразделениям</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Отчет «Расширенный список накладных»</returns>
        Task<GDocsExReport> GetGDocsExReportAsync(DateTime? from, DateTime? to, TTNTypeForRequest ttnType, GDocsRequestFilter filter, IEnumerable<Depart> departs, CancellationToken cancellationToken);
    }
}
