using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tss.Process.Contracts.Interface;
using Tss.Process.Contracts.Types.Info;
using Tss.Process.Data.Context;
using Tss.Process.Data.Entities;
using TssProcess;
using TssProcess.Data;

namespace tss_process_core_test {
    public class TestInputType {
        public bool   SuccessFlag { get; set; }
        public string StringValue { get; set; }
    } 
    
    public class TestOutputType {
        public bool   SuccessFlag { get; set;}
        public string StringValue { get; set; }
    }

    class ExampleProcessDefinition : IProcessDefinition {
        public ProcessInfo ProcessInfo { get; } = new ProcessInfo {
            AllowMultiple         = true,
            AllowMultiplePerCycle = false,
            Name                  = "example-name",
            Description           = "Example Description"
        };

        public List<IStepDefinition> Steps { get; } = new List<IStepDefinition> {
            new DefaultStepDefinition<TestInputType, TestOutputType> {
                StepInfo = new StepInfo {
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
        
    
    [TestClass]
    public class When_using_ProcessContext {

        [TestMethod]
        public void Database_is_created() {
            using (var context = new ProcessContext("Server=localhost;Database=Process;user id=sa;Password=Password@123")) {
                context.ProcessMetadata.Add(new ProcessMetadata {
                    AllowMultiple         = true,
                    AllowMultiplePerCycle = false,
                    Name                  = "example-name",
                    Description           = "Example Description"
                });
                context.SaveChanges();
            }
        }
    }
}