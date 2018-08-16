using System;
using System.Collections.Generic;
using System.Text;

namespace FlowTriggerManagingService
{
    public class FlowTriggerDataContractBase
    {
        public string HookId { get; set; }

        public string HookName { get; set; } = "default-web-hook-name";
    }
}
