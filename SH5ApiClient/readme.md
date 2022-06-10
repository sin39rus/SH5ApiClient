# SH5 Api Client v1.1

## Назначение

Библиотерка для подключение к серверу [StoreHouse 5 Web API 2](https://docs.rkeeper.ru/sh5/api) v1.12

## Инструкция

Для обмена данными с сервером SH5 API используется интерфейс IApiClient. Для инициализации интерфейса используется класс ApiClient, который в качестве обязательно параметра принимает параметры подключения к серверу.

## Описание методов интерфейса IApiClient

- GetSHServerInfo - Получение настроек сервера и информации о БД.
- LoadGDocs - Получение списка накладных.
- RequestGDoc0 - Получение приходной накладной.
- LoadCorrespondents - Запрос справочника корреспондентов.
- LoadInternalCorrespondents - Запрос справочника внутренних корреспондентов.

## Пример использования

```sh
// Запрос справочника корреспондентов.
    ConnectionParamSH5 param = new("Admin", "", "127.0.0.1", 9797);
    IApiClient client = new ApiClient(param);
    var correspondents = client.LoadCorrespondentsAsync().Result;
```
