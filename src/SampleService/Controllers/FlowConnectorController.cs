using FlowTriggerManagingService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleService.Controllers
{
    [Route("api/v1/[controller]/hookId/{hookId}")]
    [ApiController]
    public class FlowConnectorController : ControllerBase
    {
        private readonly ITriggerManagingService _triggerService;

        public FlowConnectorController(ITriggerManagingService triggerService)
        {
            _triggerService = triggerService;
        }

        // POST /api/v1/flowconnector/hookId/{hookId}
        [HttpPost]
        public async Task<IActionResult> PostAsync(string hookId, [FromBody] ConnectorRegisterParameters parameters)
        {
            if (!Uri.TryCreate(parameters.CallbackUrl, UriKind.Absolute, out var callback))
            {
                throw new InvalidOperationException("Callback URI is invalid.");
            }
            await _triggerService.UpdateTriggerAsync(hookId, callback);

            var deleteUrl = generateDeleteUri(Request.Scheme, Request.Host.ToString(), hookId);

            // https://docs.microsoft.com/en-us/connectors/custom-connectors/create-webhook-trigger#the-openapi-definition
            Request.HttpContext.Response.Headers.Add("Location", deleteUrl);
            return Ok();
        }

        // Delete /api/v1/flowconnector/hookId/{hookId}
        [HttpDelete]
        public async Task DeleteAsync(string hookId)
        {
            await _triggerService.DeleteTriggerAsync(hookId);
        }

        private static string generateDeleteUri(string scheme, string host, string hookId)
        {
            return $"{scheme}://{host}/api/v1/flowconnector/hookId/{hookId}";
        }
    }
}
