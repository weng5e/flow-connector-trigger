using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlowTriggerManagingService
{
    public interface ITriggerManagingService
    {
        Task UpdateCallbackAsync(string hookId, Uri callbackUri);

        Task DeleteCallbackAsync(string hookId);
        Task<Uri> GetCallbackAsync(string hookId);

        Task UpdatePropertiesAsync(string hookId, IEnumerable<string> properties);

        Task<IEnumerable<string>> GetPropertiesAsync(string hookId);

        IEnumerable<FlowTriggerDataContract> ListAllTriggersAsync();
    }
}
