using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject drinksMenu;
    [SerializeField] GameObject coffeeMenu;

    public void ToggleDrinks()
    {
        bool newState = !drinksMenu.activeSelf;
        drinksMenu.SetActive(newState);
        if (newState) coffeeMenu.SetActive(false);
    }

    public void ToggleCoffee()
    {
        bool newState = !coffeeMenu.activeSelf;
        coffeeMenu.SetActive(newState);
        if (newState) drinksMenu.SetActive(false);
    }

    public void CloseAll()
    {
        drinksMenu.SetActive(false);
        coffeeMenu.SetActive(false);
    }
}
