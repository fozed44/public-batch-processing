using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tss.Process.StepServer.Core.Contracts.Interface;

namespace TssProcess.Step.Server {
    
    [ApiController]
    [Route("Process/[controller]/v1")]
    public class StepController {

       #region Fields
        private readonly IStepService            _stepService;
        private readonly ILogger<StepController> _logger;
       #endregion

       #region ctor
        public StepController(
            IStepService            stepService,
            ILogger<StepController> logger
        ) {
            _stepService = stepService;
            _logger      = logger;
        } 

       #endregion

        [Route("execute/{stepName: string}")]
        public string ExecuteStep(
                       string stepName,
            [FromBody] string input
        ) {
           throw new NotImplementedException(); 
        }        
    } 
}