using UnityEngine;
using System.Collections.Generic;

public class ProductPanelPopulator : MonoBehaviour
{
    [Header("UI")]
    public Transform contentParent;
    public GameObject buttonPrefab;

    [Header("Logic")]
    public LevelManager levelManager;
    public List<ProductData> productsToShow;

    private void Start()
    {
        Populate();
    }

    public void Populate()
    {
        Debug.Log($"{contentParent.name}: Populate -> {productsToShow.Count} ürün basýlýyor");

        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        foreach (var product in productsToShow)
        {
            GameObject btnGO = Instantiate(buttonPrefab, contentParent);
            btnGO.transform.localScale = Vector3.one;

            ProductButton pb = btnGO.GetComponent<ProductButton>();
            pb.product = product;
            pb.levelManager = levelManager;
        }
    }
}
