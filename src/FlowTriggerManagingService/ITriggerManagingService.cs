using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlowTriggerManagingService
{
    public interface ITriggerManagingService
    {
        Task UpdateTriggerAsync(string hookId, string tenantId, string botId, Uri callbackUri);

        Task DeleteTriggerAsync(string hookId, string tenantId, string botId);

        Task<Uri> GetCallbackAsync(string hookId, string tenantId, string botId);

        IEnumerable<FlowTriggerDataContract> ListAllTriggerAsync();
    }
}
