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

        public Task DeleteTriggerAsync(string hookId)
        {
            _data.Remove(hookId, out var _);
            return Task.CompletedTask;
        }

        public Task<Uri> GetCallbackAsync(string hookId)
        {
            if (_data.TryGetValue(hookId, out var data))
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
            foreach (var kvp in _data)
            {
                yield return kvp.Value;
            }
        }

        public Task UpdateTriggerAsync(string hookId, Uri callbackUri)
        {
            _data.AddOrUpdate(hookId, new FlowTriggerDataContract(hookId, callbackUri), (_, __) => new FlowTriggerDataContract(hookId, callbackUri));
            return Task.CompletedTask;
        }

    }
}
