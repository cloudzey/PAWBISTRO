using UnityEngine;
using UnityEngine.UI;

public class ProductButton : MonoBehaviour
{
    public ProductData product;
    public LevelManager levelManager;

    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (levelManager != null && product != null)
            levelManager.TryServeProduct(product);
    }
}
