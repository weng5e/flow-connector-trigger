using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlowTriggerManagingService
{
    public interface ITriggerManagingService
    {
        Task UpdateCallbackAsync(string hookId, Uri callbackUri);

        Task UpdateApiKeyAsync(string hookId, string key);

        Task DeleteCallbackAsync(string hookId);

        Task<Uri> GetCallbackAsync(string hookId);

        Task UpdatePropertiesAsync(string hookId, string hookName, IEnumerable<string> properties);

        Task<IEnumerable<string>> GetPropertiesAsync(string hookId);

        IEnumerable<FlowTriggerDataContractBase> ListHooksByKey(string key);

        IEnumerable<FlowTriggerDataContract> ListAllTriggersAsync();
    }
}
