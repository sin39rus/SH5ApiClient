# SH5 Api Client

## Назначение

Библиотека для подключения к серверу [StoreHouse 5 Web API 2](https://docs.rkeeper.ru/sh5/api) v1.12.

## Инструкция

Для обмена данными с сервером SH5 API используется интерфейс `IApiClient`. Для инициализации используется класс `ApiClient`, который в качестве обязательного параметра принимает параметры подключения к серверу (`ConnectionParamSH5`).

Большинство методов имеют перегрузки с `CancellationToken` для отмены операции.

## Описание методов интерфейса IApiClient

### Сервер и права

- `GetSHServerInfoAsync` — получение настроек сервера и информации о БД.
- `GetPermissionExecuteProcedureAsync` — проверка прав на выполнение процедур.
- `LoadEnumeratedAttributeValuesAsync` — значения перечислимого атрибута.

### Корреспонденты

- `LoadCorrespondentsAsync` — справочник корреспондентов.
- `LoadInternalCorrespondentsAsync` — справочник внутренних корреспондентов.
- `CreateNewCorrespondentAsync` — создание корреспондента.
- `UpdateCorrespondentAsync` — обновление банковских реквизитов корреспондента.

### Подразделения

- `LoadDepartsAsync` — список подразделений.
- `GetDepartAsync` — информация о подразделении.

### Товары и группы

- `LoadGGroupsAsync` — список товарных групп.
- `LoadGoodsFromGGroupAsync` — список товаров в группе.
- `LoadGoodsTreeAsync` — полный список товаров.
- `CreateGoodAsync` — создание товара.

### Валюты и налоги

- `LoadCurrenciesAsync` — список валют.
- `GetNdsListAsync` — список ставок НДС.
- `CreateGtdAsync` — создание ГТД по номерам.

### Единицы измерения

- `LoadMeasureGroupsAsync` — список групп единиц измерения.
- `GetMeasureGroupAsync` — группа единиц измерения.
- `LoadMeasureUnitsAsync` — список единиц измерения в группе (или по всем группам).
- `GetGoodsMUnitsAsync` — единицы измерения товара.
- `CreateMeasureUnitAsync` — создание единицы измерения.

### Документы GDoc

- `LoadGDocsAsync` — список накладных (по умолчанию только активные).
- `GetGDoc0Async` — приходная накладная.
- `GetGDoc4Async` — расходная накладная.
- `UpdateGDoc4Async` — обновление расходной накладной.
- `GetGDoc5Async` — возврат поставщику.
- `GetGDoc8Async` — сличительная ведомость.
- `GetGDoc8DiffsAsync` — сличительная ведомость (излишки/недостачи).
- `GetGDoc10Async` — акт переработки.
- `GetGDoc11Async` — внутреннее перемещение.
- `CreateIncomingTTNAsync` — создание приходной накладной.

### Отчеты

- `GetDocsByCorrsReportAsync` — отчет «Баланс по корреспондентам».
- `GetGDocsExReportAsync` — отчет «Расширенный список накладных».

## Пример использования

```csharp
// Запрос справочника корреспондентов.
ConnectionParamSH5 param = new("Admin", "", "127.0.0.1", 9797);
IApiClient client = new ApiClient(param);
var correspondents = await client.LoadCorrespondentsAsync();
```

[![Telegram](https://img.shields.io/badge/Telegram-2CA5E0?style=for-the-badge&logo=telegram&logoColor=white)](https://t.me/sin39rus)
[![GitHub](https://img.shields.io/badge/GitHub-121011?style=for-the-badge&logo=github&logoColor=white)](https://github.com/sin39rus)
