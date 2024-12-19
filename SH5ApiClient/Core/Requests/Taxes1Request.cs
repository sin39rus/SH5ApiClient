using Newtonsoft.Json;
using SH5ApiClient.Core.ServerOperations;
using SH5ApiClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH5ApiClient.Core.Requests
{
    public class Taxes1Request : RequestBase
    {
        //Имя процедуры
        private const string procName = "Taxes1";
        public override OperationBase Operation => new ExecOperation();

        public Taxes1Request(ConnectionParamSH5 connectionParam) : base(procName, connectionParam) { }
        public override string CreateJsonRequest() =>
            JsonConvert.SerializeObject(this);
    }
}
