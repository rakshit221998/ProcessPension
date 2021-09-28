using ProcessPensionApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPensionApi.Services.IServices
{
    public interface IProcessPensionService
    {
        Task<PensionDetail> GetPensionDetailService(PensionerInput pensionerInput,string token);
        Task<ProcessPensionResponse> ProcessingCodeService(ProcessPensionInput processPensionInput,string token="");
    }
}
