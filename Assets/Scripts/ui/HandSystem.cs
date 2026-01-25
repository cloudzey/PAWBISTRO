using System.Collections.Generic;
using UnityEngine;

public class HandSystem : MonoBehaviour
{
    public static HandSystem Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private Transform handContent;      // Hand_Content (RectTransform da olabilir)
    [SerializeField] private HandItemUI handItemPrefab;  // PF_HandItem (üstünde HandItemUI scripti olan prefab)

    // Elimizdeki item listesi (dışarıdan değiştirilmesin diye private)
    private readonly List<ProductData> Items = new();

    // Dışarıya sadece okunur liste veriyoruz
    public IReadOnlyList<ProductData> CurrentItems => Items;

    public int Count => Items.Count;

    public ProductData FirstOrNull => (Items.Count > 0) ? Items[0] : null;

    public bool HasItem(ProductData product)
    {
        if (product == null) return false;
        return Items.Contains(product);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
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

    private void RefreshUI()
    {
        // Debug için: eksik referans varsa anında yakalayalım
        if (handContent == null)
        {
            Debug.LogError("[HandSystem] handContent NULL! Inspector'da Hand_Content atadın mı?");
            return;
        }

        if (handItemPrefab == null)
        {
            Debug.LogError("[HandSystem] handItemPrefab NULL! Inspector'da PF_HandItem prefabını atadın mı?");
            return;
        }

        // Öncekileri temizle
        for (int i = handContent.childCount - 1; i >= 0; i--)
            Destroy(handContent.GetChild(i).gameObject);

        // Yenileri bas
        for (int i = 0; i < Items.Count; i++)
        {
            int captured = i;
            var ui = Instantiate(handItemPrefab, handContent);
            ui.Bind(Items[i], () => RemoveAt(captured));
        }
    }
}
