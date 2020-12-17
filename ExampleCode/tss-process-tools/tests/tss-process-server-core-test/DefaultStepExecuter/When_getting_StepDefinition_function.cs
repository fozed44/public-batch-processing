using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tss.Process.Test.Common;
using Tss.Process.Step.Server.Core;
using System.Linq;

namespace Tss.Process.Server.Core.Test {

    [TestClass]
    public class When_getting_stepDefinition_function {

        [TestMethod]
        public void Can_get_result_with_no_exception(){
            var processDefinition = new ExampleProcessDefinition();
            var stepExecuter      = new StepExecuter(); 
            var func = stepExecuter.GetFunc(processDefinition.Steps.ElementAt(0));
        }
    }
}
