using System;
using UnityEngine;

namespace RPG.Character
{
    public class Health : MonoBehaviour
    {
        public float HealthPoints { get; set; } = 0f;

        public void TakeDamage(float damageAmount)
        {
            HealthPoints = Mathf.Max(HealthPoints - damageAmount, 0);
            print(HealthPoints);
        }
    }
}
