using System;
using System.Collections.Generic;
using System.Text;

namespace FlowTriggerManagingService
{
    public sealed class FlowTriggerDataContract
    {
        public string TenantId { get; set; }
        public string BotId { get; set; }
        public string HookId { get; set; }
        public string CallBackEndpoint { get; set; }

        public FlowTriggerDataContract() { }

        public FlowTriggerDataContract(string hookId, string tenantId, string botId, Uri callbackUri) {
            TenantId = tenantId;
            BotId = botId;
            HookId = hookId;
            CallBackEndpoint = callbackUri.ToString();
        }
    }
}
