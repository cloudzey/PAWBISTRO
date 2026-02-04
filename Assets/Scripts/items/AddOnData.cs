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
public class AddOnData : ScriptableObject
{
    [Header("Identity")]
    public string id;              // ice, vanilla, cheddar
    public string displayName;     // Ice, Vanilla Syrup, Cheddar

    [Header("Rules")]
    public AddOnType type;

    // Bu addon hangi kategorilerde kullanýlabilir?
    public ItemCategory[] allowedCategories;

    [Header("Visuals")]
    public Sprite icon;

    [Header("Gameplay")]
    public int priceIncrease;
}
