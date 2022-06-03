using Newtonsoft.Json;

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

        public AbleRequest(ConnectionParamSH5 connectionParamSH5, IEnumerable<string> procNameList) : base(connectionParamSH5, ServerOperationType.sh5able)
        {
            if (procNameList is null)
                throw new ArgumentNullException(nameof(procNameList));
            if (!procNameList.Any())
                throw new ArgumentException("Список процедур пуст.");
            procList = procNameList;
        }

        public override string CreateJsonRequest() =>
            JsonConvert.SerializeObject(this);

    }
}
