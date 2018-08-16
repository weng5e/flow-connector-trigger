using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowTriggerManagingService
{
    public sealed class TriggerManagingService : ITriggerManagingService
    {
        private ConcurrentDictionary<string, FlowTriggerDataContract> _data = new ConcurrentDictionary<string, FlowTriggerDataContract>();

        public Task UpdateCallbackAsync(string hookId, Uri callbackUri)
        {
            _data.AddOrUpdate(hookId, new FlowTriggerDataContract(hookId) { CallBackEndpoint = callbackUri.ToString() }, (_, val) =>
            {
                val.CallBackEndpoint = callbackUri.ToString();
                return val;
            });
            return Task.CompletedTask;
        }

        public Task DeleteCallbackAsync(string hookId)
        {
            _data.AddOrUpdate(hookId, new FlowTriggerDataContract(hookId), (_, val) =>
            {
                val.CallBackEndpoint = string.Empty;
                return val;
            });
            return Task.CompletedTask;
        }

        public Task<Uri> GetCallbackAsync(string hookId)
        {
            if (_data.TryGetValue(hookId, out var data))
            {
                if (Uri.TryCreate(data.CallBackEndpoint, UriKind.Absolute, out var callback))
                {
                    return Task.FromResult(callback);
                }
            }
            return Task.FromResult<Uri>(null);
        }

        public Task UpdatePropertiesAsync(string hookId, IEnumerable<string> properties)
        {
            _data.AddOrUpdate(hookId, new FlowTriggerDataContract(hookId) { Properties = properties.ToList() }, (_, val) =>
            {
                val.Properties = properties.ToList();
                return val;
            });
            return Task.CompletedTask;
        }

        public Task<IEnumerable<string>> GetPropertiesAsync(string hookId)
        {
            if (_data.TryGetValue(hookId, out var data))
            {
                return Task.FromResult((IEnumerable<string>)data.Properties);
            }
            return Task.FromResult(Enumerable.Empty<string>());
        }

        public IEnumerable<FlowTriggerDataContract> ListAllTriggersAsync()
        {
            foreach (var kvp in _data)
            {
                yield return kvp.Value;
            }
        }

    }
}
