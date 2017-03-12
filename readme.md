# Experiments #

This is a collection of scripts which exercise various functionalities of the ScriptHookVDotNet library.


## Build ##

The project is setup to merge all the assemblies back into a single file called
`Experiments.Scripts.dll`. There is also a post-build step setup to copy
`Experiments.Scripts.dll` to the Steam Directory:

 `copy /Y "$(TargetDir)$(TargetName).dll"
		  "%ProgramFiles(x86)%\Steam\steamapps\common\Grand Theft Auto V\scripts"`

### ILMerge ###

Global references should be first added to the top level project, `Experiments.Scripts`. Then
when referencing from other projects, ensure that *Copy Local* is set to `False`, otherwise ILMerge
will throw an error during compilation:

`ILMerge.Merge: ERROR!!: Duplicate type 'Experiments.Utilities.Logger' found in assembly
'Experiments.Utilities'. Do you want to use the /alllowDup option?`

*NOTE:* `ScriptHookVDotNet.dll` must always have *Copy Local* set to `False`
when referenced in a script, otherwise ILMerge will fail with:
`ILMerge.Merge: The assembly 'ScriptHookVDotNet' is not marked as containing only managed code.`

## CarSpawn ##

Simple script for spawning vehicles with various properties on demand.