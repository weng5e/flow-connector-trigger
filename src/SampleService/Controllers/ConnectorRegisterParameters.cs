using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleService.Controllers
{
    public sealed class ConnectorRegisterParameters
    {
        /// <summary>
        /// Call back url.
        /// </summary>
        public string CallbackUrl { get; set; }
    }
}
