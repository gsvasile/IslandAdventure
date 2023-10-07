namespace RPG.UI
{
    public abstract class UIBaseState
    {
        public UIController Controller { get; protected set; }

        public UIBaseState(UIController uIController)
        {
            Controller = uIController;
        }

        public abstract void EnterState();
        public abstract void SelectButton();
    }
}