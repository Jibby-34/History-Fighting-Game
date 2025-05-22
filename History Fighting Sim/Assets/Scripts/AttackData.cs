using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "Characters/AttackData")]
public class AttackData : ScriptableObject
{
    public string attackName;

    [Header("Timing & Force")]
    public float duration = 1f;
    public float force = 0f;

    [Header("Hitbox")]
    public Vector2 hitboxOffset;
    public Vector2 hitboxSize;
    public int damage = 10;

    [Header("Animation")]
    public AnimationClip attackAnimation;
}
