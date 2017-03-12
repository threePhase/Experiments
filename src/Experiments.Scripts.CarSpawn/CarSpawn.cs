using System;
using System.Windows.Forms;
using GTA;
using Experiments.Utilities;
using System.Collections.Generic;

namespace Experiments.Scripts.CarSpawn
{
    public class CreateVehicle : Script
    {
        private IList<Vehicle> _vehicles;
        private VehicleService _service;

        public CreateVehicle()
        {
            this._service = new VehicleService();
            this._vehicles = new List<Vehicle>();

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
                this._vehicles.Add(this._service.SpawnMovingVehicle(explodeOnImpact: true));
            }
        }
    }
}