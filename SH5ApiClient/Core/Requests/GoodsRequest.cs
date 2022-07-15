using Newtonsoft.Json.Linq;

namespace SH5ApiClient.Core.Requests
{
    public class GoodsRequest : RequestBase
    {
        private readonly uint _gGroupRID;
        public GoodsRequest(ConnectionParamSH5 connectionParam, uint gGroupRID) : base("Goods", connectionParam)
        {
            _gGroupRID = gGroupRID;
        }
        public GoodsRequest(ConnectionParamSH5 connectionParam, GGroup gGroup)
            : this(connectionParam, gGroup.Rid ?? throw new ApiClientException("Rid группы не может быть null."))
        {

        }

        public override OperationBase Operation => new ExecOperation();

        public override string CreateJsonRequest()
        {
            return new JObject(
                new JProperty("UserName", UserName),
                new JProperty("Password", Password),
                new JProperty("procName", ProcName),
                new JProperty("Input", new JArray(
                    new JObject(
                        new JProperty("head", "209"),
                        new JProperty("original", new JArray("1")),
                        new JProperty("values", new JArray()
                            { new JArray(_gGroupRID) }
                        ))))).ToString();
        }
    }
}
