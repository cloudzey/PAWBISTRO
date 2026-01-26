
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public LevelData currentLevel;
    public TMP_Text orderText;
    private ProductData currentOrder;
    public TMP_Text coinText;
    private int coin = 0;
    [Header("Craft - Hamburger")]
[SerializeField] private ProductData hamburgerProduct;
[SerializeField] private ProductData bunProduct;
[SerializeField] private ProductData lettuceProduct;
[SerializeField] private ProductData tomatoProduct;
[SerializeField] private ProductData pattyProduct;

[SerializeField] private HandSystem handSystem;

private bool hbBun, hbLettuce, hbTomato, hbPatty;
[SerializeField] private ProductData cheeseburgerProduct;
[SerializeField] private ProductData cheddarProduct;

private bool hbCheddar;




public ProductData CurrentOrder => currentOrder;

    private void Start()
    {
        Debug.Log("LevelManager Start çalıştı: " + gameObject.name);
        StartLevel();
    }

    void UpdateCoinUI()
{
    if (coinText != null)
        coinText.text = "Coin: " + coin;
}
public void AddCoin(int amount)
{
    coin += amount;
    UpdateCoinUI();
}

    public void StartLevel()
    {
        Debug.Log($"StartLevel() currentLevel = {(currentLevel ? currentLevel.name : "NULL")}");
        Debug.Log($"Unlocked list count = {(currentLevel != null && currentLevel.newUnlockedProducts != null ? currentLevel.newUnlockedProducts.Count : -1)}");

        UpdateCoinUI();
        CreateNewOrder();
    }

    public void CreateNewOrder()
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
    ResetCraftProgress();
   


}

private void ResetCraftProgress()
{
    hbBun = hbLettuce = hbTomato = hbPatty = hbCheddar = false;
}


public void TryServeProduct(ProductData served)
{
    if (served == null) return;

    if (CurrentOrder == served)
    {
        Debug.Log("Correct product served!");
        AddCoin(served.price > 0 ? served.price : 10);
        CreateNewOrder();
    }
    else
    {
        Debug.Log("Wrong product served!");
    }
}
public void TryServeFromHand()
{
    if (HandSystem.Instance == null)
    {
        Debug.LogError("HandSystem.Instance yok! Sahneye HandSystem objesi ekli mi?");
        return;
    }

    if (CurrentOrder == null)
    {
        Debug.LogWarning("Sipariş yok (CurrentOrder null).");
        return;
    }

    // Şimdilik tek ürün: elde kutusunda sipariş ürünü var mı?
    bool hasItem = HandSystem.Instance.HasItem(CurrentOrder);



    if (hasItem)
    {
        Debug.Log("Correct product served (from hand)!");
        AddCoin(CurrentOrder.price > 0 ? CurrentOrder.price : 10);
        HandSystem.Instance.Clear();   // elde boşalsın
        CreateNewOrder();
    }
    else
    {
        Debug.Log("Wrong product / missing item in hand!");
    }
}
public void AddHamburgerIngredient(ProductData ingredient)
{
    // Sadece hamburger/cheeseburger order'larında craft ilerlesin
    if (CurrentOrder != hamburgerProduct && CurrentOrder != cheeseburgerProduct)
    {
        Debug.Log("Bu order burger değil, ingredient sayılmadı.");
        return;
    }

    if (ingredient == bunProduct) hbBun = true;
    else if (ingredient == lettuceProduct) hbLettuce = true;
    else if (ingredient == tomatoProduct) hbTomato = true;
    else if (ingredient == pattyProduct) hbPatty = true;
    else if (ingredient == cheddarProduct) hbCheddar = true;
    else return;

    Debug.Log($"Burger progress → Bun:{hbBun} Lettuce:{hbLettuce} Tomato:{hbTomato} Patty:{hbPatty} Cheddar:{hbCheddar}");

    bool baseComplete = hbBun && hbLettuce && hbTomato && hbPatty;

    // Cheeseburger order'ında cheddar zorunlu olsun
    if (CurrentOrder == cheeseburgerProduct && baseComplete && !hbCheddar)
    {
        Debug.Log("Cheeseburger için Cheddar eksik!");
        return;
    }

    if (baseComplete)
    {
        // Cheddar eklendiyse sonuç cheeseburger
        ProductData result = hbCheddar ? cheeseburgerProduct : hamburgerProduct;

        Debug.Log($"✅ Burger tamamlandı! Sonuç: {result.displayName}");
        GiveProductToHand(result);
        ResetCraftProgress();
    }
}

private void GiveProductToHand(ProductData product)
{
    if (handSystem == null)
    {
        Debug.LogError("HandSystem referansı yok!");
        return;
    }

    handSystem.Add(product);
}





}
