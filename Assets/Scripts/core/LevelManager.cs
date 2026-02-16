
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public LevelData currentLevel;
    public TMP_Text orderText;
    private ProductData currentOrder;
    public TMP_Text coinText;
    private int coin = 0;
[Header("Level Progress UI")]
public TextMeshProUGUI levelText;
public Image expFill;

[Header("Levels (L1..L10)")]
public List<LevelData> levels = new List<LevelData>();

private int currentLevelIdx = 0; // levels listesinde index

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
[Header("Craft - Hotdog")]
[SerializeField] private ProductData hotdogProduct;
[SerializeField] private ProductData hotdogBunProduct;
[SerializeField] private ProductData sausageProduct;

private bool hdBun, hdSausage;





public ProductData CurrentOrder => currentOrder;

    private void Start()
    {
        Debug.Log("LevelManager Start çalıştı: " + gameObject.name);
          // Level listesi boşsa patlamasın
    if (levels != null && levels.Count > 0)
    {
        // levelIndex'e göre sırala (L1, L2, L3...)
        levels = levels.OrderBy(l => l.levelIndex).ToList();

        // Başlangıç: L1
        currentLevelIdx = 0;
        currentLevel = levels[currentLevelIdx];
    }
        StartLevel();
        UpdateLevelUI();
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
     CheckLevelUp();   // coin hedefi geçti mi?
    UpdateLevelUI();  // bar + Level yazısı güncellensin
}

    public void StartLevel()
    {
        Debug.Log($"StartLevel() currentLevel = {(currentLevel ? currentLevel.name : "NULL")}");
        Debug.Log($"Unlocked list count = {(currentLevel != null && currentLevel.newUnlockedProducts != null ? currentLevel.newUnlockedProducts.Count : -1)}");

        UpdateCoinUI();
        CreateNewOrder();
    }
    void CheckLevelUp()
{
    if (levels == null || levels.Count == 0) return;
    if (currentLevel == null) return;

    // Büyük para gelirse (ör: +500) birden fazla level atlayabilsin
    while (currentLevelIdx < levels.Count - 1 && coin >= currentLevel.targetGold)
    {
        currentLevelIdx++;
        currentLevel = levels[currentLevelIdx];

        StartLevel(); // senin mevcut unlock/order akışın burada
    }
}
void UpdateLevelUI()
{
    if (currentLevel == null) return;

    if (levelText != null)
        levelText.text = "Level: " + currentLevel.levelIndex;

    if (expFill != null)
    {
        float progress = (float)coin / currentLevel.targetGold;
        expFill.fillAmount = Mathf.Clamp01(progress);
    }
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
    hdBun = hdSausage = false;
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
public void AddHotdogIngredient(ProductData ingredient)
{
    if (CurrentOrder != hotdogProduct)
    {
        Debug.Log("Bu order hotdog değil, ingredient sayılmadı.");
        return;
    }

    if (ingredient == hotdogBunProduct) hdBun = true;
    else if (ingredient == sausageProduct) hdSausage = true;
    else return;

    Debug.Log($"Hotdog progress → Bun:{hdBun} Sausage:{hdSausage}");

    if (hdBun && hdSausage)
    {
        Debug.Log("🌭 Hotdog tamamlandı! Elde ürüne ekleniyor.");
        GiveProductToHand(hotdogProduct);
        hdBun = hdSausage = false; // sadece hotdog kısmını resetledik
    }
}






}
