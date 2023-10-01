using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Character
{
    public class Combat : MonoBehaviour
    {
        public float Damage { get; set; } = 0f;

        private Animator animatorComponent;
        private BubbleEvent bubbleEventComponent;

        public bool IsAttacking { get; set; } = false;

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
            bubbleEventComponent.OnBubbleStartAttack += HandleBubbleStartAttack;
            bubbleEventComponent.OnBubbleCompleteAttack += HandleBubbleCompleteAttack;
            bubbleEventComponent.OnBubbleHit += HandleBubbleHit;
        }

        private void OnDisable()
        {
            UnRegisterEvents();
        }

        private void UnRegisterEvents()
        {
            bubbleEventComponent.OnBubbleStartAttack -= HandleBubbleStartAttack;
            bubbleEventComponent.OnBubbleCompleteAttack -= HandleBubbleCompleteAttack;
            bubbleEventComponent.OnBubbleHit -= HandleBubbleHit;
        }

        public void HandleAttack(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                return;
            }

            StartAttack();
        }

        public void StartAttack()
        {
            if (IsAttacking)
            {
                return;
            }

            animatorComponent.SetFloat(Constants.ANIMATOR_PARAMETER_SPEED, 0);
            animatorComponent.SetTrigger(Constants.ANIMATOR_PARAMETER_ATTACK);
        }

        private void HandleBubbleStartAttack()
        {
            IsAttacking = true;
        }

        private void HandleBubbleCompleteAttack()
        {
            IsAttacking = false;
        }

        private void HandleBubbleHit()
        {
            RaycastHit[] targets = Physics.BoxCastAll(
                transform.position + transform.forward,
                transform.localScale / 2,
                transform.forward,
                transform.rotation,
                1f
            );

            foreach (RaycastHit target in targets)
            {
                if (CompareTag(target.transform.tag))
                {
                    continue;
                }

                Health healthComponent = target.transform.gameObject.GetComponent<Health>();

                if (healthComponent == null)
                {
                    continue;
                }

                healthComponent.TakeDamage(Damage);
            }
        }
    }
}
