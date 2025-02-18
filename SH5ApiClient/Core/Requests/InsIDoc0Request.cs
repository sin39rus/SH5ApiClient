using Newtonsoft.Json.Linq;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Exceptions;
using SH5ApiClient.Infrastructure.Extensions;
using SH5ApiClient.Models;
using SH5ApiClient.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SH5ApiClient.Core.Requests
{
    internal class InsIDoc0Request : RequestBase
    {
        [OriginalName("1")]
        public string DocRid { get; private set; }

        /// <summary>Опции заголовка накладной</summary>
        [OriginalName("33")]
        public GDocOptions Options { get; private set; } = 0;

        /// <summary>Дата накладной</summary>
        [OriginalName("31")]
        public DateTime TimeStamp { get; private set; }
        public InsIDoc0Request(ConnectionParamSH5 connectionParamSH5, string docRid, DateTime timeStamp) : base("InsIDoc0", connectionParamSH5)
        {
            DocRid = !string.IsNullOrEmpty(docRid) ? docRid : throw new ApiClientArgumentException(nameof(docRid));
            TimeStamp = timeStamp;
        }

        public override OperationBase Operation =>
            new ExecOperation();

        public override string CreateJsonRequest()
        {
            JArray input = new JArray();

            JObject obj117 = new JObject();
            JArray original117 = new JArray();
            JArray values117 = new JArray();



            original117.Add(this.GetOriginalNameAttributeFromProperty(nameof(Options)));
            values117.Add(new JArray(Options));

            original117.Add(this.GetOriginalNameAttributeFromProperty(nameof(TimeStamp)));
            values117.Add(new JArray(TimeStamp.ToString("yyyy-MM-dd")));

            obj117.Add(new JProperty("head", "117"));
            obj117.Add(new JProperty("original", original117));
            obj117.Add(new JProperty("values", new JArray(values117)));

            input.Add(obj117);


            JObject obj111 = new JObject();
            JArray original111 = new JArray();
            JArray values111 = new JArray();

            original111.Add(this.GetOriginalNameAttributeFromProperty(nameof(DocRid)));
            values111.Add(new JArray(DocRid));

            obj111.Add(new JProperty("head", "111"));
            obj111.Add(new JProperty("original", original111));
            obj111.Add(new JProperty("values", new JArray(values111)));
            input.Add(obj111);


            JObject main = new JObject(
                new JProperty("UserName", UserName),
                new JProperty("Password", Password),
                new JProperty("procName", ProcName),
                new JProperty("Input", input));
            return main.ToString();
        }
    }
}
