using UnityEngine;

public class ProductButton : MonoBehaviour
{
    public ProductData product;

    public void OnClick()
    {
        if (product == null)
        {
            Debug.LogError("ProductButton: product NULL");
            return;
        }

        if (HandSystem.Instance == null)
        {
            Debug.LogError("ProductButton: HandSystem.Instance NULL (sahnede HandSystem objesi var mı?)");
            return;
        }

        HandSystem.Instance.Add(product);
        Debug.Log($"Hand'e eklendi: {product.displayName}");
    }
}
