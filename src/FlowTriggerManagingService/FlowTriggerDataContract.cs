using System;
using System.Collections.Generic;
using System.Text;

namespace FlowTriggerManagingService
{
    public sealed class FlowTriggerDataContract
    {
        public string HookId { get; set; }
        public string CallBackEndpoint { get; set; }

        public FlowTriggerDataContract() { }

        public FlowTriggerDataContract(string hookId, Uri callbackUri) {
            HookId = hookId;
            CallBackEndpoint = callbackUri.ToString();
        }
    }
}
