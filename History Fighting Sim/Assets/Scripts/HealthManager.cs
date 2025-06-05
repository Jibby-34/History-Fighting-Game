using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private CharacterData characterData;
    public int playerId;
    private int health;
    public RectTransform fillImage;
    public int width = 200;
    public int height = 25;
    public bool isValidTarget = true;
    public int iFrames = 0;

    void Start()
    {
        if (playerId == 1)
        {
            characterData = GameData.player1Character;
        }
        else
        {
            characterData = GameData.player2Character;
        }
        health = characterData.health;
        UpdateHealthBar();
    }

    public void Update()
    {
        if (!isValidTarget)
        {
            Debug.Log(iFrames);

            iFrames = iFrames -= 1;

            if (iFrames <= 0)
            {
                isValidTarget = true;
            }
        }
    }

    public void TakeDamage(int damage, int iFrames)
    {
        if (isValidTarget) {
            health = health - damage;
            this.iFrames = iFrames;
            isValidTarget = false;
            UpdateHealthBar();
        }
    }

    void UpdateHealthBar()
    {
        Debug.Log(((float)health / characterData.health) * width);
        fillImage.sizeDelta = new Vector2(((float)health / characterData.health) * width, height);
    }

    public double GetHealth()
    {
        return health;
    }

    public bool GetValidTarget()
    {
        return isValidTarget;
    }
}
