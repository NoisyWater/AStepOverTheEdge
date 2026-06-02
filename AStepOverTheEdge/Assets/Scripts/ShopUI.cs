using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public int healthPotionPrice = 50;
    public int dashPrice = 100;

    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;

    public void BuyHealthPotion()
    {
        if (CurrencyManager.Instance.SpendCoins(healthPotionPrice))
        {
            playerHealth.Heal(50);
            Debug.Log("Bought health potion!");
        }
        else
        {
            Debug.Log("Not enough coins");
        }
    }

    public void BuyDash()
    {
        if (CurrencyManager.Instance.SpendCoins(dashPrice))
        {
            playerMovement.gameObject.AddComponent<PlayerDash>();
            Debug.Log("Dash unlocked!");
        }
        else
        {
            Debug.Log("Not enough coins");
        }
    }
}