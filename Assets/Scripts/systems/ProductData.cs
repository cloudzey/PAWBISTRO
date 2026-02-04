using UnityEngine;

public enum ProductType
{
    Auto,
    Manual
}

[CreateAssetMenu(menuName = "PawBistro/Product")]
public class ProductData : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite icon;
    public int price = 10;
    public ProductType type;
}
