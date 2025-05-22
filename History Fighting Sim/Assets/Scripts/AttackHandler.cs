using UnityEngine;
using System.Collections;

public class AttackHandler : MonoBehaviour
{
    public IEnumerator SlideForwardDuringAttack(float distance, float duration, int facingDirection)
    {
        float elapsed = 0f;
        float direction = Mathf.Sign(facingDirection);
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + new Vector3(direction * distance, 0f, 0f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
    }
}
