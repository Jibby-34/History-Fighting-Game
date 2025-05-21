using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Characters/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float lightAttackDuration = 1f;
    public float lightAttackForce = 0;
    public RuntimeAnimatorController animator;
    public Sprite idleSprite;
}
