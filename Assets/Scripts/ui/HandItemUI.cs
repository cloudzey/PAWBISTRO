using UnityEngine;
using UnityEngine.UI;

public class HandItemUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Button removeButton;

    public void Bind(ProductData product, System.Action onRemove)
    {
        if (product == null) return;

        Debug.Log($"[HandItemUI] Setting icon for {product.displayName} => {(product.icon ? product.icon.name : "NULL")} / iconImage={(iconImage ? iconImage.name : "NULL")}");

        if (iconImage != null)
        {
            iconImage.sprite = product.icon;
            iconImage.enabled = (product.icon != null);
            iconImage.preserveAspect = true;
            iconImage.color = Color.white; // bazen alpha 0 olabiliyor diye garanti
        }

        if (removeButton != null)
        {
            removeButton.onClick.RemoveAllListeners();
            removeButton.onClick.AddListener(() => onRemove?.Invoke());
        }
    }
}
