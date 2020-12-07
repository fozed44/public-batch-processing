# Batch processing system

## layers
---

### [Data](Data)

### [ORM](ORM)

### [Atomic](Atomic)
  - The atomic layer is a rest layer that exposes only atomic methods. This would be similar to a database transaction in that it should be impossible to fail in an unknown state. These methods should only change state when successful. This extends the idea of a database transaction to include any data, such as local files or s3 objects.

### [Procedure](Procedure)
  - The procedure layer provides an abstraction over small sets (1 or more) of steps.
  - A step consists of at most a one call to a method in the atomic layer method, and nullipotent control code.
  - Steps are defined in StepDefinitions.
  - Prcedures are defined in ProcedureDefinitions.
### [Orchestration](Orchestration)
  - UI elements that operate on procedures or readonly atomics. 
