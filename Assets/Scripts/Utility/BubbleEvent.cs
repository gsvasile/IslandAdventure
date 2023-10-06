using UnityEngine;
using UnityEngine.Events;

namespace RPG.Utility
{
    public class BubbleEvent : MonoBehaviour
    {
        public event UnityAction OnBubbleStartAttack;
        public event UnityAction OnBubbleCompleteAttack;
        public event UnityAction OnBubbleHit;
        public event UnityAction OnBubbleCompleteDefeat;

        private void OnStartAttack()
        {
            if (OnBubbleStartAttack == null)
            {
                return;
            }

            OnBubbleStartAttack.Invoke();
        }

        private void OnCompleteAttack()
        {
            if (OnBubbleCompleteAttack == null)
            {
                return;
            }

            OnBubbleCompleteAttack.Invoke();
        }

        private void OnHit()
        {
            if (OnBubbleHit == null)
            {
                return;
            }

            OnBubbleHit.Invoke();
        }

        private void OnCompleteDefeat()
        {
            if (OnBubbleCompleteDefeat == null)
            {
                return;
            }

            OnBubbleCompleteDefeat.Invoke();
        }
    }
}
