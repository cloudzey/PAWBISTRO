using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HandSystem : MonoBehaviour
{
    public static HandSystem Instance { get; private set; }

    [Header("Recipe")]
    [SerializeField] private RecipeDatabase recipeDb;

    private BaseItemData currentBase;
    private readonly List<AddOnData> currentAddOns = new();

    public ProductData CurrentFinal { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void Add(ProductData item)
    {
        if (item == null) return;

        if (item is BaseItemData b)
        {
            currentBase = b;
            currentAddOns.Clear();
        }
        else if (item is AddOnData a)
        {
            if (!currentAddOns.Contains(a))
                currentAddOns.Add(a);
        }
        else
        {
            CurrentFinal = item;
            return;
        }

        Resolve();
    }

    public void Clear()
    {
        currentBase = null;
        currentAddOns.Clear();
        CurrentFinal = null;
    }

    private void Resolve()
    {
        CurrentFinal = null;

        if (currentBase == null) return;

        if (currentAddOns.Count == 0)
        {
            CurrentFinal = currentBase;
            return;
        }

        if (recipeDb == null || recipeDb.recipes == null) return;

        foreach (var r in recipeDb.recipes)
        {
            if (r == null) continue;
            if (r.baseItem != currentBase) continue;

            if (SameAddOnSet(r.requiredAddOns, currentAddOns))
            {
                CurrentFinal = r.resultFinalProduct;
                return;
            }
        }
        CurrentFinal = null;
    }

    private bool SameAddOnSet(List<AddOnData> need, List<AddOnData> have)
    {
        if (need == null) need = new List<AddOnData>();
        if (have == null) have = new List<AddOnData>();

        if (need.Count != have.Count) return false;
        return !need.Except(have).Any();
    }

    public bool HasItem()
    {
        return CurrentFinal != null;
    }
    public ProductData Current => CurrentFinal;
    public ProductData FirstOrNull => CurrentFinal;
    public bool HasItem(ProductData item)
    {
        return CurrentFinal != null && CurrentFinal == item;
    }


}
