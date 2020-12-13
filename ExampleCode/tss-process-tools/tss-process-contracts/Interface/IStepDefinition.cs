using System;
using Tss.Process.Contracts.Types.Info;

namespace Tss.Process.Contracts.Interface {

    public interface IStepDefinition {
        StepInfo StepInfo { get; }
    }

    public interface IStepDefinition<tInput, tOutput> : IStepDefinition {
        Func<tInput, tOutput> Func         { get; }
    }
}