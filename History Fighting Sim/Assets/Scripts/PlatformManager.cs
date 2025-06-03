using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public SpriteRenderer background;

    void Start()
    {

        for (int i = 0; i < GameData.selectedStage.platformPositions.Length; i++)
        {
            GameObject rect = new GameObject("Platform" + i);
            rect.transform.position = GameData.selectedStage.platformPositions[i];
            SpriteRenderer sr = rect.AddComponent<SpriteRenderer>();
            sr.sprite = GameData.selectedStage.platformSprite;
            sr.color = GameData.selectedStage.platformColor;

            rect.transform.localScale = GameData.selectedStage.platformSizes[i];

            BoxCollider2D collider = rect.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(4, 1);
            collider.offset = Vector2.zero;
            rect.layer = LayerMask.NameToLayer("Ground");
            sr.sortingLayerName = "Background";
        }
        background.sprite = GameData.selectedStage.backgroundSprite;
    }
}
