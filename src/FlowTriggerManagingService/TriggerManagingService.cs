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

        public TriggerManagingService()
        {
            {
                var hookId = "101";
                var contract = new FlowTriggerDataContract(hookId);
                contract.HookName = "Send Email";
                contract.Properties = new List<string>();
                contract.Properties.Add("EmailSubject");
                contract.Properties.Add("EmailBody");
                contract.ApiKey = "access-key-1";
                _data.TryAdd(hookId, contract);
            }
            {
                var hookId = "102";
                var contract = new FlowTriggerDataContract(hookId);
                contract.HookName = "Send Email Hook";
                contract.Properties = new List<string>();
                contract.Properties.Add("Subject");
                contract.Properties.Add("Body");
                contract.ApiKey = "access-key-1";
                _data.TryAdd(hookId, contract);
            }
            {
                var hookId = "201";
                var contract = new FlowTriggerDataContract(hookId);
                contract.HookName = "Trigger Sending Email1";
                contract.Properties = new List<string>();
                contract.Properties.Add("FancyEmailSubject");
                contract.Properties.Add("FancyEmailBody");
                contract.ApiKey = "access-key-2";
                _data.TryAdd(hookId, contract);
            }
            {
                var hookId = "202";
                var contract = new FlowTriggerDataContract(hookId);
                contract.HookName = "Trigger Sending Email2";
                contract.Properties = new List<string>();
                contract.Properties.Add("FancySubject");
                contract.Properties.Add("FancyBody");
                contract.ApiKey = "access-key-2";
                _data.TryAdd(hookId, contract);
            }
        }

        public Task UpdateApiKeyAsync(string hookId, string key)
        {
            _data.AddOrUpdate(hookId, new FlowTriggerDataContract(hookId) { ApiKey = key }, (_, val) =>
            {
                val.ApiKey = key;
                return val;
            });
            return Task.CompletedTask;
        }

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

        public Task UpdatePropertiesAsync(string hookId, string hookName, IEnumerable<string> properties)
        {
            if (string.IsNullOrEmpty(hookName))
            {
                _data.AddOrUpdate(hookId, new FlowTriggerDataContract(hookId) { Properties = properties.ToList() }, (_, val) =>
                 {
                     val.Properties = properties.ToList();
                     return val;
                 });
            }
            else
            {
                _data.AddOrUpdate(hookId, new FlowTriggerDataContract(hookId) { Properties = properties.ToList(), HookName = hookName }, (_, val) =>
                  {
                      val.HookName = hookName;
                      val.Properties = properties.ToList();
                      return val;
                  });
            }
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

        public IEnumerable<FlowTriggerDataContractBase> ListHooksByKey(string key)
        {
            return _data.Where(kvp => kvp.Value.ApiKey == key).Select(kvp => kvp.Value);
        }
    }
}
