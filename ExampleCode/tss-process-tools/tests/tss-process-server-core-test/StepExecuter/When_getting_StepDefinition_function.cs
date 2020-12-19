using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tss.Process.StepServer.Core.Domain.Implementation;
using Tss.Process.Test.Common;

namespace Tss.Process.ServerCore.Test {

    [TestClass]
    public class When_getting_stepDefinition_function {

        /// <summary>
        /// Test the GetFunc method under normal circumstances.
        /// </summary>
        [TestMethod]
        public void Can_get_result_with_no_exception() {
            var processDefinition = new ExampleProcessDefinition();
            var stepExecuter      = new StepExecuter<TestInputType, TestOutputType>(); 
            var func = stepExecuter.GetFunc(processDefinition.Steps.ElementAt(0));
        }

        /// <summary>
        /// The StepExecuter.GetFunc must throw a ValidationExcepion (that will will
        /// hopefully have an informative message) when the Func signature on the stepDefintion
        /// does not match the type parameters on the StepExecuter.
        /// </summary>
        [TestMethod]
        public void StepExecuter_with_mismatched_output_type_throws() {
            var processDefinition = new ExampleProcessDefinition();
            // Notice that the StepExecuter and has a different output type than
            // ExampleProcessDefinition
            var stepExecuter      = new StepExecuter<TestInputType, TestInputType>(); 
            Assert.ThrowsException<ValidationException>(() => 
                stepExecuter.GetFunc(processDefinition.Steps.ElementAt(0))
            );
        }

        /// <summary>
        /// The StepExecuter.GetFunc must throw a ValidationExcepion (that will will
        /// hopefully have an informative message) when the Func signature on the stepDefintion
        /// does not match the type parameters on the StepExecuter.
        /// </summary>
        [TestMethod]
        public void StepExecuter_with_mismatched_input_type_throws() {
            var processDefinition = new ExampleProcessDefinition();
            // Notice that the StepExecuter and has a different input type than
            // ExampleProcessDefinition
            var stepExecuter      = new StepExecuter<TestOutputType, TestOutputType>(); 
            Assert.ThrowsException<ValidationException>(() => 
                stepExecuter.GetFunc(processDefinition.Steps.ElementAt(0))
            );
        }
    }
}
