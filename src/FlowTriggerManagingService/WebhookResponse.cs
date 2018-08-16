using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowTriggerManagingService
{
    /// <summary>
    /// This is the response generated from this service which will be sent to Flow connector.
    /// </summary>
    public sealed class WebhookResponse
    {
        public string Parameter1 { get; set; }

        public string Parameter2 { get; set; }
    }
}
