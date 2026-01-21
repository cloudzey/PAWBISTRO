
using UnityEngine;

public class ServeButton : MonoBehaviour
{
    public LevelManager levelManager;
    public ProductData product;

    public void Serve()
    {
        if (levelManager == null || product == null)
        {
            Debug.LogWarning("ServeButton: levelManager/product boş!");
            return;
        }

        if (levelManager.CurrentOrder == product)
        {
            Debug.Log("Correct product served!");
            levelManager.AddCoin(product.price > 0 ? product.price : 10);
            levelManager.CreateNewOrder(); // sende fonksiyon adı farklıysa söyle, düzeltelim
        }
        else
        {
            Debug.Log("Wrong product served!");
        }
    }
}