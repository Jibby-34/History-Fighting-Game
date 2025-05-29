using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "Characters/AttackData")]
public class AttackData : ScriptableObject
{
    public string attackName;

    [Header("Timing & Force")]
    public float duration = 1f;
    public float force = 0f;
    public float knockbackForce = 10f;
    public float launchAngle = 45f;
    public float delayTime = 1;

    [Header("Hitbox")]
    public Vector2 hitboxOffset;
    public Vector2 hitboxSize;
    public int damage = 10;

    [Header("Animation")]
    public AnimationClip attackAnimation;
}
