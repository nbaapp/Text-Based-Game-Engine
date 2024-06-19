using UnityEngine;
using XNode;
using System.Collections.Generic;

[NodeTint("#885CB0")]
public class StoryNode : Node
{
    public string nodeIdentifier;
    [Input] public string enter;
    [TextArea] public string storyText;
    [Output(dynamicPortList = true)] public List<Choice> choices;


    // Override toString to show the node identifier in the editor
    public override string ToString()
    {
        return string.IsNullOrEmpty(nodeIdentifier) ? base.ToString() : nodeIdentifier;
    }

    // Return the correct value of an output port when requested
    public override object GetValue(NodePort port)
    {
        return choices;
    }
}

// A class to represent choices with conditions
[System.Serializable]
public class Choice
{
    public string choiceText;
    public List<Requirement> requirements; // List of requirements for the choice to be accessible
    public List<Requirement> changes; // List of requirements to change if this choice is taken
}
