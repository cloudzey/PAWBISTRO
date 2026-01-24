using System.Collections.Generic;
using UnityEngine;

public class HandSystem : MonoBehaviour
{
    public static HandSystem Instance;

    [SerializeField] Transform handContent;
    [SerializeField] HandItemUI handItemPrefab;

    public readonly List<ProductData> Items = new();

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void Add(ProductData product)
    {
        if (product == null) return;
        Items.Add(product);
        RefreshUI();
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= Items.Count) return;
        Items.RemoveAt(index);
        RefreshUI();
    }

    public void Clear()
    {
        Items.Clear();
        RefreshUI();
    }

    void RefreshUI()
    {
        for (int i = handContent.childCount - 1; i >= 0; i--)
            Destroy(handContent.GetChild(i).gameObject);

        for (int i = 0; i < Items.Count; i++)
        {
            int captured = i;
            var ui = Instantiate(handItemPrefab, handContent);
            ui.Bind(Items[i], () => RemoveAt(captured));
        }
    }
}

