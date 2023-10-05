using RPG.Utility;
using UnityEngine;
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
            Controller.Buttons = Controller.Root
                .Query<Button>(null, Constants.CLASS_MENU_BUTTON)
                .ToList();

            Controller.Buttons[0].AddToClassList(Constants.CLASS_MENU_ACTIVE);
        }

        public override void SelectButton()
        {
            Button button = Controller.Buttons[Controller.CurrentSelection];

            Debug.Log(button.name);
        }
    }
}