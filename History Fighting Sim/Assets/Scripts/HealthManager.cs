using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public CharacterData characterData;
    private int health;

    void Start() {
        health = characterData.health;
    }

    public void TakeDamage(int damage) {
        health = health - damage;
        Debug.Log(health);
    }    

    public int GetHealth() {
        return health;
    }
}
