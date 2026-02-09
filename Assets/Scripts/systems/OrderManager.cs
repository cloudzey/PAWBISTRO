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

        int customerCount = Random.value < 0.5f ? 1 : 2;

        if (finalProducts == null || finalProducts.Count == 0)
        {
            Debug.LogError("Final Products listesi boþ! Order gelmez.");
            return;
        }

        List<ProductData> pool = new List<ProductData>(finalProducts);

        ProductData order1 = GetRandomProduct(pool);
        customerA = Instantiate(customerPrefab, customerSpotA.position, Quaternion.identity);
        customerA.Init(order1, this);

        if (customerCount == 2)
        {
            ProductData order2 = GetRandomProduct(pool);
            customerB = Instantiate(customerPrefab, customerSpotB.position, Quaternion.identity);
            customerB.Init(order2, this);
        }
    }

    private ProductData GetRandomProduct(List<ProductData> pool)
    {
        int index = Random.Range(0, pool.Count);
        ProductData product = pool[index];
        pool.RemoveAt(index);
        return product;
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
        ProductData held = handSystem.FirstOrNull;

        if (held == null)
        {
            Debug.Log("Elinde ürün yok");
            return;
        }

        bool served = customer.TryServe(held);

        if (served)
        {
            handSystem.Clear();

            if (AllServed())
                SpawnCustomersAndOrders();
        }
        else
        {
            Debug.Log("Yanlýþ ürün verdin");
        }
    }

    private bool AllServed()
    {
        bool aDone = (customerA == null || customerA.IsServed);
        bool bDone = (customerB == null || customerB.IsServed);
        return aDone && bDone;
    }
}
