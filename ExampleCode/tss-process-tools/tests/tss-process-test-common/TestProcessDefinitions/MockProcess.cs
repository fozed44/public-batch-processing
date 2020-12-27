using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using log4net;
using Tss.Process.Contracts.Interface;
using Tss.Process.Contracts.Types.Info;

namespace Tss.Process.Test.Common {

    public class MockInputType {
        public bool   SuccessFlag { get; set; }
        public string StringValue { get; set; }
    } 
    
    public class MockOutputType {
        public bool   SuccessFlag { get; set;}
        public string StringValue { get; set; }
    }

    public class MockProcessDefinition : IProcessDefinition {
        
        private readonly ILog _log;

        public MockProcessDefinition() {
            _log        = LogManager.GetLogger(typeof(MockProcessDefinition));
            ProcessInfo = GetProcessInfo();
            Steps       = GetSteps();
        }
        
        public ProcessInfo ProcessInfo { get; }
        public IEnumerable<IStepDefinition> Steps { get; }
    
        public ProcessInfo GetProcessInfo() =>
            new ProcessInfo {
                AllowMultiple         = true,
                AllowMultiplePerCycle = false,
                Name                  = "MockProcess",
            };

        public IEnumerable<IStepDefinition> GetSteps() 
            => new List<IStepDefinition> {
                new DefaultStepDefinition<MockInputType ,MockOutputType> {
                    StepInfo = new StepInfo {
                        Name           = "MockProcess_Step 1",
                        Description    = "example step description",
                        Ordinal        = 0,
                        InputTypename  = typeof(MockInputType).Name,
                        OutputTypename = typeof(MockOutputType).Name
                    },
                    Func = (i) => {
                        _log.Info("*** INSIDE  STEP  ");
                        if (!i.SuccessFlag)
                            throw new ValidationException("Test Exception");
                        return new MockOutputType {
                            SuccessFlag = true,
                            StringValue = "test result string"
                        };
                    }
                },
                new DefaultStepDefinition<MockInputType, MockOutputType> {
                    StepInfo = new StepInfo {
                        Name           = "MockProcess_Step 2",
                        Description    = "example step description",
                        Ordinal        = 1,
                        InputTypename  = typeof(MockInputType).Name,
                        OutputTypename = typeof(TestOutputType).Name
                    },
                    Func = (i) => {
                        if (!i.SuccessFlag)
                            throw new ValidationException("Test Exception");
                        return new MockOutputType {
                            SuccessFlag = true,
                            StringValue = "test result string"
                        };
                    }
                }
            };
    }
}