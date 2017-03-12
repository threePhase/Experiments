# TODO #

## AP Pistol Car Spawn ##

Make vehicle spawing an actual weapon that can be used by the player. Ideally it
would work like a sticky bomb and allow the player to detonate all the generated
vehicles that are still present.

[x] - Update AP Pistol firing to spawn a random vehicle
[x] - Update AP Pistol reloading to remove all the spawned vehicles
[] - Add detonating functionality to destroy all spawned vehicles


## Script Manager ##

Add manager which handles the global activation and deactivation of the various
experiments.

[] - Implement script manager in `Experiments.Scripts.Hotkey` which allows activating
     and deactivating scripts
	 [] - Add interface for exposing script name, activation status, hotkeys, and
	      hotstrings
     [] - Update `Experiments.Scripts.Hotkey` to subscribe and unsubscribe the
	      script events (Tick, KeyDown, KeyUp)