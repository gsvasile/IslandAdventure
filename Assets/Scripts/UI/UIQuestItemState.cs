using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using RPG.Utility;

namespace RPG.UI
{
    public class UIQuestItemState : UIBaseState
    {
        private VisualElement questItemContainer;
        private Label questItemText;
        private PlayerInput playerInputCmp;

        public UIQuestItemState(UIController ui) : base(ui) { }

        public override void EnterState()
        {
            playerInputCmp = GameObject.FindGameObjectWithTag(
                Constants.GAME_MANAGER_TAG
            ).GetComponent<PlayerInput>();

            playerInputCmp.SwitchCurrentActionMap(Constants.UI_ACTION_MAP);

            questItemContainer = Controller.Root.Q<VisualElement>("quest-item-container");
            questItemText = questItemContainer.Q<Label>("quest-item-label");

            questItemContainer.style.display = DisplayStyle.Flex;
        }

        public override void SelectButton()
        {
            questItemContainer.style.display = DisplayStyle.None;
            playerInputCmp.SwitchCurrentActionMap(Constants.GAMEPLAY_ACTION_MAP);
        }
    }
}