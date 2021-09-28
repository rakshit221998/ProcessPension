
using ProcessPensionApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPensionApi.DTOs
{
    public class PensionDetailDto
    {
        public string Name { get; set; }
        public DateTime Dateofbirth { get; set; }
        public string Pan { get; set; }
        public string AadharNumber { get; set; }
        public PensionType PensionType { get; set; }
        public Model.BankType BankType { get; set; }
        public double PensionerAmount { get; set; }
       
    }
    
  
}
