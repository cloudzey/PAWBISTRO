
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public LevelData currentLevel;
    public TMP_Text orderText;
    private ProductData currentOrder;

    private void Start()
    {
        StartLevel();
    }

    public void StartLevel()
    {
        Debug.Log("LEVEL STARTED: " + (currentLevel != null ? currentLevel.levelIndex : -1));
         CreateNewOrder();
    }
   void CreateNewOrder()
{
    if (currentLevel == null || currentLevel.newUnlockedProducts == null || currentLevel.newUnlockedProducts.Count == 0)
    {
        Debug.LogWarning("No unlocked products in this level!");
        if (orderText != null) orderText.text = "Order: (none)";
        return;
    }

    int r = Random.Range(0, currentLevel.newUnlockedProducts.Count);
    currentOrder = currentLevel.newUnlockedProducts[r];

    if (orderText != null)
        orderText.text = "Order: " + currentOrder.displayName;

    Debug.Log("NEW ORDER: " + currentOrder.displayName);
}
public void TryServeProduct(ProductData served)
{
    if (currentOrder == null) return;

    if (served == currentOrder)
    {
        Debug.Log("✅ Correct product served!");
        CreateNewOrder();
    }
    else
    {
        Debug.Log("❌ Wrong product served!");
    }
}
}
