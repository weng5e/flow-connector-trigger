using System;
using System.Collections.Generic;
using System.Text;

namespace FlowTriggerManagingService
{
    public sealed class FlowTriggerDataContract : FlowTriggerDataContractBase
    {
        public string CallBackEndpoint { get; set; }

        public List<string> Properties { get; set; }

        public string ApiKey { get; set; } = "default-api-key";

        public FlowTriggerDataContract() { }

        public FlowTriggerDataContract(string hookId)
        {
            HookId = hookId;
        }
    }
}
