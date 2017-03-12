# TODO #

## Stick Bomb Car Spawn ##

Make vehicle spawing an actual weapon that can be used by the player. Ideally it
would work like a sticky bomb and allow the player to detonate all the generated
vehicles that are still present.

[] - Update sticky bomb firing to call `SpawnMovingVehicle`
[] - Increase sticky bomb firing rate
[] - Update sticky bomb detonating to destroy all spawned vehicles


## Script Manager ##

Add manager which handles the global activation and deactivation of the various
experiments.

[] - Implement script manager in `Experiments.Scripts.Hotkey` which allows activating
     and deactivating scripts
	 [] - Add interface for exposing script name, activation status, hotkeys, and
	      hotstrings
     [] - Update `Experiments.Scripts.Hotkey` to subscribe and unsubscribe the
	      script events (Tick, KeyDown, KeyUp)