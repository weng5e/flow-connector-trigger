using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlowTriggerManagingService
{
    public interface ITriggerManagingService
    {
        Task UpdateTriggerAsync(string hookId, Uri callbackUri);

        Task DeleteTriggerAsync(string hookId);

        Task<Uri> GetCallbackAsync(string hookId);

        IEnumerable<FlowTriggerDataContract> ListAllTriggerAsync();
    }
}
