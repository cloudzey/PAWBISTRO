using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandItemUI : MonoBehaviour
{
    [SerializeField] TMP_Text title;
    [SerializeField] Button removeButton; // X butonu

    public void Bind(ProductData product, System.Action onRemove)
    {
        title.text = product.displayName;

        if (removeButton != null)
        {
            removeButton.onClick.RemoveAllListeners();
            removeButton.onClick.AddListener(() => onRemove?.Invoke());
        }
    }
}
