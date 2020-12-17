
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tss.Process.Test.Common;
using System.Linq;
using Tss.Process.StepServer.Core.Domain.Implementation;

namespace Tss.Process.Server.Core.Test {

    [TestClass]
    public class When_generating_step_executer{

        [TestMethod]
        public void Can_get_result_with_no_exception(){
            var testStepDefinition = new ExampleProcessDefinition().Steps.ElementAt(0);
            var testFactory        = new StepExecuterFactory();

            // The test StepDefinition we are passing in here
            // is valid. Therefor we should create a valid 
            // executer.
            var executer           = testFactory.CreateExecuter(testStepDefinition);

            Assert.IsNotNull(executer);

            // The types on the Func of the test step definition we are
            // using are TestInputType and TestOutputType, the generic
            // types on the factory generated object should match.
            Assert.AreEqual(executer.GetType().GetGenericArguments()[0].Name, typeof(TestInputType).Name);
            Assert.AreEqual(executer.GetType().GetGenericArguments()[1].Name, typeof(TestOutputType).Name);
        }
    }
}
