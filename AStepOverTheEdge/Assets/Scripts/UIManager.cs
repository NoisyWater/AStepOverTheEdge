using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject hitUI;

    public GameObject DeathUI;

    public TextMeshProUGUI ammoText;

    private void Awake()
    {
        Time.timeScale = 1.0f;
        Instance = this;
    }

    public void InstantiateHitUI()
    {
        Instantiate(hitUI, transform);
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EnableDeathUI()
    {
        DeathUI.SetActive(true);
    }
}
