using UnityEngine;
using UnityEngine.UI;

public class ProductButton : MonoBehaviour
{
    public ProductData product;
    public LevelManager levelManager;

    public void OnClick()
    {
        if (product == null || levelManager == null)
        {
            Debug.LogError("ProductButton: product veya levelManager NULL");
            return;
        }

        levelManager.TryServeProduct(product);
    }
}
