using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using log4net;
using Tss.Process.Contracts.Interface;
using Tss.Process.StepServer.Core.Interface;
namespace Tss.Process.StepServer.Core.Domain.Implementation {

    internal class StepExecuterFactory : IStepExecuterFactory {

       #region Fields

        private readonly ILog _log;

       #endregion 

       #region ctor

        public StepExecuterFactory()
            : this(LogManager.GetLogger(typeof(StepExecuterFactory))){}

        public StepExecuterFactory(ILog log) {
            _log = log;
        }

       #endregion

       #region IStepExecuterFactory

        public IStepExecuter CreateExecuter(IStepDefinition stepDefinition) {
            var signature = GetFuncSignatureTypes(stepDefinition);
            
            var genericType = typeof(StepExecuter<,>).MakeGenericType(signature.inputType, signature.outputType);
            return (IStepExecuter)Activator.CreateInstance(genericType);
        }

       #endregion

       #region Private

        private (Type inputType, Type outputType) GetFuncSignatureTypes(IStepDefinition stepDefinition) {
            var funcType = GetFuncType(stepDefinition);

            var typeArgs = funcType.GenericTypeArguments;

            if(typeArgs.Count() != 2)
                ThrowInvalidStepDefinition(stepDefinition);
            
            return (
                inputType:  typeArgs[0],
                outputType: typeArgs[1]
            );
        }

        private Type GetFuncType(IStepDefinition stepDefinition) {
            var stepDefinitionType = stepDefinition.GetType();

            var funcPropertyInfo = stepDefinitionType.GetProperty("Func");
                if(funcPropertyInfo == null)
                    ThrowInvalidStepDefinition(stepDefinition);

            return funcPropertyInfo.PropertyType;
        }

        private void ThrowInvalidStepDefinition(IStepDefinition stepDefinition) {
            throw new ValidationException (
                $"Can't create step executor for step defintion {stepDefinition.StepInfo.Name}. " +
                $"The implementation of IStepDefinition is not correct."
            );
        }
       #endregion


    }
}