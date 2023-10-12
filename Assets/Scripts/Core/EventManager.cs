
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Core
{
    public static class EventManager
    {
        public static event UnityAction<float> OnChangePlayerHealth;
        public static event UnityAction<int> OnChangePlayerPotionCount;
        public static event UnityAction<TextAsset> OnInitiateDialogue;

        public static void RaiseChangePlayerHealth(float newHealthPoints) =>
            OnChangePlayerHealth?.Invoke(newHealthPoints);

        public static void RaiseChangePlayerPotionCount(int newPotionCount) =>
            OnChangePlayerPotionCount?.Invoke(newPotionCount);

        public static void RaiseInitiateDialogue(TextAsset inkJSON) =>
            OnInitiateDialogue?.Invoke(inkJSON);
    }
}