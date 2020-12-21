using System;
using Microsoft.AspNetCore.Mvc;

namespace TssProcess.Step.Server {
    
    [ApiController]
    [Route("Process/[controller]/v1")]
    public class StepController {

        [Route("execute/{stepName: string}")]
        public string ExecuteStep(
                       string stepName,
            [FromBody] string input
        ) {
           throw new NotImplementedException(); 
        }        
    } 
}