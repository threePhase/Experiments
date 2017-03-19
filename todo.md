# TODO #

## AP Pistol Car Spawn ##

Make vehicle spawing an actual weapon that can be used by the player. Ideally it
would work like a sticky bomb and allow the player to detonate all the generated
vehicles that are still present.

- [x] - Update AP Pistol firing to spawn a random vehicle
- [x] - Update AP Pistol reloading to remove all the spawned vehicles
- [x] - Play around with `MarkAsNoLongerNeeded` instead of manually cleaning up entities
- [x] - Add detonating functionality to destroy all spawned vehicles
- [x] - Setup vehicles to spawn in aiming/camera direction
- [ ] - Setup riders to be angry and attack nearby peds


## Script Manager ##

Add manager which handles the global activation and deactivation of the various
experiments.

- [x] - Implement script manager in `Experiments.Scripts.Hotkey` which allows activating
     and deactivating scripts
	 - [x] - Add interface for exposing script name, activation status, hotkeys, and
	      hotstrings
     - [x] - Update `Experiments.Scripts.Hotkey` to subscribe and unsubscribe the
	      script events (Tick, KeyDown, KeyUp)


## Local Multiplayer ##

### Overseer ###

Add "overseer" mode where one player controls the camera with the keyboard
while the player is controlled by the usb controller. The "overseer" can then
spawn various things to help or hinder the player. Make camera somewhat sticky
to player, ideally snapping back after out of focus for a certain amount of
time.
