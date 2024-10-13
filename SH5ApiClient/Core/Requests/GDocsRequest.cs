using Newtonsoft.Json.Linq;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Extensions;
using SH5ApiClient.Models;
using SH5ApiClient.Models.Enums;
using System;
using System.Linq;
using System.Reflection;

namespace SH5ApiClient.Core.Requests
{
    public class GDocsRequest : RequestBase
    {
        //Имя процедуры
        private const string procName = "GDocs";

        /// <summary>Отчет с  даты включительно</summary>
        [OriginalName("1")]
        public DateTime? DateFrom { get; set; }

        /// <summary>Отчет по дату включительно</summary>
        [OriginalName("2")]
        public DateTime? DateTo { get; set; }

        /// <summary>Типы запрашиваемых накладных</summary>
        [OriginalName("111\\8")]
        public TTNTypeForRequest? TTNTypeForRequest { get; set; }

        /// <summary>Фильтр накладных</summary>
        [OriginalName("6")]
        public GDocsRequestFilter? GDocsRequestFilter { get; set; }

        public override OperationBase Operation => new ExecOperation();

        public GDocsRequest(ConnectionParamSH5 connectionParam) : base(procName, connectionParam) { }

        public override string CreateJsonRequest()
        {
            JArray original108 = new JArray();
            JArray values108 = new JArray();


            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(t => t.CustomAttributes
                    .Any(x => x.AttributeType == typeof(OriginalNameAttribute)));
            foreach (var property in properties)
            {
                var value = property.GetValue(this);
                if (value != null)
                {
                    original108.Add(this.GetOriginalNameAttributeFromProperty(property.Name));
                    values108.Add(new JArray(value));
                }
            }

            JObject root = new JObject(
                new JProperty(this.GetOriginalNameAttributeFromProperty(nameof(UserName)), UserName),
                new JProperty(this.GetOriginalNameAttributeFromProperty(nameof(Password)), Password),
                new JProperty(this.GetOriginalNameAttributeFromProperty(nameof(ProcName)), ProcName));

            if (original108.Count > 0)
            {
                JArray input = new JArray()
                {
                    new JObject(
                        new JProperty("head", "108"),
                        new JProperty("original", original108),
                        new JProperty("values", new JArray(values108)))
                };
                root.Add(new JProperty("Input", input));
            }

            return root.ToString();
        }
    }
}
