using UnityEngine;

public enum ScreenType { MainCounter, FastFoodCounter, KitchenManual }

public class UIManager : MonoBehaviour
{
    public GameObject panelMain;
    public GameObject panelFastFood;
    public GameObject panelKitchen;

    public ScreenType CurrentScreen { get; private set; }

    private void Start()
    {
        Show(ScreenType.MainCounter);
    }

    public void Show(ScreenType screen)
    {
        CurrentScreen = screen;

        panelMain.SetActive(screen == ScreenType.MainCounter);
        panelFastFood.SetActive(screen == ScreenType.FastFoodCounter);
        panelKitchen.SetActive(screen == ScreenType.KitchenManual);
    }

    // Butonlara kolay baðlamak için:
    public void ShowMain() => Show(ScreenType.MainCounter);
    public void ShowFastFood() => Show(ScreenType.FastFoodCounter);
    public void ShowKitchen() => Show(ScreenType.KitchenManual);
}
