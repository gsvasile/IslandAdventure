using RPG.Core;
using RPG.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace RPG.Character
{
    public class Health : MonoBehaviour
    {
        public event UnityAction OnStartDefeated = () => { };

        public float HealthPoints { get; set; } = 0f;
        [SerializeField] private int potionCount = 1;
        [SerializeField] private float healAmount = 15f;

        private Animator animatorComponent;
        private BubbleEvent bubbleEventComponent;
        public Slider sliderComponent { get; private set; }

        private bool isDefeated = false;


        private void Awake()
        {
            animatorComponent = GetComponentInChildren<Animator>();
            bubbleEventComponent = GetComponentInChildren<BubbleEvent>();
            sliderComponent = GetComponentInChildren<Slider>();
        }

        private void Start()
        {
            if (CompareTag(Constants.PLAYER_TAG))
            {
                EventManager.RaiseChangePlayerPotionCount(potionCount);
            }
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

            if (sliderComponent != null)
            {
                sliderComponent.value = HealthPoints;
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
                OnStartDefeated.Invoke();
            }

            animatorComponent.SetTrigger(Constants.ANIMATOR_PARAMETER_DEFEATED);
            isDefeated = true;
        }

        private void HandleBubbleCompleteDefeat()
        {
            Destroy(gameObject);
        }

        public void HandleHeal(InputAction.CallbackContext context)
        {
            if (!context.performed || potionCount == 0)
            {
                return;
            }

            potionCount--;
            HealthPoints += healAmount;

            EventManager.RaiseChangePlayerHealth(HealthPoints);
            EventManager.RaiseChangePlayerPotionCount(potionCount);
        }
    }
}
