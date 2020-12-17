using System;
using System.Text.Json;
using log4net;
using Tss.Process.Contracts.Interface;
using Tss.Process.StepServer.Core.Interface;

namespace Tss.Process.Step.Server.Core {

    /// <summary>
    /// StepExecuter
    ///
    /// Executer a step given a IStepDefinition describing that step.
    /// </summery>
    public class StepExecuter : IStepExecuter {

       #region Fields

        private readonly ILog _log;

       #endregion Fields

       #region ctor

        public StepExecuter(ILog log) {
            _log = log;
        }

        public StepExecuter()
            : this(LogManager.GetLogger(typeof(StepExecuter))){}

       #endregion

       #region IStepExecuter

        public string Execute(IStepDefinition stepDefinition, string input) {
            var deserializedInput = DeserializeInput(stepDefinition, input);
            var func   = GetFunc(stepDefinition);
            var result = InvokeFunc(func, deserializedInput);
            return SerializeOutput(result);
        }

       #endregion 

       #region Internal

        internal object DeserializeInput(IStepDefinition stepDefinition, string input) {
            // Type.GetType and JsonSerializer.Deserialize have decent
            // error reporting so we don't really need to wrap these
            // calls.
            var type   = Type.GetType(stepDefinition.StepInfo.InputTypename);
            var result = JsonSerializer.Deserialize(input, type);

            _log?.Debug($"Deserialized input {input}.");
            return result;
        }
    
        internal string SerializeOutput(object toSerialize) {
            _log?.Debug($"Serialize {toSerialize}");

            var result = JsonSerializer.Serialize(toSerialize);

            _log?.Debug(result);

            return result;
        }

        internal Func<object, object>  GetFunc(IStepDefinition stepDefinition) {
            _log?.Debug($"Locating step function for {stepDefinition.StepInfo.Name}");

            var stepDefinitionType = stepDefinition.GetType();

            var funcProperty = stepDefinitionType.GetProperty(nameof(IStepDefinition<object, object>.Func));

            _log?.Debug($"Located {funcProperty}");

            return (Func<object, object>)funcProperty.GetValue(stepDefinition);
        }

        internal object InvokeFunc(Func<object, object> func, object @param) {
            _log?.Debug($"Execute Func.");
            var result = func.Invoke(@param);

            if(result == null)
               _log?.Warn("Null result returned from step func.");

            return result;
        }

       #endregion
    }    
}