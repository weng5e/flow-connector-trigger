using FlowTriggerManagingService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SampleService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExecutionController : ControllerBase
    {
        private readonly ITriggerManagingService _triggerService;
        private static readonly HttpClient _client = new HttpClient();

        public ExecutionController(ITriggerManagingService triggerService)
        {
            _triggerService = triggerService;
        }

        // GET api/execution/connectors
        [HttpGet("connectors")]
        public object GetConnectors()
        {
            return _triggerService.ListAllTriggerAsync();
        }

        // POST api/execution
        [HttpPost()]
        public async Task ExecAsync(string hookId, string para1, string para2)
        {
            var callback = await _triggerService.GetCallbackAsync(hookId);
            var req = new WebhookResponse() { Parameter1 = para1, Parameter2 = para2 };
            var res = await _client.PostAsync(callback, new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json"));
        }
    }
}
