using TMPro;
using UnityEngine;

public class PointMarker : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void UpdateText(string newText)
    {
        text.text = newText;
    }
}
