using ProcessPensionApi.Model;
using ProcessPensionApi.Repository.IRepository;
using ProcessPensionApi.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPensionApi.Services
{
    public class ProcessPensionService : IProcessPensionService
    {
        private IProcessPensionRepository processPensionRepository;


        public ProcessPensionService(IProcessPensionRepository processPensionRepository)
        {
            this.processPensionRepository = processPensionRepository;

        }

        public async Task<PensionDetail> GetPensionDetailService(PensionerInput pensionerInput,string token="")
        {
            var detailobj = await processPensionRepository.PensionDetail(pensionerInput,token);
            return detailobj;
        }

        public async Task<ProcessPensionResponse> ProcessingCodeService(ProcessPensionInput processPensionInput,string token="")
        {
            var obj = await processPensionRepository.ProcessedCode(processPensionInput,token);
            return obj;
        }
    }
}
