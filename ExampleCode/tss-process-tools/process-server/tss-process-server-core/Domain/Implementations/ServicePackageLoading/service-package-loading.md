### Service Package Loading.

## Process GUID and version.
    - In addition to name identifiers (which must unique), processes must have a GUID property and a Version property.
        - The GUID is used to ensure that new process version coming from a step service initialization,
          which will update the db, is in fact the correct process that should be used to update the db
          and not just a process different process with the same name.
        
        - The processes Version property is an int that must have a value greater than the value currently
          stored in the db if the process server is to update the DB data. This should prevent an old version
          from overwriting a new version.

## Handling a StepService process initialization.
  - Search the database to see if a process with the same GUID exists.
    - If the process does not exist.
        - Search the db for the process name, throw an exception if the process name already exists in the database.
        - Search the db for all step names - step names must be globally unique. Throw  an exception if any of the step names are not unique.
    
    - If the process does exist.
        - If the incoming version is less than the version already in the database, throw an exception.
        - If the Version is the same 
          - Compare all data, throw an exception if it doesn't match.        
        - If the Version is newer
            - Ensure no process is currenty running.
            - Delete the process.
            - Add the new data to the database.
            
## Comparing Processes 

## Loading
  - Convert ServicePackageDto into into entities.
