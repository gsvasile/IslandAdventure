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
        }

        public override void SelectButton()
        {
            Debug.Log("Interaction Detected.");
        }
    }
}