using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PawBistro/Level")]
public class LevelData : ScriptableObject
{
    public int levelIndex = 1;
    public int targetGold = 100;

    public List<ProductData> unlockedProducts = new List<ProductData>();
}
