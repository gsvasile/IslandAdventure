using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Quest
{
    public class TreasureChest : MonoBehaviour
    {
        [field: SerializeField] public Animator AnimatorComponent { get; set; }

        private bool isInteractable = false;
        private bool hasBeenOpened = false;


        private void OnTriggerEnter()
        {
            isInteractable = true;
        }

        private void OnTriggerExit()
        {
            isInteractable = false;
        }

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if (!isInteractable || hasBeenOpened)
            {
                return;
            }

            AnimatorComponent.SetBool(Constants.ANIMATOR_PARAMETER_IS_SHAKING, false);
            hasBeenOpened = true;
        }
    }
}
