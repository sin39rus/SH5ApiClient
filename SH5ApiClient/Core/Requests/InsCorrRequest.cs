using Newtonsoft.Json.Linq;

namespace SH5ApiClient.Core.Requests
{
    public class InsCorrRequest : RequestBase
    {
        private string _inn = string.Empty;
        private CorrType corrType = CorrType.OutsideCorrespondent;
        //Имя процедуры
        private const string procName = "InsCorr";
        public InsCorrRequest(ConnectionParamSH5 connectionParam, string name, string inn) : base(procName, connectionParam)
        {
            Name = name;
            INN = inn;
        }

        /// <summary>
        /// Name
        /// </summary>
        [OriginalName("3")]
        public string Name { set; get; }
        /// <summary>
        /// ИНН
        /// </summary>
        [OriginalName("2")]
        public string INN
        {
            get => _inn; set
            {
                if (value is null || !value.IsINN())
                    throw new ArgumentException($"Не корректный ИНН \"{value}\".");
                if (value.Length == 12)
                    CorrTypeEx = CorrTypeEx.PrivatePerson;
                _inn = value;
            }
        }
        /// <summary>
        /// Тип1
        /// </summary>
        [OriginalName("5")]
        public CorrType CorrType
        {
            get => corrType; set
            {
                corrType = value;
                CorrType3 = corrType switch
                {
                    CorrType.OutsideCorrespondent => Models.Enums.CorrType3.NotDefined,
                    _ => null,
                };
            }
        }
        /// <summary>
        /// Тип CorrType3 (внешних контрагентов)
        /// </summary>
        [OriginalName("31")]
        public CorrType3? CorrType3 { set; get; }
        /// <summary>
        /// Тип2
        /// </summary>
        [OriginalName("32")]
        public CorrTypeEx CorrTypeEx { set; get; } = CorrTypeEx.Organization;
        /// <summary>
        /// ПлательщикБанк1
        /// </summary>
        [OriginalName("34\\Bank_Name")]
        public string? BankName { set; get; }
        /// <summary>
        /// ПлательщикБИК
        /// </summary>
        [OriginalName("34\\Bank_BIK")]
        public string? BIK { set; get; }
        /// <summary>
        /// ПлательщикКорсчет
        /// </summary>
        [OriginalName("34\\Bank_CAcc")]
        public string? CorAccount { set; get; }
        /// <summary>
        /// ПлательщикСчет
        /// </summary>
        [OriginalName("34\\Bank_PAcc")]
        public string? BankAccount { set; get; }

        public override OperationBase Operation => new ExecOperation();

        public override string CreateJsonRequest()
        {
            JArray input = new();

            JObject obj107 = new();
            JArray original107 = new();
            JArray values107 = new();


            original107.Add(this.GetOriginalNameAttributeFromProperty(nameof(Name)));
            values107.Add(new JArray(Name));
            original107.Add(this.GetOriginalNameAttributeFromProperty(nameof(INN)));
            values107.Add(new JArray(INN));
            original107.Add(this.GetOriginalNameAttributeFromProperty(nameof(CorrType)));
            values107.Add(new JArray(CorrType));
            original107.Add(this.GetOriginalNameAttributeFromProperty(nameof(CorrTypeEx)));
            values107.Add(new JArray(CorrTypeEx));
            if (CorrType3 is not null)
            {
                original107.Add(this.GetOriginalNameAttributeFromProperty(nameof(CorrType3)));
                values107.Add(new JArray(CorrType3));
            }
            if (BankName is not null)
            {
                original107.Add(this.GetOriginalNameAttributeFromProperty(nameof(BankName)));
                values107.Add(new JArray(BankName));
            }
            if (BIK is not null)
            {
                original107.Add(this.GetOriginalNameAttributeFromProperty(nameof(BIK)));
                values107.Add(new JArray(BIK));
            }
            if (CorAccount is not null)
            {
                original107.Add(this.GetOriginalNameAttributeFromProperty(nameof(CorAccount)));
                values107.Add(new JArray(CorAccount));
            }
            if (BankAccount is not null)
            {
                original107.Add(this.GetOriginalNameAttributeFromProperty(nameof(BankAccount)));
                values107.Add(new JArray(BankAccount));
            }

            obj107.Add(new JProperty("head", "107"));
            obj107.Add(new JProperty("original", original107));
            obj107.Add(new JProperty("values", new JArray(values107)));
            input.Add(obj107);

            JObject main = new(
                new JProperty("UserName", UserName),
                new JProperty("Password", Password),
                new JProperty("procName", ProcName),
                new JProperty("Input", input));

            return main.ToString();
        }
    }
}
