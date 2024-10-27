using TMPro;
using UnityEngine;

public class LifeUIController : MonoBehaviour
{
    public TextMeshProUGUI heartsText;

    public void SetCount (int count)
    {
        heartsText.SetText($"Hearts : {count}");
    }
}
