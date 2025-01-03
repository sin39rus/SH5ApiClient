﻿using Newtonsoft.Json.Linq;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Extensions;
using SH5ApiClient.Models;
using System;

namespace SH5ApiClient.Core.Requests
{
    public class CorrRequest : RequestBase
    {
        [OriginalName("4")]
        public string Guid { private set; get; }

        public override OperationBase Operation => new ExecOperation();

        public CorrRequest(ConnectionParamSH5 connectionParamSH5, string guid) : base("Corr", connectionParamSH5)
        {
            if (System.Guid.TryParse(guid, out Guid value))
            {
                Guid = $"{{{value.ToString().ToUpperInvariant()}}}";
            }
            else
            {
                throw new ArgumentException($"Не корректное значение Guid \"{guid}\"", nameof(guid));
            }
        }

        public override string CreateJsonRequest()
        {
            JArray input = new JArray();

            JObject obj107 = new JObject();
            JArray original107 = new JArray();
            JArray values107 = new JArray();

            original107.Add(this.GetOriginalNameAttributeFromProperty(nameof(Guid)));
            values107.Add(new JArray(Guid));

            obj107.Add(new JProperty("head", "107"));
            obj107.Add(new JProperty("original", original107));
            obj107.Add(new JProperty("values", new JArray(values107)));
            input.Add(obj107);

            JObject main = new JObject(
                new JProperty("UserName", UserName),
                new JProperty("Password", Password),
                new JProperty("procName", ProcName),
                new JProperty("Input", input));

            return main.ToString();
        }
    }
}
