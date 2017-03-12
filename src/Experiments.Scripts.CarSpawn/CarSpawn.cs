using Experiments.Utilities;
using GTA;
using System.Windows.Forms;

namespace Experiments.Scripts.CarSpawn
{
    public class CreateVehicle : Script
    {
        private VehicleService _service;

        public CreateVehicle()
        {
            this._service = new VehicleService();

            this.Tick += this._service.Update;
            this.KeyUp += onKeyUp;
            this.KeyDown += onKeyDown;
        }

        private void onKeyUp(object sender, KeyEventArgs e)
        {
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Oemcomma)
            {
                Logger.Log("Creating vehicle");
                this._service.SpawnMovingVehicle(explodeOnImpact: true);
            }
        }
    }
}