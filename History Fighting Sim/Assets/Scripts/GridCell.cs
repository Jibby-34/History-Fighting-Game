using UnityEngine;

public class GridCell : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite selector1Sprite;
    public Sprite selector2Sprite;
    public Sprite bothSelectorsSprite;
    public Sprite defaultSprite;

    public bool isSelector1Here = false;
    public bool isSelector2Here = false;

    public void UpdateSprite()
    {
        if (isSelector1Here && isSelector2Here)
            spriteRenderer.sprite = bothSelectorsSprite;
        else if (isSelector1Here)
            spriteRenderer.sprite = selector1Sprite;
        else if (isSelector2Here)
            spriteRenderer.sprite = selector2Sprite;
        else
            spriteRenderer.sprite = defaultSprite;
    }
}
