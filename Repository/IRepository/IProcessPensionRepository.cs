using ProcessPensionApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPensionApi.Repository.IRepository
{
    public interface IProcessPensionRepository
    {
        Task<PensionDetail> PensionDetail(PensionerInput pensionerInput,string token);
        Task<ProcessPensionResponse> ProcessedCode(ProcessPensionInput processPensionInput,string token);
    }
}
