using UnityEngine;
using UnityEngine.UI;

public class PizzaSpriteSwapController : MonoBehaviour
{
    [Header("UI")]
    public Image pizzaStageImage;

    [Header("Stage Sprites")]
    public Sprite[] margaritaStages;   // dough, sauce, cheese
    public Sprite[] pepperoniStages;   // dough, sauce, cheese, pepperoni
    public Sprite[] mixedStages;       // dough, sauce, cheese, tomato, pepperoni, pepper

    public enum PizzaType { Margarita, Pepperoni, Mixed }
    public enum IngredientType { Dough, Sauce, Cheese, Tomato, Pepperoni, Pepper }

    private PizzaType activePizza;
    private int stepIndex = -1;

    private IngredientType[] activeSteps;
    private Sprite[] activeStageSprites;

    void Start()
    {
        // Test için default:
        StartRecipe(PizzaType.Margarita);
    }

    public void StartRecipe(PizzaType type)
    {
        activePizza = type;
        stepIndex = -1;

        switch (activePizza)
        {
            case PizzaType.Margarita:
                activeSteps = new[] { IngredientType.Dough, IngredientType.Sauce, IngredientType.Cheese };
                activeStageSprites = margaritaStages;
                break;

            case PizzaType.Pepperoni:
                activeSteps = new[] { IngredientType.Dough, IngredientType.Sauce, IngredientType.Cheese, IngredientType.Pepperoni };
                activeStageSprites = pepperoniStages;
                break;

            case PizzaType.Mixed:
                activeSteps = new[] { IngredientType.Dough, IngredientType.Sauce, IngredientType.Cheese, IngredientType.Tomato, IngredientType.Pepperoni, IngredientType.Pepper };
                activeStageSprites = mixedStages;
                break;
        }

        // Başlangıçta görsel boş kalsın:
        if (pizzaStageImage != null) pizzaStageImage.sprite = null;
    }

    public void PressIngredient(IngredientType pressed)
    {
        int nextIndex = stepIndex + 1;
        if (nextIndex < 0 || nextIndex >= activeSteps.Length) return;

        // Sıradaki beklenen malzeme bu mu?
        if (pressed != activeSteps[nextIndex])
        {
            Debug.Log($"Wrong ingredient. Expected: {activeSteps[nextIndex]}, Pressed: {pressed}");
            return;
        }

        stepIndex = nextIndex;

        // Sprite değiştir
        if (activeStageSprites != null && stepIndex >= 0 && stepIndex < activeStageSprites.Length)
            pizzaStageImage.sprite = activeStageSprites[stepIndex];

        // Tamamlandı mı?
        if (stepIndex == activeSteps.Length - 1)
            Debug.Log("Pizza completed!");
    }

    // Eğer enum parametre dropdown çıkmazsa diye wrapper'lar:
    public void PressDough() => PressIngredient(IngredientType.Dough);
    public void PressSauce() => PressIngredient(IngredientType.Sauce);
    public void PressCheese() => PressIngredient(IngredientType.Cheese);
    public void PressTomato() => PressIngredient(IngredientType.Tomato);
    public void PressPepperoni() => PressIngredient(IngredientType.Pepperoni);
    public void PressPepper() => PressIngredient(IngredientType.Pepper);
}

