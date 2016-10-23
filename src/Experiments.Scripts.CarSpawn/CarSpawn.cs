using System;
using System.Windows.Forms;
using GTA;
using Experiments.Utilities;

namespace Experiments.Scripts.CarSpawn
{
    public class CreateVehicle : Script
    {
        public CreateVehicle()
        {
            this.Tick += onTick;
            this.KeyUp += onKeyUp;
            this.KeyDown += onKeyDown;
        }

        private void onTick(object sender, EventArgs e)
        {

        }

        private void onKeyUp(object sender, KeyEventArgs e)
        {
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Oemcomma)
            {
                Logger.Log("Creating vehicle");
                new VehicleService().SpawnMovingVehicle(explodeOnImpact: true);
            }
        }
    }
}