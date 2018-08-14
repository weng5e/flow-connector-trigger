using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleService.Controllers
{
    /// <summary>
    /// This is the response generated from this service which will be sent to Flow connector.
    /// </summary>
    public sealed class WebhookResponse
    {
        public string ActionType { get; set; }

        public string Parameter1 { get; set; }

        public string Parameter2 { get; set; }
    }
}
