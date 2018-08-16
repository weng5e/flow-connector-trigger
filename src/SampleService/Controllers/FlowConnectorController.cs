using FlowTriggerManagingService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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

        // GET /api/v1/flowconnector/hookId/{hookId}/Schema
        [HttpGet("Schema")]
        public async Task<object> GetSchemaAsync(string hookId)
        {
            var properties = await _triggerService.GetPropertiesAsync(hookId);
            return GenerateJSONSchema(properties);
        }

        // POST /api/v1/flowconnector/hookId/{hookId}
        [HttpPost]
        public async Task<IActionResult> PostAsync(string hookId, [FromBody] ConnectorRegisterParameters parameters)
        {
            if (!Uri.TryCreate(parameters.CallbackUrl, UriKind.Absolute, out var callback))
            {
                throw new InvalidOperationException("Callback URI is invalid.");
            }
            await _triggerService.UpdateCallbackAsync(hookId, callback);

            var deleteUrl = GenerateDeleteUri(Request.Scheme, Request.Host.ToString(), hookId);

            // https://docs.microsoft.com/en-us/connectors/custom-connectors/create-webhook-trigger#the-openapi-definition
            Request.HttpContext.Response.Headers.Add("Location", deleteUrl);
            return Ok();
        }

        // Delete /api/v1/flowconnector/hookId/{hookId}
        [HttpDelete]
        public async Task DeleteAsync(string hookId)
        {
            await _triggerService.DeleteCallbackAsync(hookId);
        }

        private static string GenerateDeleteUri(string scheme, string host, string hookId)
        {
            return $"{scheme}://{host}/api/v1/flowconnector/hookId/{hookId}";
        }

        private static object GenerateJSONSchema(IEnumerable<string> properties)
        {
            if (properties != null && properties.Count() > 0)
            {
                dynamic propertiesObj = new JObject();

                foreach (var prop in properties)
                {
                    dynamic propObj = new JObject();
                    propObj.type = "string";
                    propObj.description = prop;
                    propertiesObj[prop] = propObj;
                }

                dynamic obj = new JObject();
                obj.type = "object";
                obj.properties = propertiesObj;
                return obj;
            }

            return null;
        }
    }
}
