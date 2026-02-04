using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PawBistro/Level")]
public class LevelData : ScriptableObject
{
    public int levelIndex = 1;
    public int targetGold = 100;

    [Header("Chain")]
    public LevelData previousLevel;

    [Header("Only NEW products for this level")]
    public List<ProductData> newUnlockedProducts = new List<ProductData>();
}
