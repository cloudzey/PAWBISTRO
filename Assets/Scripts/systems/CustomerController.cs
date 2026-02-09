using UnityEngine;

public class CustomerController : MonoBehaviour
{
    public void OnClick()
    {
        orderManager.OnCustomerClicked(this);
    }

    public ProductData Requested { get; private set; }
    public bool IsServed { get; private set; }

    private OrderManager orderManager;

    public void Init(ProductData requested, OrderManager manager)
    {
        Requested = requested;
        orderManager = manager;
        IsServed = false;
    }

    private void OnMouseDown()
    {
        if (orderManager != null)
            orderManager.OnCustomerClicked(this);
    }

    public bool TryServe(ProductData heldProduct)
    {
        if (IsServed) return false;
        if (heldProduct == null || Requested == null) return false;

        if (heldProduct == Requested)
        {
            IsServed = true;

            Destroy(gameObject);

            return true;
        }

        return false;
    }
}
