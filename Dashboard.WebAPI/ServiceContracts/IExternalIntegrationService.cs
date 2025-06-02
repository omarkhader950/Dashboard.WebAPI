using Dashboard.WebAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.WebAPI.ServiceContracts
{
    public interface IExternalIntegrationService
    {
        Task<List<NormalizedResultDto>> FetchAllAsync();
    }
}
