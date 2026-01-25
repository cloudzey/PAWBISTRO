using UnityEngine;

public enum ScreenType { MainCounter, FastFoodCounter, KitchenManual, DrinksMenu,CoffeeMenu,SyrupMenu }

public class UIManager : MonoBehaviour
{
    public GameObject panelMain;
    public GameObject panelFastFood;
    public GameObject panelKitchen;
    public GameObject panelDrinksMenu;
    public GameObject panelCoffeeMenu;
    public GameObject panelSyrupMenu;
    
    private ScreenType lastScreenBeforeOverlay;


    public ScreenType CurrentScreen { get; private set; }

    private void Start()
    {
        Show(ScreenType.MainCounter);
    }
    private bool IsOverlay(ScreenType s) =>
    s == ScreenType.DrinksMenu || s == ScreenType.CoffeeMenu || s == ScreenType.SyrupMenu;

    public void Show(ScreenType screen)
{
    CurrentScreen = screen;

    // Eğer overlay açıyorsak, arkada hangi ekran kalacak?
    ScreenType baseScreen = IsOverlay(screen) ? lastScreenBeforeOverlay : screen;

    // Base paneller (ARKADA KALACAK EKRAN)
    panelMain.SetActive(baseScreen == ScreenType.MainCounter);
    panelFastFood.SetActive(baseScreen == ScreenType.FastFoodCounter);
    panelKitchen.SetActive(baseScreen == ScreenType.KitchenManual);

    // Overlay paneller (POPUP MENÜLER)
    panelDrinksMenu.SetActive(screen == ScreenType.DrinksMenu);
    panelCoffeeMenu.SetActive(screen == ScreenType.CoffeeMenu);
    panelSyrupMenu.SetActive(screen == ScreenType.SyrupMenu);
}
    public void ToggleDrinksMenu()
{
    if (CurrentScreen == ScreenType.DrinksMenu)
    {
        Show(lastScreenBeforeOverlay);
    }
    else
    {
        lastScreenBeforeOverlay = CurrentScreen;
        Show(ScreenType.DrinksMenu);
    }
}
public void ToggleCoffeeMenu()
{
    if (CurrentScreen == ScreenType.CoffeeMenu)
    {
        Show(lastScreenBeforeOverlay);
    }
    else
    {
        lastScreenBeforeOverlay = CurrentScreen;
        Show(ScreenType.CoffeeMenu);
    }
}
public void ToggleSyrupMenu()
{
    if (CurrentScreen == ScreenType.SyrupMenu)
    {
        Show(lastScreenBeforeOverlay);
    }
    else
    {
        lastScreenBeforeOverlay = CurrentScreen;
        Show(ScreenType.SyrupMenu);
    }
}



    // Butonlara kolay ba�lamak i�in:
    public void ShowMain() => Show(ScreenType.MainCounter);
    public void ShowFastFood() => Show(ScreenType.FastFoodCounter);
    public void ShowKitchen() => Show(ScreenType.KitchenManual);
    public void ShowDrinksMenu() => Show(ScreenType.DrinksMenu);
    public void ShowSyrupMenu()  => Show(ScreenType.SyrupMenu);

}
