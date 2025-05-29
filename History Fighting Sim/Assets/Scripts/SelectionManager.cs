using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class SelectionManager : MonoBehaviour
{
    public CharacterData[] characters;  // assign in inspector, one per grid cell
    public GridCell[] gridCells;    // references to cells, aligned with characters[]

    public int selector1Index = 0;  // current selected index by selector 1
    public int selector2Index = 0;  // current selected index by selector 2
    int gridWidth = 3;
    int gridHeight = 3;
    public TextMeshProUGUI player1Ready;
    public TextMeshProUGUI player2Ready;
    bool player1Selected = false;
    bool player2Selected = false;
    public SelectedCharacter player1SelectedCharacter;
    public SelectedCharacter player2SelectedCharacter;

    void Start()
    {
        MoveSelector1(0);
        MoveSelector2(1);
    }

    void Update()
    {
        if (!player1Selected)
        {
            // Move selector one (blue)
            if (Input.GetKeyDown(KeyCode.W)) MoveSelector1(-gridWidth); // move up one row
            if (Input.GetKeyDown(KeyCode.S)) MoveSelector1(gridWidth);  // move down one row
            if (Input.GetKeyDown(KeyCode.A)) MoveSelector1(-1);         // left
            if (Input.GetKeyDown(KeyCode.D)) MoveSelector1(1);          // right
        }

        if (!player2Selected)
        {
            // Move selector two (red)
            if (Input.GetKeyDown(KeyCode.I)) MoveSelector2(-gridWidth);
            if (Input.GetKeyDown(KeyCode.K)) MoveSelector2(gridWidth);
            if (Input.GetKeyDown(KeyCode.J)) MoveSelector2(-1);
            if (Input.GetKeyDown(KeyCode.L)) MoveSelector2(1);
        }

        if (Input.GetKeyDown(KeyCode.U)) SelectCharacter(selector2Index, 2);
        if (Input.GetKeyDown(KeyCode.E)) SelectCharacter(selector1Index, 1);

        if (Input.GetKeyDown(KeyCode.H)) UnselectCharacter(2);
        if (Input.GetKeyDown(KeyCode.F)) UnselectCharacter(1);
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

    public void SelectCharacter(int index, int player)
    {
        if (player == 1)
        {
            player1Ready.text = "Ready";
            player1Ready.color = new Color(0.217556f, 0.9372549f, 0.1607843f);
            player1SelectedCharacter.selectedCharacter = characters[index];
            player1Selected = true;
        }
        else
        {
            player2Ready.text = "Ready";
            player2Ready.color = new Color(0.217556f, 0.9372549f, 0.1607843f);
            player2SelectedCharacter.selectedCharacter = characters[index];
            player2Selected = true;
        }

        if (player1Selected && player2Selected)
        {
            SceneManager.LoadScene("MainScene");
        }
    }
    
    public void UnselectCharacter(int player)
    {
        if (player == 1)
        {
            player1Ready.text = "Not Ready";
            player1Ready.color = new Color(0.9371068f, 0.1620781f, 0.1620781f); 
            player1Selected = false;
        }
        else
        {
            player2Ready.text = "Not Ready";
            player2Ready.color = new Color(0.9371068f, 0.1620781f, 0.1620781f);
            player2Selected = false;
        }
    }
}

