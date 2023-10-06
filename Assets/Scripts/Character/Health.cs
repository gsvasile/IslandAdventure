using RPG.Core;
using RPG.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Character
{
    public class Health : MonoBehaviour
    {
        public event UnityAction OnStartDefeated;

        private Animator animatorComponent;
        private BubbleEvent bubbleEventComponent;

        private bool isDefeated = false;

        public float HealthPoints { get; set; } = 0f;

        private void Awake()
        {
            animatorComponent = GetComponentInChildren<Animator>();
            bubbleEventComponent = GetComponentInChildren<BubbleEvent>();
        }

        private void OnEnable()
        {
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            bubbleEventComponent.OnBubbleCompleteDefeat += HandleBubbleCompleteDefeat;
        }

        private void OnDisable()
        {
            bubbleEventComponent.OnBubbleCompleteDefeat -= HandleBubbleCompleteDefeat;
        }

        public void TakeDamage(float damageAmount)
        {
            HealthPoints = Mathf.Max(HealthPoints - damageAmount, 0);

            if (CompareTag(Constants.PLAYER_TAG))
            {
                EventManager.RaiseChangePlayerHealth(HealthPoints);
            }

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

            if (CompareTag(Constants.ENEMY_TAG))
            {
                OnStartDefeated?.Invoke();
            }

            animatorComponent.SetTrigger(Constants.ANIMATOR_PARAMETER_DEFEATED);
            isDefeated = true;
        }

        private void HandleBubbleCompleteDefeat()
        {
            Destroy(gameObject);
        }
    }
}
