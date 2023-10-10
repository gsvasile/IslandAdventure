using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Character
{
    public class NPCController : MonoBehaviour
    {
        private Canvas canvasComponent;

        private void Awake()
        {
            canvasComponent = GetComponentInChildren<Canvas>();
        }

        private void OnTriggerEnter()
        {
            canvasComponent.enabled = true;
        }

        private void OnTriggerExit()
        {
            canvasComponent.enabled = false;
        }

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if (!context.performed || !canvasComponent.enabled)
            {
                return;
            }

            print("talking with the NPC");
        }
    }
}
