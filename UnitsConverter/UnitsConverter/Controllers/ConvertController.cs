using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitsConverter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvertController : ControllerBase
    {
        private readonly ConvertProcessor convertProcessor;

        public ConvertController(ConvertProcessor convertProcessor)
        {
            this.convertProcessor = convertProcessor;
        }

        [HttpGet]
        
        public string Get([BindRequired] string inputValue, [BindRequired] string outputUnit)
        {
            return convertProcessor.ProcessConversion(inputValue, outputUnit);
        }
    }
}
