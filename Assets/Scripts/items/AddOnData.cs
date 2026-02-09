using UnityEngine;

public enum AddOnType
{
    Ice,
    Syrup,
    Topping,
    Sauce,
    Extra
}

[CreateAssetMenu(menuName = "Pawbistro/Items/Add On")]
public class AddOnData : ProductData
{
    [Header("AddOn Rules")]
    public AddOnType addonType;

    public ItemCategory[] allowedCategories;

    [Header("Gameplay")]
    public int priceIncrease;
}