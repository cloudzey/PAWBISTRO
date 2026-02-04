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
    [Header("Identity")]
    public string id;
    public string displayName;

    [Header("Category")]
    public ItemCategory category;

    [Header("Visuals")]
    public Sprite icon;

    [Header("Gameplay")]
    public int basePrice;
}
