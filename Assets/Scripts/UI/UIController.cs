using System.Collections.Generic;
using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace RPG.UI
{
    public class UIController : MonoBehaviour
    {
        private UIDocument uiDocumentComponent;
        public VisualElement Root { get; private set; }
        public List<Button> Buttons { get; set; }

        public UIBaseState CurrentState { get; set; }
        public UIMainMenuState MainMenuState { get; private set; }

        public int CurrentSelection { get; set; }

        private void Awake()
        {
            MainMenuState = new UIMainMenuState(this);

            uiDocumentComponent = GetComponent<UIDocument>();
            Root = uiDocumentComponent.rootVisualElement;
        }

        // Start is called before the first frame update
        private void Start()
        {
            CurrentState = MainMenuState;
            CurrentState.EnterState();
        }

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                return;
            }

            CurrentState.SelectButton();
        }

        public void HandleNavigate(InputAction.CallbackContext context)
        {
            if (!context.performed || Buttons.Count == 0)
            {
                return;
            }

            Buttons[CurrentSelection].RemoveFromClassList(Constants.CLASS_MENU_ACTIVE);

            Vector2 input = context.ReadValue<Vector2>();

            CurrentSelection += input.x < 0 ? -1 : 1;
            CurrentSelection = Mathf.Clamp(CurrentSelection, 0, Buttons.Count - 1);

            Buttons[CurrentSelection].AddToClassList(Constants.CLASS_MENU_ACTIVE);

        }
    }
}
