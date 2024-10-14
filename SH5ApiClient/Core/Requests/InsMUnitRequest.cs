using Newtonsoft.Json.Linq;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Extensions;
using SH5ApiClient.Models;
using SH5ApiClient.Models.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SH5ApiClient.Core.Requests
{
    public class InsMUnitRequest : RequestBase
    {
        [OriginalName("3")]
        public string Name {  get; set; }

        [OriginalName("41")]
        public decimal Ratio {  get; set; }

        [OriginalName("205\\1")]
        public uint GroupRid {  get; set; }
        public InsMUnitRequest(ConnectionParamSH5 connectionParam, string name, decimal ratio, uint groupRid) : base("InsMUnit", connectionParam)
        {
            Name = name;
            Ratio = ratio;
            GroupRid = groupRid;
        }
        public override OperationBase Operation => new ExecOperation();

        public override string CreateJsonRequest()
        {
            JArray input = new JArray();

            JObject obj206 = new JObject();
            JArray original206 = new JArray();
            JArray values206 = new JArray();

            original206.Add(this.GetOriginalNameAttributeFromProperty(nameof(Name)));
            values206.Add(new JArray(Name));

            original206.Add(this.GetOriginalNameAttributeFromProperty(nameof(Ratio)));
            values206.Add(new JArray(Ratio));

            original206.Add(this.GetOriginalNameAttributeFromProperty(nameof(GroupRid)));
            values206.Add(new JArray(GroupRid));

            obj206.Add(new JProperty("head", "206"));
            obj206.Add(new JProperty("original", original206));
            obj206.Add(new JProperty("values", new JArray(values206)));

            input.Add(obj206);

            JObject main = new JObject(
                new JProperty("UserName", UserName),
                new JProperty("Password", Password),
                new JProperty("procName", ProcName),
                new JProperty("Input", input));

            return main.ToString();
        }
    }
}
