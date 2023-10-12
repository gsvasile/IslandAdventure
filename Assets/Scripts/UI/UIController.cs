using System.Collections.Generic;
using RPG.Core;
using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace RPG.UI
{
    public class UIController : MonoBehaviour
    {
        private UIDocument uiDocumentComponent;
        public VisualElement Root { get; private set; }
        public List<Button> Buttons { get; set; }
        public VisualElement MainMenuContainer { get; private set; }
        public VisualElement PlayerInfoContainer { get; private set; }
        public Label HealthLabel { get; private set; }
        public Label PotionCountLabel { get; private set; }

        public UIBaseState CurrentState { get; set; }
        public UIMainMenuState MainMenuState { get; private set; }
        public UIDialogueState DialogueState { get; private set; }

        public int CurrentSelection { get; set; }

        private void Awake()
        {
            uiDocumentComponent = GetComponent<UIDocument>();
            Root = uiDocumentComponent.rootVisualElement;

            MainMenuContainer = Root.Q<VisualElement>(Constants.CLASS_MAIN_MENU_CONTAINER);
            PlayerInfoContainer = Root.Q<VisualElement>(Constants.CLASS_PLAYER_INFO_CONTAINER);
            HealthLabel = PlayerInfoContainer.Q<Label>(Constants.LABEL_HEALTH_NAME);
            PotionCountLabel = PlayerInfoContainer.Q<Label>(Constants.LABEL_POTIONS_NAME);

            MainMenuState = new UIMainMenuState(this);
            DialogueState = new UIDialogueState(this);
        }

        private void OnEnable()
        {
            EventManager.OnChangePlayerHealth += HandleChangePlayerHealth;
            EventManager.OnChangePlayerPotionCount += HandleChangePlayerPotionCount;
            EventManager.OnInitiateDialogue += HandleInitiateDialogue;
        }

        // Start is called before the first frame update
        private void Start()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (sceneIndex == 0)
            {
                CurrentState = MainMenuState;
                CurrentState.EnterState();
            }
            else
            {
                PlayerInfoContainer.style.display = DisplayStyle.Flex;
            }
        }

        private void OnDisable()
        {
            EventManager.OnChangePlayerHealth -= HandleChangePlayerHealth;
            EventManager.OnChangePlayerPotionCount -= HandleChangePlayerPotionCount;
            EventManager.OnInitiateDialogue -= HandleInitiateDialogue;
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

        private void HandleChangePlayerHealth(float newHealthPoints)
        {
            HealthLabel.text = newHealthPoints.ToString();
        }

        private void HandleChangePlayerPotionCount(int newPotionCount)
        {
            PotionCountLabel.text = newPotionCount.ToString();
        }

        private void HandleInitiateDialogue(TextAsset inkJSON)
        {
            CurrentState = DialogueState;
            CurrentState.EnterState();
        }
    }
}
