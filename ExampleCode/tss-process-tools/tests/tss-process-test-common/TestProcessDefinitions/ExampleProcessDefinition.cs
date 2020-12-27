using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tss.Process.Contracts.Interface;
using Tss.Process.Contracts.Types.Info;

namespace Tss.Process.Test.Common {

    public class TestInputType {
        public bool   SuccessFlag { get; set; }
        public string StringValue { get; set; }
    } 
    
    public class TestOutputType {
        public bool   SuccessFlag { get; set;}
        public string StringValue { get; set; }
    }

    public class ExampleProcessDefinition : IProcessDefinition {

        public ProcessInfo ProcessInfo { get; } = new ProcessInfo {
            AllowMultiple         = true,
            AllowMultiplePerCycle = false,
            Name                  = "Example Process",
            Description           = "Example process description."
        };

        public IEnumerable<IStepDefinition> Steps { get; } = new List<IStepDefinition> {
            new DefaultStepDefinition<TestInputType, TestOutputType> {
                StepInfo = new StepInfo {
                    Name           = "ExampleProcess_step1",
                    Description    = "example step description",
                    Ordinal        = 0,
                    InputTypename  = typeof(TestInputType).Name,
                    OutputTypename = typeof(TestOutputType).Name
                },
                Func = (i) => {
                    if (!i.SuccessFlag)
                        throw new ValidationException("Test Exception");
                    return new TestOutputType {
                        SuccessFlag = true,
                        StringValue = "test result string"
                    };
                }
            },
            new DefaultStepDefinition<TestInputType, TestOutputType> {
                StepInfo = new StepInfo {
                    Name           = "ExampleProcess_step2",
                    Description    = "example step description",
                    Ordinal        = 1,
                    InputTypename  = typeof(TestInputType).Name,
                    OutputTypename = typeof(TestOutputType).Name
                },
                Func = (i) => {
                    if (!i.SuccessFlag)
                        throw new ValidationException("Test Exception");
                    return new TestOutputType {
                        SuccessFlag = true,
                        StringValue = "test result string"
                    };
                }
            }
        };
    }
}