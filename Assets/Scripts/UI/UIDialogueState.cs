using UnityEngine;
using UnityEngine.UIElements;
using Ink.Runtime;

namespace RPG.UI
{
    public class UIDialogueState : UIBaseState
    {
        private VisualElement dialogueContainer;
        private Label dialogueText;
        private VisualElement nextButton;
        private VisualElement choicesGroup;
        private Story currentStory;

        public UIDialogueState(UIController ui) : base(ui) { }

        public override void EnterState()
        {
            dialogueContainer = Controller.Root.Q<VisualElement>("dialogue-container");
            dialogueText = dialogueContainer.Q<Label>("dialogue-text");
            nextButton = dialogueContainer.Q<VisualElement>("dialogue-next-button");
            choicesGroup = dialogueContainer.Q<VisualElement>("choices-group");

            dialogueContainer.style.display = DisplayStyle.Flex;
        }

        public override void SelectButton() { }

        public void SetStory(TextAsset inkJSON)
        {
            currentStory = new Story(inkJSON.text);
            UpdateDialogue();
        }

        public void UpdateDialogue()
        {
            dialogueText.text = currentStory.Continue();
        }
    }
}