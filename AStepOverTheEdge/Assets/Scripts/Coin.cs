using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CurrencyManager.Instance.AddCoins(value);
            Destroy(gameObject);
        }
    }

    public void BuyItem()
    {
        if (CurrencyManager.Instance.SpendCoins(50))
        {
            Debug.Log("Item purchased!");
        }
        else
        {
            Debug.Log("Not enough coins.");
        }
    }


}