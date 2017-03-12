﻿using Experiments.Scripts.CarSpawn.Vehicles;
using GTA;
using GTA.Math;
using GTA.Native;
using System.Collections.Generic;
using System.Drawing;
using System;

namespace Experiments.Scripts.CarSpawn
{
    class VehicleService
    {
        private IList<Entity> _entities;

        public VehicleService()
        {
            this._entities = new List<Entity>();
        }

        internal void Update(object sender, EventArgs e)
        {
            CleanUpDeadEntities();
        }

        public void SpawnMovingVehicle(bool explodeOnImpact)
        {
            // position vehicle 5 meters in front of player
            Vector3 position =
                Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0, 5, 0));

            // angle vehicle perpendicular to player
            float heading = Game.Player.Character.Heading;

            Vehicle vehicle = SpawnMotorcycleWithRider(position, heading);

            vehicle.Speed = 100f;
            vehicle.NumberPlate = "DIE";
            //vehicle.DirtLevel = 15f;

            if (explodeOnImpact)
            {
                SetToExplodeOnImpact(vehicle);
            }

            // spawn vehicle
            vehicle.PlaceOnGround();

            vehicle.ApplyForceRelative(new Vector3(0, 0, -10));
            // TODO: set vehicle mods

            this._entities.Add(vehicle);
        }

        private void CleanUpDeadEntities()
        {
            var livingEntities = new List<Entity>();
            foreach(var entity in this._entities)
            {
                if (entity.IsDead && !entity.IsOnScreen)
                {
                    entity.Delete();
                }
                else
                {
                    livingEntities.Add(entity);
                }
            }
            this._entities = livingEntities;
        }

        private Vehicle CreateRandomMotorcycle(Vector3 position, float heading)
        {
            // TODO: Make selection random
            VehicleHash motorcycleType = Motorcycles.GetRandomMotorcycle();

            Vehicle vehicle =
                World.CreateVehicle(motorcycleType, position, heading);

            SetColors(vehicle, Color.Crimson, Color.Black);

            return vehicle;
        }

        private Vehicle SpawnMotorcycleWithRider(Vector3 position, float heading)
        {
            Vehicle vehicle = CreateRandomMotorcycle(position, heading);
            this._entities.Add(vehicle.CreateRandomPedOnSeat(VehicleSeat.Driver));
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
