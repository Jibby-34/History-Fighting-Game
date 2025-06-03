public static class GameData
{
    public static CharacterData player1Character;
    public static CharacterData player2Character;
    public static StageData selectedStage;

    // Example method to reset data
    public static void ResetData()
    {
        player1Character = null;
        player2Character = null;
        selectedStage = null;

    }
}
