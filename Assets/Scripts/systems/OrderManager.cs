using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HandSystem handSystem;
    [SerializeField] private CustomerController customerPrefab;
    [SerializeField] private Transform customerSpotA;
    [SerializeField] private Transform customerSpotB;

    [Header("Order Pool (Final Products Only)")]
    [SerializeField] private List<ProductData> finalProducts;

    private CustomerController customerA;
    private CustomerController customerB;

    private void Start()
    {
        SpawnCustomersAndOrders();
    }

    public void SpawnCustomersAndOrders()
    {
        ClearCustomers();

        if (finalProducts == null || finalProducts.Count == 0)
        {
            Debug.LogError("Final products listesi boþ! Order gelmez.");
            return;
        }

        if (customerPrefab == null)
        {
            Debug.LogError("Customer Prefab boþ!");
            return;
        }

        if (customerSpotA == null || customerSpotB == null)
        {
            Debug.LogError("CustomerSpotA / CustomerSpotB boþ!");
            return;
        }

        int customerCount = Random.value < 0.5f ? 1 : 2;

        List<ProductData> pool = new List<ProductData>(finalProducts);

        ProductData order1 = GetRandomProduct(pool);
        customerA = SpawnCustomerAtSpot(customerSpotA);
        customerA.Init(order1, this);

        if (customerCount == 2)
        {
            ProductData order2 = GetRandomProduct(pool);
            customerB = SpawnCustomerAtSpot(customerSpotB);
            customerB.Init(order2, this);
        }
    }

    private CustomerController SpawnCustomerAtSpot(Transform spot)
    {
        var c = Instantiate(customerPrefab, spot);

        c.transform.localPosition = Vector3.zero;
        c.transform.localRotation = Quaternion.identity;
        c.transform.localScale = Vector3.one;

        if (c.transform is RectTransform rt)
        {
            rt.anchoredPosition = Vector2.zero;
            rt.localRotation = Quaternion.identity;
            rt.localScale = Vector3.one;
        }

        return c;
    }

    private ProductData GetRandomProduct(List<ProductData> pool)
    {
        int i = Random.Range(0, pool.Count);
        ProductData picked = pool[i];
        pool.RemoveAt(i);
        return picked;
    }

    private void ClearCustomers()
    {
        if (customerA != null) Destroy(customerA.gameObject);
        if (customerB != null) Destroy(customerB.gameObject);

        customerA = null;
        customerB = null;
    }

    public void OnCustomerClicked(CustomerController customer)
    {
        if (handSystem == null)
        {
            Debug.LogError("OrderManager: HandSystem referansý yok!");
            return;
        }

        ProductData held = handSystem.FirstOrNull;
        if (held == null) return;

        bool served = customer.TryServe(held);
        if (served)
        {
            handSystem.Clear();

            if (AllServed())
                SpawnCustomersAndOrders();
        }
        else
        {
            Debug.Log("Yanlýþ ürün!");
        }
    }

    private bool AllServed()
    {
        bool aOk = (customerA == null) || customerA.IsServed;
        bool bOk = (customerB == null) || customerB.IsServed;
        return aOk && bOk;
    }
}
