using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public SelectedStage selectedStage;
    public Sprite platformSprite;
    public SpriteRenderer background;

    void Start()
    {
        for (int i = 0; i < selectedStage.selectedStage.platformPositions.Length; i++)
        {
            GameObject rect = new GameObject("Platform" + i);
            rect.transform.position = selectedStage.selectedStage.platformPositions[i];
            SpriteRenderer sr = rect.AddComponent<SpriteRenderer>();
            sr.sprite = platformSprite;
            sr.color = selectedStage.selectedStage.platformColor;

            rect.transform.localScale = selectedStage.selectedStage.platformSizes[i];

            BoxCollider2D collider = rect.AddComponent<BoxCollider2D>();
            collider.size = Vector2.one;
            collider.offset = Vector2.zero;
            rect.layer = LayerMask.NameToLayer("Ground");
            sr.sortingLayerName = "Background";
        }
        background.sprite = selectedStage.selectedStage.backgroundSprite;
    }
}
