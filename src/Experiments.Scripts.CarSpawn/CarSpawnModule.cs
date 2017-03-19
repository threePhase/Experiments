using GTA;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Experiments.Scripts.CarSpawn
{
    public class CarSpawnModule : IScriptModule
    {
        private VehicleService _service;

        public bool Activated { get; set; }
        public Dictionary<Keys, Action> Hotkeys { get; }
        public Dictionary<string, Action<string[]>> Hotstrings { get; }

        public string Name { get; }

        public CarSpawnModule() : this(nameof(CarSpawnModule)) { }

        public CarSpawnModule(string name)
        {
            Activated = true;

            Hotkeys = new Dictionary<Keys, Action>();

            const string CLEAN_UP = "clean_up";
            Hotstrings = new Dictionary<string, Action<string[]>>
            {
                { CLEAN_UP, (args) =>
                    {
                        if (args == null || args.Length > 0)
                        {
                            UI.Notify($"Usage: {CLEAN_UP}");
                            return;
                        }

                        World.GetAllEntities()
                             .ToList()
                             .ForEach(e => e.Delete());
                        UI.Notify("Cleaned up entities");
                    }
                }
            };

            Name = name;

            _service = new VehicleService();
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (Game.Player.Character.Weapons.Current.Hash == GTA.Native.WeaponHash.APPistol)
            {
                Game.Player.Character.Weapons.Current.InfiniteAmmo = true;
                // if AP Pistol if fired
                if (Game.Player.Character.IsShooting)
                {
                    _service.SpawnMovingVehicle(explodeOnImpact: false, hasRider: true);
                }
                else if (Game.Player.Character.IsReloading)
                {
                    _service.DestroyAllVehicles();
                }
            }
        }

        public void OnKeyUp(object sender, KeyEventArgs e)
        {
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
        }
    }
}