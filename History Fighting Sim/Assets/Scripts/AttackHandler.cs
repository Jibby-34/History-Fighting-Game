using UnityEngine;
using System.Collections;


public class AttackHandler : MonoBehaviour
{
    public IEnumerator SlideForwardDuringAttack(float distance, float duration)
    {
        float elapsed = 0f;
        float direction = Mathf.Sign(transform.localScale.x);
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + new Vector3(direction * distance, 0f, 0f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            // Lerp the position smoothly
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
    }
}
