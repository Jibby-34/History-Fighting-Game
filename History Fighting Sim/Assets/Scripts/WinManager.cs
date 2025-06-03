using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public TextMeshProUGUI winText;
    public bool gameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        winText.text = "";
    }

    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene("StageSelect");
        }
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
        gameOver = true;
    }
}
