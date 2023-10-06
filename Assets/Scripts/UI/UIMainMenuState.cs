using RPG.Core;
using RPG.Utility;
using UnityEngine.UIElements;

namespace RPG.UI
{
    public class UIMainMenuState : UIBaseState
    {
        public UIMainMenuState(UIController uIController)
        : base(uIController)
        {
        }

        public override void EnterState()
        {
            Controller.MainMenuContainer.style.display = DisplayStyle.Flex;

            Controller.Buttons = Controller.MainMenuContainer
                .Query<Button>(null, Constants.CLASS_MENU_BUTTON)
                .ToList();

            Controller.Buttons[0].AddToClassList(Constants.CLASS_MENU_ACTIVE);
        }

        public override void SelectButton()
        {
            Button button = Controller.Buttons[Controller.CurrentSelection];

            if (button.name == Constants.MENU_START_BUTTON_NAME)
            {
                SceneTransition.Initiate(1);
            }
        }
    }
}