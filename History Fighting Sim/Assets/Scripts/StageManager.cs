using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class StageManager : MonoBehaviour
{
    public StageData[] stages;  // assign in inspector, one per grid cell
    public GridCell[] gridCells;    // references to cells, aligned with stages[]

    public int selectorIndex = 0;  // current selected index by selector 1
    int gridWidth = 2;
    int gridHeight = 2;
    public TextMeshProUGUI stageText;
    bool stageSelected = false;
    public SelectedStage selectedStage;

    void Start()
    {
        for (int i = 0; i < gridCells.Length; i++)
        {
            gridCells[i].spriteRenderer.color = new Color(1f, 1f, 1f, 0.4f);
        }
        MoveSelector(0);
    }

    void Update()
    {
        if (!stageSelected)
        {
            // Move selector one (blue)
            if (Input.GetKeyDown(KeyCode.W)) MoveSelector(-gridWidth); // move up one row
            if (Input.GetKeyDown(KeyCode.S)) MoveSelector(gridWidth);  // move down one row
            if (Input.GetKeyDown(KeyCode.A)) MoveSelector(-1);         // left
            if (Input.GetKeyDown(KeyCode.D)) MoveSelector(1);          // right
        }

        if (Input.GetKeyDown(KeyCode.E)) SelectStage(selectorIndex, 1);
    }

    void MoveSelector(int offset)
    {
        int newIndex = Mathf.Clamp(selectorIndex + offset, 0, stages.Length - 1);
        UpdateSelectorPosition(1, newIndex);
    }

    void UpdateSelectorPosition(int selectorId, int newIndex)
    {
        // Clear old cell state
        gridCells[selectorIndex].isSelector1Here = false;
        gridCells[selectorIndex].spriteRenderer.color = new Color(1f, 1f, 1f, 0.4f);

        selectorIndex = newIndex;

        // Set new cell state
        gridCells[selectorIndex].isSelector1Here = true;
        gridCells[selectorIndex].spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        stageText.text = stages[selectorIndex].stageName;
    }

    public void SelectStage(int index, int player)
    {
        stageText.color = new Color(0.217556f, 0.9372549f, 0.1607843f);
        selectedStage.selectedStage = stages[index];
        stageSelected = true;
        SceneManager.LoadScene("CharacterSelect");
    }
}

