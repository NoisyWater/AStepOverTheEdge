using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    public int coins = 0;
    public TextMeshProUGUI coinText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Re-find UI after scene loads
        GameObject uiObj = GameObject.Find("CoinText");

        if (uiObj != null)
        {
            coinText = uiObj.GetComponent<TextMeshProUGUI>();
        }

        UpdateUI();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateUI();
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            UpdateUI();
            return true;
        }

        return false;
    }

    void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coins;
        }
    }
}