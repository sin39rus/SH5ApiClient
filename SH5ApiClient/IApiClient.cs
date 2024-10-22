using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Models;
using SH5ApiClient.Models.DTO;
using SH5ApiClient.Models.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SH5ApiClient
{
    public interface IApiClient
    {
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

        /// <summary>Запросить значения перечислимого атрибута.</summary>
        /// <param name="head">идентификатор таблицы</param>
        /// <param name="path">имя поля атрибута перечислимого типа</param>
        /// <returns></returns>
        Task<Dictionary<int, string>> LoadEnumeratedAttributeValuesAsync(string head, string path);

        /// <summary>Загрузка справочника корреспондентов
        /// </summary>
        Task<IEnumerable<Сorrespondent>> LoadCorrespondentsAsync();

        /// <summary>Загрузка справочника внутренних корреспондентов
        /// </summary>
        Task<IEnumerable<InternalСorrespondent>> LoadInternalCorrespondentsAsync();

        /// <summary>Обновление банковских реквизитов у корреспондента</summary>
        /// <param name="guid">Guid обновляемого корреспондента</param>
        /// <param name="bankName">Наименование банка</param>
        /// <param name="bankAccount">Расчетный счет</param>
        /// <param name="bik">БИК</param>
        /// <param name="corAccount">Кор. счет</param>
        Task UpdateCorrespondentAsync(string guid, string bankName, string bankAccount, string bik, string corAccount);

        /// <summary>Запросить наличие прав на выполнение процедуры</summary>
        /// <param name="procedureNames">Имена процедур для проверки</param>
        Task<AbleOperation> GetPermissionExecuteProcedure(IEnumerable<string> procedureNames);

        /// <summary>Загрузка списка товарных групп</summary>
        /// <returns>Товарные группы</returns>
        Task<IEnumerable<GGroup>> LoadGGroupsAsync();

        /// <summary>Запрос списка товаров в группе</summary>
        /// <param name="groupRid">Rid товарной группы</param>
        /// <returns>Список товаров в группе</returns>
        Task<IEnumerable<GoodsItem>> LoadGoodsFromGGroupAsync(uint groupRid); //ToDo реализовать расчет себестоимости, для этого надо запросить подразделение и дату https://docs.rkeeper.ru/sh5/api/protsedury-servera/slovari/tovary/goods-spisok-tovarov-v-gruppe

        /// <summary>Запрос полного списка товаров</summary>
        Task<IEnumerable<GoodsItem>> LoadGoodsTreeAsync();

        /// <summary>Загрузка списка подразделений</summary>
        /// <returns>Список подразделений</returns>
        Task<IEnumerable<Depart>> LoadDepartsAsync();

        /// <summary>Загрузка информации о подразделении</summary>
        /// <param name="rid">RID подразделения</param>
        /// <param name="guid">GUID подразделения</param>
        /// <returns></returns>
        Task<Depart> GetDepartAsync(uint rid, string guid);

        /// <summary>Загрузка списка валют</summary>
        /// <returns>Список валют</returns>
        Task<IEnumerable<Currency>> LoadCurrenciesAsync();
        /// <summary>Создание товара</summary>
        /// <param name="name">Наименование товара</param>
        /// <param name="measureUnits">Список единиц измерения</param>
        /// <returns>Товар</returns>
        Task<GoodsItem> CreateGoodAsync(string name, IEnumerable<MeasureUnit> measureUnits);


        #region Единицы измерения
        /// <summary>Запрос списка групп единиц измерения</summary>
        /// <returns>Список групп единиц измерения</returns>
        Task<IEnumerable<MeasureGroup>> LoadMeasureGroupsAsync();

        /// <summary>Загрузить группу единицы измерения</summary>
        /// <param name="groupRid">RID группы ед.изм</param>
        /// <returns>Группа единиц измерения</returns>
        Task<MeasureGroup> GetMeasureGroupAsync(uint groupRid);

        /// <summary>Запрос списка единиц измерения в группе</summary>
        /// <param name="groupRid">RID группы единиц измерения, по умолчанию по всем группам</param>
        /// <returns>Список единиц измерения в группе</returns>
        Task<IEnumerable<MeasureUnit>> LoadMeasureUnitsAsync(uint? groupRid = null);

        /// <summary>Запрос списка единиц измерения товара</summary>
        /// <param name="goodRid">Идентификатор товара</param>
        /// <returns></returns>
        Task<IEnumerable<MeasureUnit>> GetGoodsMUnitsAsync(uint goodRid);

        /// <summary>Создать единицу измерения</summary>
        /// <param name="name">Наименование единицы измерения</param>
        /// <param name="ration">Коэффициент к базовой единицы измерения</param>
        /// <param name="groupRid">RID группы единиц измерения</param>
        /// <returns>Единица измерения</returns>
        Task<MeasureUnit> CreateMeasureUnitAsync(string name, decimal ration, uint groupRid);
        #endregion

        #region Работа с документами GDoc
        /// <summary>Запрос списка накладных, по умолчанию возвращает только активные накладные</summary>
        /// <param name="dateFrom">С даты включительно</param>
        /// <param name="dateTo">По дату включительно</param>
        /// <param name="ttnTypeForRequest">Типы запрашиваемых накладных</param>
        /// <param name="gDocsRequestFilter">Фильтр накладных</param>
        /// <returns></returns>
        Task<IEnumerable<GDocHeader>> LoadGDocsAsync(DateTime? dateFrom = null, DateTime? dateTo = null, TTNTypeForRequest? ttnTypeForRequest = null, GDocsRequestFilter? gDocsRequestFilter = GDocsRequestFilter.ShowActiveInvoices);

        /// <summary>Запросить приходную накладную</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GOID накладной</param>
        /// <returns>Приходная накладная</returns>
        Task<GDoc0> GetGDoc0Async(uint rid, string guid);

        /// <summary>Запросить расходную накладную</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <returns>Расходная накладная накладная</returns>
        Task<GDoc4> GetGDoc4Async(uint rid, string guid);

        /// <summary>Запросить возврат поставщику</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <returns>Возврат поставщику</returns>
        Task<GDoc5> GetGDoc5Async(uint rid, string guid);

        /// <summary>Запросить сличительную ведомость</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <returns>Сличительную ведомость</returns>
        Task<GDoc8> GetGDoc8Async(uint rid, string guid);

        /// <summary>Запросить сличительную ведомость излишки/недостачи</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <returns>Сличительную ведомость излишки/недостачи</returns>
        Task<GDoc8Diffs> GetGDoc8DiffsAsync(uint rid, string guid);

        /// <summary>Запросить акт переработки</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <returns>Акт переработки</returns>
        Task<GDoc10> GetGDoc10Async(uint rid, string guid);

        /// <summary>Запросить внутреннее перемещение</summary>
        /// <param name="rid">RID накладной</param>
        /// <param name="guid">GUID накладной</param>
        /// <returns>Внутреннее перемещение</returns>
        Task<GDoc11> GetGDoc11Async(uint rid, string guid);
        Task<GDoc4> UpdateGDoc4(GDoc4 doc);
        #endregion
    }
}
