using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tss.Process.Test.Common;
using System.Linq;
using Tss.Process.StepServer.Core.Domain.Implementation;
using System;
using Tss.Process.Contracts.Interface;
using Tss.Process.Contracts.Types.Info;
using System.ComponentModel.DataAnnotations;

namespace Tss.Process.Server.Core.Test {

    [TestClass]
    public class When_generating_step_executer {

       #region Test classes

    
        /// <summary>
        /// These test classes are used to ensure proper exceptions are
        /// thrown when attempting to create a StepExecuter from an invalid
        /// IStepDefinition implementation.
        /// </summary>
        private class StepDefinitionWithNoFunc : IStepDefinition {
            public StepInfo StepInfo => new StepInfo { Name = "Step test info." };
        }

        private class StepDefinitionWithInvalidFuncSignature : IStepDefinition {
            public StepInfo StepInfo => new StepInfo { Name = "Step test info." };
            public Func<object> Func => throw new NotImplementedException();
        }

       #endregion

        /// <summary>
        /// Make sure that we can create a new StepExecuter with no
        /// issues.
        /// </summary>
        [TestMethod]
        public void Can_get_result_with_no_exception(){
            var testStepDefinition = new ExampleProcessDefinition().Steps.ElementAt(0);
            var testFactory        = new StepExecuterFactory();

            // The test StepDefinition we are passing in here
            // is valid. Therefor we should create a valid 
            // executer.
            var executer = testFactory.CreateExecuter(testStepDefinition);

            Assert.IsNotNull(executer);

            // The types on the Func of the test step definition we are
            // using are TestInputType and TestOutputType, the generic
            // types on the factory generated object should match.
            Assert.AreEqual(executer.GetType().GetGenericArguments()[0].Name, typeof(TestInputType).Name);
            Assert.AreEqual(executer.GetType().GetGenericArguments()[1].Name, typeof(TestOutputType).Name);
        }

        /// <summary>
        /// Pasing a step definition that does not have a Func property
        /// should throw a validation exception.
        /// </summary>
        [TestMethod]
        public void StepDefinition_with_now_Func_throws_validation_exception(){
            Assert.ThrowsException<ValidationException>(() =>
                new StepExecuterFactory().CreateExecuter(
                    new StepDefinitionWithNoFunc()
                )
            );
        }

        /// <summary>
        /// Passing a step definition that does have a Func prop but does
        /// not have the Func<tIn, tOut> signature will thorw a validation
        /// exception.
        /// </summary>
        [TestMethod]
        public void StepDefinition_with_func_with_invalid_signature_throws_validation_exception(){
            Assert.ThrowsException<ValidationException>(() =>
                new StepExecuterFactory().CreateExecuter(
                    new StepDefinitionWithInvalidFuncSignature()
                )
            );
        }
    }
}
