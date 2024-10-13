using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Infrastructure.Attributes;
using SH5ApiClient.Infrastructure.Exceptions;
using SH5ApiClient.Infrastructure.Extensions;
using SH5ApiClient.Models;
using SH5ApiClient.Models.DTO;
using SH5ApiClient.Models.Enums;

namespace SH5ApiClient.Core.Requests
{
    /// <summary>
    /// Запрос наличия прав пользователя на выполнение процедур
    /// </summary>
    public class AbleRequest : RequestBase
    {
        /// <summary>
        /// Процедуры необходимые для работы программы.
        /// </summary>
        [JsonProperty("procList")]
#pragma warning disable IDE0052
        private readonly IEnumerable<string> procList;
#pragma warning restore IDE0052

        public AbleRequest(ConnectionParamSH5 connectionParamSH5, IEnumerable<string> procNameList) : base(connectionParamSH5)
        {
            if (procNameList is null)
                throw new ArgumentNullException(nameof(procNameList));
            if (!procNameList.Any())
                throw new ArgumentException("Список процедур пуст.");
            procList = procNameList;
        }

        public override OperationBase Operation => new AbleOperation();

        public override string CreateJsonRequest() =>
            JsonConvert.SerializeObject(this);

    }
}
