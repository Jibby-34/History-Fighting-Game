using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public CharacterData[] characters;  // assign in inspector, one per grid cell
    public GridCell[] gridCells;    // references to cells, aligned with characters[]

    public int selector1Index = 0;  // current selected index by selector 1
    public int selector2Index = 0;  // current selected index by selector 2
    int gridWidth = 3;
    int gridHeight = 3;

    void Update()
    {
        // Move selector one (blue)
        if (Input.GetKeyDown(KeyCode.W)) MoveSelector1(-gridWidth); // move up one row
        if (Input.GetKeyDown(KeyCode.S)) MoveSelector1(gridWidth);  // move down one row
        if (Input.GetKeyDown(KeyCode.A)) MoveSelector1(-1);         // left
        if (Input.GetKeyDown(KeyCode.D)) MoveSelector1(1);          // right

        // Move selector two (red)
        if (Input.GetKeyDown(KeyCode.I)) MoveSelector2(-gridWidth);
        if (Input.GetKeyDown(KeyCode.K)) MoveSelector2(gridWidth);
        if (Input.GetKeyDown(KeyCode.J)) MoveSelector2(-1);
        if (Input.GetKeyDown(KeyCode.L)) MoveSelector2(1);
    }

    void MoveSelector1(int offset)
    {
        int newIndex = Mathf.Clamp(selector1Index + offset, 0, characters.Length - 1);
        UpdateSelectorPosition(1, newIndex);
    }

    void MoveSelector2(int offset)
    {
        int newIndex = Mathf.Clamp(selector2Index + offset, 0, characters.Length - 1);
        UpdateSelectorPosition(2, newIndex);
    }

    void UpdateSelectorPosition(int selectorId, int newIndex)
    {
        if (selectorId == 1)
        {
            // Clear old cell state
            gridCells[selector1Index].isSelector1Here = false;
            gridCells[selector1Index].UpdateSprite();

            selector1Index = newIndex;

            // Set new cell state
            gridCells[selector1Index].isSelector1Here = true;
            gridCells[selector1Index].UpdateSprite();
        }
        else if (selectorId == 2)
        {
            gridCells[selector2Index].isSelector2Here = false;
            gridCells[selector2Index].UpdateSprite();

            selector2Index = newIndex;

            gridCells[selector2Index].isSelector2Here = true;
            gridCells[selector2Index].UpdateSprite();
        }
    }
}

