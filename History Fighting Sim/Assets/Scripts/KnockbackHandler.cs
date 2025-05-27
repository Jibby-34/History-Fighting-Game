using UnityEngine;

public class KnockbackHandler : MonoBehaviour
{
    public Rigidbody2D rb;
    public float knockbackForce = 10f;
    public float launchAngle = 45f; // degrees

    public void ApplyKnockback(Transform attacker)
    {
        // Determine if attacker is to the left or right
        bool attackerOnLeft = attacker.position.x < transform.position.x;

        // Get direction based on fixed angle and attacker side
        Vector2 direction = GetKnockbackDirection(attacker, launchAngle);

        // Optional: zero out existing velocity before applying knockback
        rb.linearVelocity = Vector2.zero;

        // Apply force
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
    }

    private Vector2 GetKnockbackDirection(bool attackerOnLeft, float angleDegrees)
    {
        float angle = angleDegrees * Mathf.Deg2Rad;

        // If attacker is on the left, launch rightward (+x); else leftward (-x)
        float xDir = attackerOnLeft ? Mathf.Cos(angle) : -Mathf.Cos(angle);
        float yDir = Mathf.Sin(angle);

        return new Vector2(xDir, yDir).normalized;
    }
}
