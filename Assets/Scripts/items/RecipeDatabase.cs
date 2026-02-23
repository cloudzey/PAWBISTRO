using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pawbistro/Items/Recipe Database")]
public class RecipeDatabase : ScriptableObject
{
    public List<RecipeData> recipes = new();
}