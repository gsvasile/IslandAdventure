using System;
using RPG.Utility;
using UnityEngine;

namespace RPG.Character
{
    public class Health : MonoBehaviour
    {
        private Animator animatorComponent;
        private bool isDefeated = false;

        public float HealthPoints { get; set; } = 0f;


        private void Awake()
        {
            animatorComponent = GetComponentInChildren<Animator>();
        }

        public void TakeDamage(float damageAmount)
        {
            HealthPoints = Mathf.Max(HealthPoints - damageAmount, 0);

            if (HealthPoints == 0)
            {
                Defeated();
            }
        }

        private void Defeated()
        {
            if (isDefeated)
            {
                return;
            }

            animatorComponent.SetTrigger(Constants.ANIMATOR_PARAMETER_DEFEATED);
            isDefeated = true;
        }
    }
}
