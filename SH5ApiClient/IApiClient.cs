using SH5ApiClient.Core.ServerOperations;
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
        /// <summary>Получение настроек сервера и информации о БД. </summary>
        /// <returns>Информация о сервере SH</returns>
        Task<InfoOperation> GetSHServerInfoAsync(CancellationToken cancellationToken);
        /// <summary>Получение настроек сервера и информации о БД. </summary>
        /// <returns>Информация о сервере SH</returns>
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
        /// <param name="corrTypeEx">Тип корреспондента SH</param>
        Task<Сorrespondent> CreateNewCorrespondentAsync(string name, string inn, string bankAccount, string bik, string bankName, string corAccount, CorrType corrType, CorrTypeEx corrTypeEx);
        Task<Сorrespondent> CreateNewCorrespondentAsync(string name, string inn, string bankAccount, string bik, string bankName, string corAccount, CorrType corrType, CorrTypeEx corrTypeEx, CancellationToken cancellationToken);

        /// <summary>Запросить значения перечислимого атрибута.</summary>
        /// <param name="head">идентификатор таблицы</param>
        /// <param name="path">имя поля атрибута перечислимого типа</param>
        /// <returns></returns>
        Task<Dictionary<int, string>> LoadEnumeratedAttributeValuesAsync(string head, string path);
        Task<Dictionary<int, string>> LoadEnumeratedAttributeValuesAsync(string head, string path, CancellationToken cancellationToken);

        /// <summary>Загрузка справочника корреспондентов
        /// </summary>
        Task<IEnumerable<Сorrespondent>> LoadCorrespondentsAsync();
        Task<IEnumerable<Сorrespondent>> LoadCorrespondentsAsync(CancellationToken cancellationToken);

        /// <summary>Загрузка справочника внутренних корреспондентов
        /// </summary>
        Task<IEnumerable<InternalСorrespondent>> LoadInternalCorrespondentsAsync();
        Task<IEnumerable<InternalСorrespondent>> LoadInternalCorrespondentsAsync(CancellationToken cancellationToken);

        /// <summary>Обновление банковских реквизитов у корреспондента</summary>
        /// <param name="guid">Guid обновляемого корреспондента</param>
        /// <param name="bankName">Наименование банка</param>
        /// <param name="bankAccount">Расчетный счет</param>
        /// <param name="bik">БИК</param>
        /// <param name="corAccount">Кор. счет</param>
        Task UpdateCorrespondentAsync(string guid, string bankName, string bankAccount, string bik, string corAccount);
        Task UpdateCorrespondentAsync(string guid, string bankName, string bankAccount, string bik, string corAccount, CancellationToken cancellationToken);

        /// <summary>Запросить наличие прав на выполнение процедуры</summary>
        /// <param name="procedureNames">Имена процедур для проверки</param>
        Task<AbleOperation> GetPermissionExecuteProcedure(IEnumerable<string> procedureNames);
        Task<AbleOperation> GetPermissionExecuteProcedure(IEnumerable<string> procedureNames, CancellationToken cancellationToken);

        /// <summary>Загрузка списка товарных групп</summary>
        /// <returns>Товарные группы</returns>
        Task<IEnumerable<GGroup>> LoadGGroupsAsync();
        Task<IEnumerable<GGroup>> LoadGGroupsAsync(CancellationToken cancellationToken);

        /// <summary>Запрос списка товаров в группе</summary>
        /// <param name="groupRid">Rid товарной группы</param>
        /// <returns>Список товаров в группе</returns>
        Task<IEnumerable<GoodsItem>> LoadGoodsFromGGroupAsync(uint groupRid); //ToDo реализовать расчет себестоимости, для этого надо запросить подразделение и дату https://docs.rkeeper.ru/sh5/api/protsedury-servera/slovari/tovary/goods-spisok-tovarov-v-gruppe
        Task<IEnumerable<GoodsItem>> LoadGoodsFromGGroupAsync(uint groupRid, CancellationToken cancellationToken); //ToDo реализовать расчет себестоимости, для этого надо запросить подразделение и дату https://docs.rkeeper.ru/sh5/api/protsedury-servera/slovari/tovary/goods-spisok-tovarov-v-gruppe

        /// <summary>Запрос полного списка товаров</summary>
        Task<IEnumerable<GoodsItem>> LoadGoodsTreeAsync();
        Task<IEnumerable<GoodsItem>> LoadGoodsTreeAsync(CancellationToken cancellationToken);

        /// <summary>Загрузка списка подразделений</summary>
        /// <returns>Список подразделений</returns>
        Task<IEnumerable<Depart>> LoadDepartsAsync();
        Task<IEnumerable<Depart>> LoadDepartsAsync(CancellationToken cancellationToken);

        /// <summary>Загрузка информации о подразделении</summary>
        /// <param name="rid">RID подразделения</param>
        /// <param name="guid">GUID подразделения</param>
        /// <returns></returns>
        Task<Depart> GetDepartAsync(uint rid, string guid);
        Task<Depart> GetDepartAsync(uint rid, string guid, CancellationToken cancellationToken);

        /// <summary>Загрузка списка валют</summary>
        /// <returns>Список валют</returns>
        Task<IEnumerable<Currency>> LoadCurrenciesAsync();
        Task<IEnumerable<Currency>> LoadCurrenciesAsync(CancellationToken cancellationToken);
        /// <summary>Создание товара</summary>
        /// <param name="name">Наименование товара</param>
        /// <param name="measureUnits">Список единиц измерения</param>
        /// <returns>Товар</returns>
        Task<GoodsItem> CreateGoodAsync(string name, IEnumerable<MeasureUnit> measureUnits);
        Task<GoodsItem> CreateGoodAsync(string name, IEnumerable<MeasureUnit> measureUnits, CancellationToken cancellationToken);


        #region Единицы измерения
        /// <summary>Запрос списка групп единиц измерения</summary>
        /// <returns>Список групп единиц измерения</returns>
        Task<IEnumerable<MeasureGroup>> LoadMeasureGroupsAsync();
        Task<IEnumerable<MeasureGroup>> LoadMeasureGroupsAsync(CancellationToken cancellationToken);

        /// <summary>Загрузить группу единицы измерения</summary>
        /// <param name="groupRid">RID группы ед.изм</param>
        /// <returns>Группа единиц измерения</returns>
        Task<MeasureGroup> GetMeasureGroupAsync(uint groupRid);
        Task<MeasureGroup> GetMeasureGroupAsync(uint groupRid, CancellationToken cancellationToken);

        /// <summary>Запрос списка единиц измерения в группе</summary>
        /// <param name="groupRid">RID группы единиц измерения, по умолчанию по всем группам</param>
        /// <returns>Список единиц измерения в группе</returns>
        Task<IEnumerable<MeasureUnit>> LoadMeasureUnitsAsync(uint? groupRid = null);
        Task<IEnumerable<MeasureUnit>> LoadMeasureUnitsAsync(CancellationToken cancellationToken, uint? groupRid = null);

        /// <summary>Запрос списка единиц измерения товара</summary>
        /// <param name="goodRid">Идентификатор товара</param>
        /// <returns></returns>
        Task<IEnumerable<MeasureUnit>> GetGoodsMUnitsAsync(uint goodRid);
        Task<IEnumerable<MeasureUnit>> GetGoodsMUnitsAsync(uint goodRid, CancellationToken cancellationToken);

        /// <summary>Создать единицу измерения</summary>
        /// <param name="name">Наименование единицы измерения</param>
        /// <param name="ration">Коэффициент к базовой единицы измерения</param>
        /// <param name="groupRid">RID группы единиц измерения</param>
        /// <returns>Единица измерения</returns>
        Task<MeasureUnit> CreateMeasureUnitAsync(string name, decimal ration, uint groupRid);
        Task<MeasureUnit> CreateMeasureUnitAsync(string name, decimal ration, uint groupRid, CancellationToken cancellationToken);
        #endregion

        #region Работа с документами GDoc
        /// <summary>Запрос списка накладных, по умолчанию возвращает только активные накладные</summary>
        /// <param name="dateFrom">С даты включительно</param>
        /// <param name="dateTo">По дату включительно</param>
        /// <param name="ttnTypeForRequest">Типы запрашиваемых накладных</param>
        /// <param name="gDocsRequestFilter">Фильтр накладных</param>
        /// <returns></returns>
        Task<IEnumerable<GDocHeader>> LoadGDocsAsync(DateTime? dateFrom = null, DateTime? dateTo = null, TTNTypeForRequest? ttnTypeForRequest = null, GDocsRequestFilter? gDocsRequestFilter = GDocsRequestFilter.ShowActiveInvoices);
        Task<IEnumerable<GDocHeader>> LoadGDocsAsync(CancellationToken cancellationToken, DateTime? dateFrom = null, DateTime? dateTo = null, TTNTypeForRequest? ttnTypeForRequest = null, GDocsRequestFilter? gDocsRequestFilter = GDocsRequestFilter.ShowActiveInvoices);

        /// <summary>Запросить приходную накладную</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GOID накладной</param>
        /// <returns>Приходная накладная</returns>
        Task<GDoc0> GetGDoc0Async(uint rid, string guid);
        Task<GDoc0> GetGDoc0Async(uint rid, string guid, CancellationToken cancellationToken);

        /// <summary>Запросить расходную накладную</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <returns>Расходная накладная накладная</returns>
        Task<GDoc4> GetGDoc4Async(uint rid, string guid);
        Task<GDoc4> GetGDoc4Async(uint rid, string guid, CancellationToken cancellationToken);

        /// <summary>Запросить возврат поставщику</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <returns>Возврат поставщику</returns>
        Task<GDoc5> GetGDoc5Async(uint rid, string guid);
        Task<GDoc5> GetGDoc5Async(uint rid, string guid, CancellationToken cancellationToken);

        /// <summary>Запросить сличительную ведомость</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <returns>Сличительную ведомость</returns>
        Task<GDoc8> GetGDoc8Async(uint rid, string guid);
        Task<GDoc8> GetGDoc8Async(uint rid, string guid, CancellationToken cancellationToken);

        /// <summary>Запросить сличительную ведомость излишки/недостачи</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <returns>Сличительную ведомость излишки/недостачи</returns>
        Task<GDoc8Diffs> GetGDoc8DiffsAsync(uint rid, string guid);
        Task<GDoc8Diffs> GetGDoc8DiffsAsync(uint rid, string guid, CancellationToken cancellationToken);

        /// <summary>Запросить акт переработки</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <returns>Акт переработки</returns>
        Task<GDoc10> GetGDoc10Async(uint rid, string guid);
        Task<GDoc10> GetGDoc10Async(uint rid, string guid, CancellationToken cancellationToken);

        /// <summary>Запросить внутреннее перемещение</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <returns>Внутреннее перемещение</returns>
        Task<GDoc11> GetGDoc11Async(uint rid, string guid);
        Task<GDoc11> GetGDoc11Async(uint rid, string guid, CancellationToken cancellationToken);
        Task<GDoc4> UpdateGDoc4(GDoc4 doc);
        Task<GDoc4> UpdateGDoc4(GDoc4 doc, CancellationToken cancellationToken);
        #endregion

        /// <summary>
        /// Создание приходной накладной
        /// </summary>
        /// <param name="name">Номер накладной</param>
        /// <param name="timeStamp">Дата</param>
        /// <param name="number">Номер</param>
        /// <param name="supplierRid">RID Поставщика</param>
        /// <param name="consigneeRid">RID Склада</param>
        /// <param name="comment">Примечание</param>
        /// <param name="createInvoice">Создавать счет фактуру</param>
        /// <param name="items">Содержимое накладной</param>
        /// <returns>
        /// <para>value1 RID созданной накладной</para>
        /// <para>value2 Номер созданной накладной</para>
        /// </returns>
        Task<string> CreateIncomingTTNAsync(string name, DateTime timeStamp, string number, uint supplierRid, uint consigneeRid, string comment, bool createInvoice, IEnumerable<GDoc0Item> items);
        /// <summary>
        /// Создание приходной накладной
        /// </summary>
        /// <param name="name">Номер накладной</param>
        /// <param name="timeStamp">Дата</param>
        /// <param name="number">Номер</param>
        /// <param name="supplierRid">RID Поставщика</param>
        /// <param name="consigneeRid">RID Склада</param>
        /// <param name="comment">Примечание</param>
        /// <param name="createInvoice">Создавать счет фактуру</param>
        /// <param name="items">Содержимое накладной</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>
        /// <para>item1 RID созданной накладной</para>
        /// <para>item2 Номер созданной накладной</para>
        /// </returns>
        Task<string> CreateIncomingTTNAsync(string name, DateTime timeStamp, string number, uint supplierRid, uint consigneeRid, string comment, bool createInvoice, IEnumerable<GDoc0Item> items, CancellationToken cancellationToken);

        /// <summary>Запрос списка ставок НДС</summary>
        /// <returns>Ставки НДС</returns>
        Task<IEnumerable<NDSInfo>> GetNdsListAsync();
        Task<IEnumerable<NDSInfo>> GetNdsListAsync(CancellationToken cancellationToken);

        /// <summary>Баланс по корреспондентам</summary>
        /// <param name="from">С</param>
        /// <param name="to">По</param>
        /// <param name="correspondent">Собственное юридическое лицо</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DocsByCorrsReport> GetDocsByCorrsReportAsync(DateTime from, DateTime to, InternalСorrespondent correspondent, CancellationToken cancellationToken);

        /// <summary>Баланс по корреспондентам</summary>
        /// <param name="from">С</param>
        /// <param name="to">По</param>
        /// <param name="correspondentRid">Rid собственного юридического лица</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DocsByCorrsReport> GetDocsByCorrsReportAsync(DateTime from, DateTime to, uint correspondentRid, CancellationToken cancellationToken);
    }
}
