using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Models;
using SH5ApiClient.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH5ApiClient.Core.Requests
{
    public class CreateAttrRequest : RequestBase
    {
        public CreateAttrRequest(string bob, string ident, string caption, SHAttributeType sHAttributeType, ConnectionParamSH5 connectionParamSH5) : base(connectionParamSH5)
        {
            Bob = bob ?? throw new ArgumentNullException(nameof(bob));
            Ident = ident ?? throw new ArgumentNullException(nameof(ident));
            Caption = caption ?? throw new ArgumentNullException(nameof(caption));
            SHAttributeType = sHAttributeType;
        }
        public CreateAttrRequest(AttributeSH attr, ConnectionParamSH5 connectionParamSH5)
            : this(attr.Bob, attr.Identity, attr.Caption, attr.Type, connectionParamSH5)
        {

        }
        /// <summary>
        /// Идентификатор контейнера (SdbMan: Обслуживание - Статистика - Поля бин.объектов)
        /// </summary>
        [JsonProperty("bob")]
        public string Bob { get; set; }
        /// <summary>
        /// Идентификатор создаваемого поля
        /// </summary>
        [JsonProperty("ident")]
        public string Ident { get; set; }
        /// <summary>
        /// Заголовок создаваемого поля
        /// </summary>
        [JsonProperty("caption")]
        public string Caption { get; set; }
        /// <summary>
        /// Тип поля.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("type")]
        public SHAttributeType SHAttributeType { get; set; }

        public override OperationBase Operation => new CreateAttrOperation();

        public override string CreateJsonRequest() =>
            JsonConvert.SerializeObject(this);
    }
}
