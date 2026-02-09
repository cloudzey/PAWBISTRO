using UnityEngine;

public enum ItemCategory
{
    Drink,
    FastFood,
    Pizza,
}

[CreateAssetMenu(menuName = "Pawbistro/Items/Base Item")]
public class BaseItemData : ProductData
{
    [Header("Category")]
    public ItemCategory category;

    [Header("Gameplay")]
    public int basePrice;
}