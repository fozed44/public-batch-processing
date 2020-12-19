using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using log4net;
using Tss.Process.Contracts.Interface;
using Tss.Process.StepServer.Core.Interface;

namespace Tss.Process.StepServer.Core.Domain.Implementation {

    /// <summary>
    /// StepExecuter
    ///
    /// Executer a step given a IStepDefinition describing that step.
    /// </summery>
    public class StepExecuter<tInput,tOutput> : IStepExecuter {

       #region Fields

        private readonly ILog _log;

       #endregion Fields

       #region ctor

        public StepExecuter(ILog log) {
            _log = log;
        }

        public StepExecuter()
            : this(LogManager.GetLogger(typeof(StepExecuter<tOutput, tInput>))){}

       #endregion

       #region IStepExecuter

        public string Execute(IStepDefinition stepDefinition, string input) {
            var deserializedInput = DeserializeInput(stepDefinition, input);
            var func              = GetFunc(stepDefinition);
            var result            = InvokeFunc(func, deserializedInput);
            return SerializeOutput(result);
        }

       #endregion 

       #region Internal

        internal tInput DeserializeInput(IStepDefinition stepDefinition, string input) {
            // Type.GetType and JsonSerializer.Deserialize have decent
            // error reporting so we don't really need to wrap these
            // calls.
            var result = (tInput)JsonSerializer.Deserialize(input, typeof(tInput));

            _log?.Debug($"Deserialized input {input}.");
            return result;
        }
    
        internal string SerializeOutput(tOutput toSerialize) {
            _log?.Debug($"Serialize {toSerialize}");

            var result = JsonSerializer.Serialize(toSerialize);

            _log?.Debug(result);

            return result;
        }

        internal Func<tInput, tOutput> GetFunc(IStepDefinition stepDefinition) {
            _log?.Debug($"Locating step function for {stepDefinition.StepInfo.Name}");

            var stepDefinitionType = stepDefinition.GetType();

            var funcProperty       = stepDefinitionType.GetProperty(nameof(IStepDefinition<tInput, tOutput>.Func));

            _log?.Debug($"Located {funcProperty}");

            var uncastResult = funcProperty.GetValue(stepDefinition);
            return VerifiedFuncCast(uncastResult, stepDefinition);
        }

        internal tOutput InvokeFunc(Func<tInput, tOutput> func, tInput @param) {
            _log?.Debug($"Execute Func.");
            var result = func.Invoke(@param);

            if(result == null)
               _log?.Warn("Null result returned from step func.");
            return result;
        }

        /// <summary>
        /// Throw a ValidationException if the StepDefinition's Func type parameters 
        /// do not match the type paramters of this StepExecuter.
        /// </summary>
        internal Func<tInput, tOutput> VerifiedFuncCast(
            object          stepFunc,
            IStepDefinition stepDefinition
        ) {
            var funcType = stepFunc.GetType();
            
            var typeArgs = funcType.GetGenericArguments();

            if(typeArgs[0] != typeof(tInput)
            || typeArgs[1] != typeof(tOutput))
                throw new ValidationException(
                   $"validation exception: the generic type parameters of this executer " +
                   $"({typeof(tInput).Name}, {typeof(tOutput).Name}) do not match" +
                   $"the signature of the stepdefinition func ({typeArgs[0].Name}, {typeArgs[1].Name})." 
                );

            return (Func<tInput, tOutput>)stepFunc;
        }

       #endregion
    }    
}