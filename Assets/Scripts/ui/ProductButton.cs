using UnityEngine;
using UnityEngine.UI;

public class ProductButton : MonoBehaviour
{
    [Header("Data")]
    public ProductData product;

    [Header("UI")]
    [SerializeField] private Image iconImage;

    private void Start()
    {
        RefreshUI();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        RefreshUI();
    }
#endif

    private void RefreshUI()
    {
        if (iconImage == null) return;

        if (product != null && product.icon != null)
        {
            iconImage.sprite = product.icon;
            iconImage.enabled = true;
        }
        else
        {
            iconImage.sprite = null;
            iconImage.enabled = false;
        }
    }

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
