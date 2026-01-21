using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelData currentLevel;

    private void Start()
    {
        StartLevel();
    }

    public void StartLevel()
    {
        Debug.Log("LEVEL STARTED: " + (currentLevel != null ? currentLevel.levelIndex : -1));
    }
}
