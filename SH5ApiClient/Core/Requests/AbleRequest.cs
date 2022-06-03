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
#pragma warning disable IDE0052 // Удалить непрочитанные закрытые члены
        private readonly string[] procList = new string[] { "InsPDoc0", "InsPDoc1", "Corrs", "LEntities" };
#pragma warning restore IDE0052 // Удалить непрочитанные закрытые члены

        public AbleRequest(ConnectionParamSH5 connectionParamSH5) : base(connectionParamSH5, ServerOperationType.sh5able)
        {
        }

        public override string CreateJsonRequest() =>
            JsonConvert.SerializeObject(this);

    }
}
