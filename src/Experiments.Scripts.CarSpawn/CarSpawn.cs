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
            _service = new VehicleService();

            Tick += _service.Update;
            KeyUp += OnKeyUp;
            KeyDown += OnKeyDown;
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Oemcomma)
            {
                Logger.Log("Creating vehicle");
                _service.SpawnMovingVehicle(explodeOnImpact: false, hasRider: true);
            }
        }
    }
}