using System.Collections.Generic;
using UnityEngine;

public class RequirementManager : MonoBehaviour
{
    private Dictionary<string, bool> requirements = new Dictionary<string, bool>();

    public bool IsRequirementMet(string requirementName)
    {
        return requirements.ContainsKey(requirementName) && requirements[requirementName];
    }

    public void SetRequirement(string requirementName, bool state)
    {
        requirements[requirementName] = state;
    }

    public void ApplyChanges(List<Requirement> changes)
    {
        foreach (var change in changes)
        {
            SetRequirement(change.requirementName, change.isMet);
        }
    }
}
