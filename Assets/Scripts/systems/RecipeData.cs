using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pawbistro/Items/Recipe")]
public class RecipeData : ScriptableObject
{
    public BaseItemData baseItem;
    public List<AddOnData> requiredAddOns = new();
    public ProductData resultFinalProduct;
}