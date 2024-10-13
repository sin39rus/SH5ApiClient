using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Exceptions;
using SH5ApiClient.Infrastructure.Extensions;
using SH5ApiClient.Models;
using SH5ApiClient.Models.DTO;
using SH5ApiClient.Models.Enums;

namespace SH5ApiClient.Core.Requests
{
    public class EnumValuesRequest : RequestBase
    {
        public string Head { private set; get; }
        public string Path { private set; get; }

        public override OperationBase Operation => new EnumOperation();

        public EnumValuesRequest(ConnectionParamSH5 connectionParam, string head, string path) : base(connectionParam)
        {
            if (string.IsNullOrWhiteSpace(head))
                throw new ArgumentException($"\"{nameof(head)}\" не может быть пустым или содержать только пробел.", nameof(head));
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException($"\"{nameof(path)}\" не может быть пустым или содержать только пробел.", nameof(path));
            Head = head;
            Path = path;
        }
        public override string CreateJsonRequest()
        {
            return new JObject(
                new JProperty("UserName", UserName),
                new JProperty("Password", Password),
                new JProperty("head", Head),
                new JProperty("path", Path))
                .ToString();
        }
    }
}
