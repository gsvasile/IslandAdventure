using RPG.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Character
{
    public class NPCController : MonoBehaviour
    {
        private Canvas canvasComponent;

        [field: SerializeField] public TextAsset InkJSON { get; private set; }

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

            if (InkJSON == null)
            {
                Debug.LogWarning("Please add an ink file to the npc.");
                return;
            }

            EventManager.RaiseInitiateDialogue(InkJSON);
        }
    }
}
