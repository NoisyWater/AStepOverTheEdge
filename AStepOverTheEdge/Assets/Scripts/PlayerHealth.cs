using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float health = 100;
    public RandomSoundPlayer soundPlayer;


    private void Start()
    {
        UIManager.Instance.heartUI.UpdateHearts(health, maxHealth);
    }

    private void OnCollisionEnter(Collision collision)
	{
		bool isDamage = collision.gameObject.CompareTag("Damage");

		if (isDamage)
		{
			DecreaseHealth(10);

            if (soundPlayer != null)
            {
                soundPlayer.PlayRandomDamageSound();
            }

        }
	}

    private void DecreaseHealth(int decreaseAmount)
    {
        health -= decreaseAmount;

        UIManager.Instance.heartUI.UpdateHearts(health, maxHealth);

        PlayerLook.Instance.AddShake(0.1f, 0.1f);
        UIManager.Instance.InstantiateHitUI();

        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health += amount;

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        UIManager.Instance.heartUI.UpdateHearts(health, maxHealth);
    }

    private void Die()
	{
		Time.timeScale = 0f;
		UIManager.Instance.EnableDeathUI();
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        UIManager.Instance.heartUI.UpdateHearts(health, maxHealth);
    }
}
