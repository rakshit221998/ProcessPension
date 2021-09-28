
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcessPensionApi.DTOs;
using ProcessPensionApi.Model;
using ProcessPensionApi.Repository.IRepository;
using ProcessPensionApi.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProcessPensionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProcessPensionController : ControllerBase
    {
        static int count = 1;
        private static Dictionary<int, string> dictstatuscode = new Dictionary<int, string>()
        {
            { 10,"Pension Disbursement Success" },
            { 21,"Pension Amount Calculated is Wrong for three consecutive times." }

        };

        private IProcessPensionService processPensionService;
        public ProcessPensionController(IProcessPensionService processPensionService)
        {
            this.processPensionService = processPensionService;
        }
        

        [HttpGet("pensionDetail/{token}")]
        public async Task<IActionResult> GetPensionDetail([FromBody] PensionerInput pensionerInput,string token="")
        {
            double newAmount;
            var detailobj = await processPensionService.GetPensionDetailService(pensionerInput,token);
            if(detailobj==null)
            {
                return BadRequest("Invalid pensioner detail provided, please provide valid detail");
            }
            var salaryEarned = detailobj.SalaryEarned;
            if(detailobj.PensionType== PensionType.Self)
            {
                 newAmount = (salaryEarned) * 0.80 + detailobj.Allowances;
            }
            else
            {
                 newAmount = (salaryEarned) * 0.50 + detailobj.Allowances;
            }
            detailobj.PensionerAmount = newAmount;

            //int bankCharges = 0;
            //if (detailobj.BankType == (BankType)1)
            //    bankCharges = 500;

            //else
            //    bankCharges = 550;

            var objdto1 = new PensionDetailDto()
            {
                Name = detailobj.Name,
                Dateofbirth = detailobj.Dateofbirth,
                Pan = detailobj.Pan,
                AadharNumber=detailobj.AadharNumber,
                PensionType = (PensionType)detailobj.PensionType,
                BankType=detailobj.BankType,
                PensionerAmount = newAmount,
               

            };

            //return Ok(new { amount = newAmount });
            return Ok(objdto1);

        }

        [HttpPost("processingcode/{token}")]
        public async Task<IActionResult> ProcessingCode(ProcessPensionInput processPensionInput,string token="")
        {
            ProcessPensionResponse processPensionResponse;
            //Loop:          
            var obj = await processPensionService.ProcessingCodeService(processPensionInput,token);
            //int key;
            if(obj.ProcessPensionStatusCode=="Success")
            {
                processPensionResponse = new ProcessPensionResponse()
                {
                    ProcessPensionStatusCode = dictstatuscode[10]
                };
                return Ok(processPensionResponse);
            }
            else
            {
                //if (count <= 3)
                //{
                //    count++;
                //    goto Loop;
                //}

                processPensionResponse = new ProcessPensionResponse()
                {
                    ProcessPensionStatusCode = dictstatuscode[21]
                };
                return BadRequest(processPensionResponse);
            }


        }

    }
}
