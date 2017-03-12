using Experiments.Utilities;
using GTA;
using System.Windows.Forms;
using System;

namespace Experiments.Scripts.CarSpawn
{
    public class CreateVehicle : Script
    {
        private VehicleService _service;

        public CreateVehicle()
        {
            _service = new VehicleService();

            Tick += OnTick;
            KeyUp += OnKeyUp;
            KeyDown += OnKeyDown;
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (Game.Player.Character.Weapons.Current.Hash == GTA.Native.WeaponHash.APPistol)
            {
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

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
        }
    }
}