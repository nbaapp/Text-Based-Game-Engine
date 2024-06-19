using UnityEngine;
using TMPro;

public class TextDisplayManager : MonoBehaviour
{
    public TextMeshProUGUI storyText;

    public void UpdateText(string newText)
    {
        storyText.text = newText;
    }
}
