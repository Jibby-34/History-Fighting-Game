using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private CharacterData characterData;
    public SelectedCharacter selectedCharacter;
    private int health;
    public RectTransform fillImage;
    public int width = 200;
    public int height = 25;

    void Start()
    {
        health = characterData.health;
        characterData = selectedCharacter.selectedCharacter;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        health = health - damage;
        UpdateHealthBar();
        Debug.Log(health);
    }

    void UpdateHealthBar()
    {
        Debug.Log(((float)health / characterData.health) * width);
        fillImage.sizeDelta = new Vector2(((float)health / characterData.health) * width, height);   
    }
}
