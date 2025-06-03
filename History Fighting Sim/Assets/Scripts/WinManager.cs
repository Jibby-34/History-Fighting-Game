using UnityEngine;
using TMPro;

public class WinManager : MonoBehaviour
{
    public TextMeshProUGUI winText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        winText.text = "";
    }

    public void FlagLoss(int loserId)
    {
        if (loserId == 1)
        {
            winText.text = "Player 2 Wins!";
        }
        else
        {
            winText.text = "Player 1 Wins!";
        }
    }
}
