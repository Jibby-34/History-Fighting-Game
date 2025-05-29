using UnityEngine;
using System.Collections;

public class KnockbackHandler : MonoBehaviour
{
    public Rigidbody2D rb;
    public float knockbackTimer = 0;


    public void ApplyKnockback(Transform attacker, float angle, float delay, float force)
    {
        // Determine if attacker is to the left or right
        bool attackerOnLeft = attacker.position.x < transform.position.x;

        // Get direction based on fixed angle and attacker side
        Vector2 direction = GetKnockbackDirection(attackerOnLeft, angle);

        // Zero out existing velocity before applying knockback
        rb.linearVelocity = Vector2.zero;

        knockbackTimer = delay;

        // Apply force
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    private Vector2 GetKnockbackDirection(bool attackerOnLeft, float angleDegrees)
    {
        float angle = angleDegrees * Mathf.Deg2Rad;

        // If attacker is on the left, launch rightward (+x); else leftward (-x)
        float xDir = attackerOnLeft ? Mathf.Cos(angle) : -Mathf.Cos(angle);
        float yDir = Mathf.Sin(angle);

        Debug.Log("X Direction" + xDir);
        Debug.Log("Y Direction"+ yDir);
        Debug.Log("Applied velocity: " + rb.linearVelocity);

        return new Vector2(xDir, yDir).normalized;
    }
}
