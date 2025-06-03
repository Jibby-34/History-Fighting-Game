using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
public class StageData : ScriptableObject
{
    public string stageName = "Blank";
    public Vector2[] platformPositions;
    public Vector2[] platformSizes;
    public Color platformColor;
    public Sprite backgroundSprite;
    public Sprite platformSprite;
}
