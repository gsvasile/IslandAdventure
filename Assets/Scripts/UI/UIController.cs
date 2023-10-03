using System.Collections.Generic;
using UnityEngine;
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

        public void HandleInteract()
        {
            CurrentState.SelectButton();
        }
    }
}
