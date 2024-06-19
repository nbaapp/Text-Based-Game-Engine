using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ChoiceManager : MonoBehaviour
{
    public List<Button> choiceButtons;

    public void DisplayChoices(List<Choice> choices)
    {
        RequirementManager requirementManager = FindObjectOfType<RequirementManager>();

        for (int i = 0; i < choiceButtons.Count; i++)
        {
            if (i < choices.Count)
            {
                Choice choice = choices[i];

                // Check if all requirements are met
                bool isAccessible = true;
                foreach (var requirement in choice.requirements)
                {
                    if (!requirementManager.IsRequirementMet(requirement.requirementName))
                    {
                        isAccessible = false;
                        break;
                    }
                }

                choiceButtons[i].gameObject.SetActive(isAccessible);
                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = choice.choiceText;


                if (isAccessible)
                {
                    int index = i; // Capture the index for the button's click event
                    choiceButtons[i].onClick.RemoveAllListeners();
                    choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(index, choice.changes));
                }
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnChoiceSelected(int index, List<Requirement> changes)
    {
        // Apply changes to requirements
        FindObjectOfType<RequirementManager>().ApplyChanges(changes);
        FindObjectOfType<StoryInterpreter>().OnChoiceMade(index);
    }
}
