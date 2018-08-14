using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlowTriggerManagingService
{
    public sealed class TriggerManagingService : ITriggerManagingService
    {
        private ConcurrentDictionary<string, FlowTriggerDataContract> _data = new ConcurrentDictionary<string, FlowTriggerDataContract>();

        public Task DeleteTriggerAsync(string hookId, string tenantId, string botId)
        {
            var key = getKey(hookId, tenantId, botId);
            _data.Remove(key, out var _);
            return Task.CompletedTask;
        }

        public Task<Uri> GetCallbackAsync(string hookId, string tenantId, string botId)
        {
            var key = getKey(hookId, tenantId, botId);
            if (_data.TryGetValue(key, out var data))
            {
                var callback = new Uri(data.CallBackEndpoint);
                return Task.FromResult(callback);
            }
            else
            {
                return Task.FromResult<Uri>(null);
            }

        }

        public IEnumerable<FlowTriggerDataContract> ListAllTriggerAsync()
        {
            foreach(var kvp in _data)
            {
                yield return kvp.Value;
            }
        }

        public Task UpdateTriggerAsync(string hookId, string tenantId, string botId, Uri callbackUri)
        {
            var key = getKey(hookId, tenantId, botId);
            _data.AddOrUpdate(key, new FlowTriggerDataContract(hookId, tenantId, botId, callbackUri), (_, __) => new FlowTriggerDataContract(hookId, tenantId, botId, callbackUri));
            return Task.CompletedTask;
        }

        private string getKey(string hookId, string tenantId, string botId)
        {
            return $"{hookId}|{tenantId}|{botId}";
        }
    }
}
