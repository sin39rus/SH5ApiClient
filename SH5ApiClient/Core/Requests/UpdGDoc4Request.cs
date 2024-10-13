using Newtonsoft.Json.Linq;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Infrastructure.Extensions;
using SH5ApiClient.Models;
using SH5ApiClient.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace SH5ApiClient.Core.Requests
{
    internal class UpdGDoc4Request : RequestBase
    {
        private readonly GDoc4 _doc;
        internal UpdGDoc4Request(ConnectionParamSH5 connectionParamSH5, GDoc4 doc) : base("UpdGDoc4", connectionParamSH5)
        {
            _doc = doc ?? throw new ArgumentNullException(nameof(doc));
        }
        public override OperationBase Operation =>
            new ExecOperation();

        public override string CreateJsonRequest()
        {
            JObject root = new JObject(
                    new JProperty("UserName", UserName),
                    new JProperty("Password", Password),
                    new JProperty("procName", ProcName),
                    new JProperty("Input", _doc.ToJson()));
            return root.ToString();
        }
        private string ReсursiveMethod(PropertyInfo info, string originalName = null)
        {
            if (info.PropertyType.GetProperties().Any(t => t.ContainsOriginalName()))
            {
                originalName += "//" + info.GetOriginalName();
                List<string> names = new List<string>();
                foreach (var prop in info.PropertyType.GetProperties().Where(t => t.ContainsOriginalName()))
                {
                    names.Add(ReсursiveMethod(prop, originalName));
                }
                return string.Join("", names);
            }
            else
                return originalName + "//" + info.GetOriginalName() + Environment.NewLine;
        }
        private static string IterativeMethod()
        {
            List<string> fields = new List<string>();

            Type root = typeof(GDoc4);

            foreach (var propL0 in root.GetProperties().Where(t => t.ContainsOriginalName()))
            {
                string l0 = propL0.GetOriginalName();
                if (propL0.PropertyType.GetProperties().Any(t => t.ContainsOriginalName()))
                {
                    foreach (var propL1 in propL0.PropertyType.GetProperties().Where(t => t.ContainsOriginalName()))
                    {
                        string l1 = propL1.GetOriginalName();
                        if (propL1.PropertyType.GetProperties().Any(t => t.ContainsOriginalName()))
                        {
                            foreach (var propL2 in propL1.PropertyType.GetProperties().Where(t => t.ContainsOriginalName()))
                            {
                                string l2 = propL2.GetOriginalName();
                                fields.Add(l0 + "//" + l1 + "//" + l2);
                            }
                        }
                        else
                            fields.Add(l0 + "//" + l1);
                    }
                }
                else
                    fields.Add(l0);
            }
            return string.Join(Environment.NewLine, fields);
        }
    }
}
