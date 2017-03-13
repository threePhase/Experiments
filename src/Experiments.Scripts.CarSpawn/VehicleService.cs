using Experiments.Scripts.CarSpawn.Vehicles;
using GTA;
using GTA.Math;
using GTA.Native;
using System.Drawing;
using System;
using Experiments.Utilities;

namespace Experiments.Scripts.CarSpawn
{
    class VehicleService
    {
        private TrackedEntityQueue _entities;
        private Random _random = new Random();

        public VehicleService()
        {
            _entities = new TrackedEntityQueue();
        }

        public void SpawnMovingVehicle(bool explodeOnImpact = false, bool hasRider = true)
        {
            // position vehicle 5 meters in front of player
            Vector3 position =
                Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0, 5, 0));

            // angle vehicle perpendicular to player
            float heading = Game.Player.Character.Heading;

            Vehicle vehicle = hasRider ?
                SpawnMotorcycleWithRider(position, heading) :
                CreateRandomMotorcycle(position, heading);

            vehicle.Speed = 100f;
            vehicle.NumberPlate = "DIE";
            //vehicle.DirtLevel = 15f;

            if (explodeOnImpact)
            {
                SetToExplodeOnImpact(vehicle);
            }

            vehicle.EngineRunning = true;

            vehicle.ApplyForceRelative(new Vector3(0, 0, -10));
            // TODO: set vehicle mods

            _entities.Enqueue(vehicle);
            Logger.Log($"Entity Count: {_entities.Count}");
        }

        public void DestroyAllVehicles()
        {
            while (_entities.Count > 0)
            {
                _entities.Dequeue();
            }
        }

        private Vehicle CreateRandomMotorcycle(Vector3 position, float heading)
        {
            VehicleHash motorcycleType = Motorcycles.GetRandomMotorcycle();

            Vehicle vehicle =
                World.CreateVehicle(motorcycleType, position, heading);

            // random colors
            Color primaryColor =
                Color.FromArgb(_random.Next(0, 255), _random.Next(0, 255), _random.Next(0, 255));
            Color secondaryColor =
                Color.FromArgb(_random.Next(0, 255), _random.Next(0, 255), _random.Next(0, 255));

            SetColors(vehicle, primaryColor, secondaryColor);

            return vehicle;
        }

        private Vehicle SpawnMotorcycleWithRider(Vector3 position, float heading)
        {
            Vehicle vehicle = CreateRandomMotorcycle(position, heading);
            _entities.Enqueue(vehicle.CreateRandomPedOnSeat(VehicleSeat.Driver));
            return vehicle;
        }

        private void SetColors(Vehicle vehicle,
                               Color primaryColor, Color secondaryColor)
        {
            vehicle.CustomPrimaryColor = primaryColor;
            vehicle.CustomSecondaryColor = secondaryColor;
        }

        private void SetToExplodeOnImpact(Vehicle vehicle)
        {
            // make vehicle explode on impact
            vehicle.PetrolTankHealth = -1f;
        }
    }
}
