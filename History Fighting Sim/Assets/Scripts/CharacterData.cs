using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Characters/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public int health = 100;
    public Vector2 hitboxScale;
    public Vector2 hitboxPosition;

    [Header("Visuals")]
    public Sprite idleSprite;
    public RuntimeAnimatorController animator;

    [Header("Attacks")]
    public AttackData lightAttack;
    public AttackData heavyAttack;
    public AttackData specialAttack;
    public Vector2 spriteScale;
}

