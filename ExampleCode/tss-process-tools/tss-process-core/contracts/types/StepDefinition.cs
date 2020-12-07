using System;

namespace TssProcessCore.Types {

    public class StepDefinition<tInput, tOutput> : IStepDefinition {
        public StepMetadata           StepMetadata { get; set; }
        public Func<tInput, tOutput>  Func         { get; set; }
    }
}