#define HIGH_LIFE
#define INDEPENDENCE_DAY
#define LAST_TEAM_STANDING
#define HEISTS
#define ILL_GOTTEN_GAINS_PT_2
#define CUNNING_STUNTS
#define BIKER

using GTA.Native;
using System;

namespace Experiments.Scripts.CarSpawn.Vehicles
{
    static internal class Motorcycles
    {
        #region MotorcycleList
        private static VehicleHash[] motorcycles =
            new VehicleHash[] {
                VehicleHash.Akuma,
                VehicleHash.Bagger,
                VehicleHash.Bati,
                VehicleHash.Bati2,
                VehicleHash.Blazer,
                VehicleHash.Blazer2,
                VehicleHash.Blazer3,
                VehicleHash.CarbonRS,
                VehicleHash.Daemon,
                VehicleHash.Double,
                VehicleHash.Faggio2,
                VehicleHash.Hexer,
                VehicleHash.Nemesis,
                VehicleHash.PCJ,
                VehicleHash.Policeb,
                VehicleHash.Ruffian,
                VehicleHash.Sanchez,
                VehicleHash.Vader,
#if HIGH_LIFE
                VehicleHash.Thrust,
#endif
#if INDEPENDENCE_DAY
                VehicleHash.Sovereign,
#endif
#if LAST_TEAM_STANDING
                VehicleHash.Hakuchou,
                VehicleHash.Innovation,
#endif
#if HEISTS
                VehicleHash.Enduro,
                VehicleHash.Lectro,
#endif
#if ILL_GOTTEN_GAINS_PT_2
                VehicleHash.Vindicator,
#endif
#if CUNNING_STUNTS
                /*
                VehicleHash.BF400,
                VehicleHash.Cliffhanger,
                VehicleHash.Gargoyle,
                */
#endif
#if BIKER
                // VehicleHash.Avarus,
                // VehicleHash.Bagger, TODO: Duplicate?
                // VehicleHash.Chimera,
                VehicleHash.Daemon,
                /*
                VehicleHash.Defiler,
                VehicleHash.FaggioMod,
                VehicleHash.Manchez,
                VehicleHash.Nightblade,
                VehicleHash.Ratbike,
                VehicleHash.StreetBlazer,
                VehicleHash.Wolfsbane,
                VehicleHash.ZombieChopper,
                VehicleHash.ZombieBobber,
                */
#endif
            };
        #endregion

        public static VehicleHash[] GetAllMotorcycles()
        {
            return motorcycles;
        }

        public static VehicleHash GetRandomMotorcycle()
        {
            var rand = new Random();
            int motorcycleId = rand.Next(0, motorcycles.Length);
            return motorcycles[motorcycleId];
        }
    }
}
