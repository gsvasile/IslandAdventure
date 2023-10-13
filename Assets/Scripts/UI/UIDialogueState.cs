using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using Ink.Runtime;
using RPG.Utility;
using System.Collections.Generic;
using System;

namespace RPG.UI
{
    public class UIDialogueState : UIBaseState
    {
        private VisualElement dialogueContainer;
        private Label dialogueText;
        private VisualElement nextButton;
        private VisualElement choicesGroup;
        private Story currentStory;
        private PlayerInput playerInputComponent;

        private bool hasChoices = false;

        public UIDialogueState(UIController ui) : base(ui) { }

        public override void EnterState()
        {
            dialogueContainer = Controller.Root.Q<VisualElement>("dialogue-container");
            dialogueText = dialogueContainer.Q<Label>("dialogue-text");
            nextButton = dialogueContainer.Q<VisualElement>("dialogue-next-button");
            choicesGroup = dialogueContainer.Q<VisualElement>("choices-group");

            dialogueContainer.style.display = DisplayStyle.Flex;

            playerInputComponent = GameObject.FindGameObjectWithTag(
                Constants.GAME_MANAGER_TAG).GetComponent<PlayerInput>();
            playerInputComponent.SwitchCurrentActionMap(Constants.UI_ACTION_MAP);
        }

        public override void SelectButton()
        {
            UpdateDialogue();
        }

        public void SetStory(TextAsset inkJSON)
        {
            currentStory = new Story(inkJSON.text);
            UpdateDialogue();
        }

        public void UpdateDialogue()
        {
            dialogueText.text = currentStory.Continue();

            hasChoices = currentStory.currentChoices.Count > 0;

            if (hasChoices)
            {
                HandleNewChoices(currentStory.currentChoices);
            }
            else
            {
                nextButton.style.display = DisplayStyle.Flex;
                choicesGroup.style.display = DisplayStyle.None;
            }
        }

        private void HandleNewChoices(List<Choice> choices)
        {
            nextButton.style.display = DisplayStyle.None;
            choicesGroup.style.display = DisplayStyle.Flex;

            choicesGroup.Clear();
            Controller.Buttons?.Clear();

            choices.ForEach(CreateNewChoiceButton);
        }

        private void CreateNewChoiceButton(Choice choice)
        {
            Button choiceButton = new Button();

            choiceButton.AddToClassList("menu-button");
            choiceButton.text = choice.text;
            choiceButton.style.marginRight = 20;

            choicesGroup.Add(choiceButton);
        }
    }
}