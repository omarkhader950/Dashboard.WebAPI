using Dashboard.WebAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.WebAPI.ServiceContracts
{
    public interface IIntegrationService
    {
        Task<List<object>?> GetDataAsync(string integrationName);

        Task<Dictionary<string, List<object>>?> GetAllDataGroupedByNameAsync();


    }
}
