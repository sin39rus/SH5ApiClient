using Newtonsoft.Json.Linq;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Extensions;
using SH5ApiClient.Models;
using SH5ApiClient.Models.DTO;
using SH5ApiClient.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SH5ApiClient.Core.Requests
{
    /// <summary>
    /// Запрос на создание платежного документы
    /// </summary>
    public class InsPDocRequest : RequestBase
    {
        /// <summary>
        /// Не определенный обязательный атрибут ToDo GDocOptions переделать
        /// </summary>
        [OriginalName("33")]
        public int Id { private set; get; } = 1;
        /// <summary>
        /// Не определенный обязательный атрибут
        /// </summary>
        [OriginalName("31")]
        public DateTime DocumentDate { private set; get; }
        /// <summary>
        /// Курс валюты (единица базовой)
        /// </summary>
        [OriginalName("34")]
        public int Indefinite1 { private set; get; } = 1;
        /// <summary>
        /// к единице валюты накладной)
        /// </summary>
        [OriginalName("35")]
        public int Indefinite2 { private set; get; } = 1;
        /// <summary>
        /// Вид платежа
        /// </summary>
        [OriginalName("118\\1")]
        public PaymentType PaymentType { private set; get; }
        /// <summary>
        /// Валюта
        /// </summary>
        [OriginalName("100\\1")]
        public Currency Currency { private set; get; }
        /// <summary>
        /// Внешний корреспондент
        /// </summary>
        [OriginalName("105\\1")]
        public СorrespondentOld Сorrespondent { private set; get; }
        /// <summary>
        /// Собственное юридическое лицо
        /// </summary>
        [OriginalName("102\\1")]
        public InternalСorrespondent InternalСorrespondent { private set; get; }
        /// <summary>
        /// Финансовый блок
        /// </summary>
        [OriginalName("6\\Payment_Place")]
        public int? Payment_Place { set; get; }
        /// <summary>
        /// Счет поступления денег
        /// </summary>
        [OriginalName("6\\BankAccount")]
        public string BankAccount { set; get; }
        /// <summary>
        /// Номер и дата платежа
        /// </summary>
        [OriginalName("6\\NumberAndDatePayment")]
        public string NumberAndDatePayment { set; get; }
        /// <summary>
        /// Номер и дата платежа
        /// </summary>
        [OriginalName("6\\Comment")]
        public string Comment { set; get; }
        /// <summary>
        /// Дата импортирования
        /// </summary>
        [OriginalName("6\\ImportDate")]
        public DateTime ImportDate { set; get; }
        /// <summary>
        /// Информация об оплате
        /// </summary>
        public List<PaymentInfo> Payments { set; get; } = new List<PaymentInfo>();
        public override OperationBase Operation => new ExecOperation();
        public InsPDocRequest(PGocType docType, ConnectionParamSH5 connectionParam, DateTime documentDate, PaymentType paymentType, Currency currency, СorrespondentOld correspondent, InternalСorrespondent internalСorrespondent) : base(connectionParam)
        {
            DocumentDate = documentDate;
            PaymentType = paymentType;
            Currency = currency ?? throw new ArgumentNullException(nameof(currency));
            Сorrespondent = correspondent ?? throw new ArgumentNullException(nameof(correspondent));
            InternalСorrespondent = internalСorrespondent ?? throw new ArgumentNullException(nameof(internalСorrespondent));
            ImportDate = DateTime.Now;

            switch (docType)
            {
                case PGocType.Incoming:
                    ProcName = "InsPDoc0";
                    break;
                case PGocType.Outgoing:
                    ProcName = "InsPDoc1";
                    break;
                case PGocType.Inside:
                    throw new NotImplementedException("Создание внутренних платежных документов не реализовано.");
                default:
                    throw new NotImplementedException($"Не известный тип документа \"{docType}\".");
            }
        }
        public override string CreateJsonRequest()
        {
            JArray input = new JArray();

            JObject obj119 = new JObject();
            JArray original119 = new JArray();
            JArray values119 = new JArray();

            original119.Add(this.GetOriginalNameAttributeFromProperty(nameof(Id)));
            values119.Add(new JArray(Id));
            original119.Add(this.GetOriginalNameAttributeFromProperty(nameof(DocumentDate)));
            values119.Add(new JArray(DocumentDate.ToString("yyyy-MM-dd")));
            original119.Add(this.GetOriginalNameAttributeFromProperty(nameof(Indefinite1)));
            values119.Add(new JArray(Indefinite1));
            original119.Add(this.GetOriginalNameAttributeFromProperty(nameof(Indefinite2)));
            values119.Add(new JArray(Indefinite2));
            original119.Add(this.GetOriginalNameAttributeFromProperty(nameof(PaymentType)));
            values119.Add(new JArray((int)PaymentType));
            original119.Add(this.GetOriginalNameAttributeFromProperty(nameof(Currency)));
            values119.Add(new JArray(Currency.Rid));
            original119.Add(this.GetOriginalNameAttributeFromProperty(nameof(Сorrespondent)));
            values119.Add(new JArray(Сorrespondent.Rid));
            original119.Add(this.GetOriginalNameAttributeFromProperty(nameof(InternalСorrespondent)));
            values119.Add(new JArray(InternalСorrespondent.Rid));
            original119.Add(this.GetOriginalNameAttributeFromProperty(nameof(ImportDate)));
            values119.Add(new JArray(ImportDate));
            if (Payment_Place != null)
            {
                original119.Add(this.GetOriginalNameAttributeFromProperty(nameof(Payment_Place)));
                values119.Add(new JArray(Payment_Place));
            }
            if (BankAccount != null)
            {
                original119.Add(this.GetOriginalNameAttributeFromProperty(nameof(BankAccount)));
                values119.Add(new JArray(BankAccount));
            }
            if (NumberAndDatePayment != null)
            {
                original119.Add(this.GetOriginalNameAttributeFromProperty(nameof(NumberAndDatePayment)));
                values119.Add(new JArray(NumberAndDatePayment));
            }
            if (Comment != null)
            {
                original119.Add(this.GetOriginalNameAttributeFromProperty(nameof(Comment)));
                values119.Add(new JArray(Comment));
            }

            obj119.Add(new JProperty("head", "119"));
            obj119.Add(new JProperty("original", original119));
            obj119.Add(new JProperty("values", new JArray(values119)));
            input.Add(obj119);

            if (Payments.Count > 0)
            {
                JObject obj194 = new JObject();
                JArray original194 = new JArray();
                JArray values194 = new JArray();

                original194.Add(Payments[0].GetOriginalNameAttributeFromProperty(nameof(PaymentInfo.VATRate)));
                values194.Add(new JArray(Payments.Select(t => t.VATRate)));
                original194.Add(Payments[0].GetOriginalNameAttributeFromProperty(nameof(PaymentInfo.AmountWithoutVAT)));
                values194.Add(new JArray(Payments.Select(t => t.AmountWithoutVAT)));
                original194.Add(Payments[0].GetOriginalNameAttributeFromProperty(nameof(PaymentInfo.VATVolume)));
                values194.Add(new JArray(Payments.Select(t => t.VATVolume)));
                original194.Add(Payments[0].GetOriginalNameAttributeFromProperty(nameof(PaymentInfo.NSP)));
                values194.Add(new JArray(Payments.Select(t => t.NSP)));


                obj194.Add(new JProperty("head", "194"));
                obj194.Add(new JProperty("original", original194));
                obj194.Add(new JProperty("values", new JArray(values194)));
                input.Add(obj194);
            }

            JObject main = new JObject(
                new JProperty("UserName", UserName),
                new JProperty("Password", Password),
                new JProperty("procName", ProcName),
                new JProperty("Input", input));
            return main.ToString();
        }
    }
}
