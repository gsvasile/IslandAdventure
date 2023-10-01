using System;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

// RPG is the main namespace
// for our game.
namespace RPG.Example
{
    public class Robot : MonoBehaviour
    {
        private BatteryRegulations includedBattery;

        public Robot()
        {
            includedBattery = new Battery(80f);
            includedBattery.CheckHealth();
            Charger.ChargeBattery(includedBattery);
            includedBattery.CheckHealth();
            print(Charger.ChargerInUse);
        }
    }

    public class Battery : BatteryRegulations
    {
        public Battery(float health)
            : base(health)
        {
        }

        public override void CheckHealth()
        {
            Debug.Log(Health);
        }
    }

    public static class Charger
    {
        public static bool ChargerInUse { get; private set; } = false;

        public static void ChargeBattery(BatteryRegulations batteryToCharge)
        {
            ChargerInUse = true;
            batteryToCharge.Health = 100f;
        }
    }

    public abstract class BatteryRegulations
    {
        public float Health { get; set; } = 100f;

        public BatteryRegulations(float health)
        {
            Health = health;
            Debug.Log("New battery created!");
        }

        public abstract void CheckHealth();
    }
}