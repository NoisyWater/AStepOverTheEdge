using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	public float health = 100;
    public RandomSoundPlayer soundPlayer;

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
		PlayerLook.Instance.AddShake(0.1f, 0.1f);
		UIManager.Instance.InstantiateHitUI();

		if(health <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		Time.timeScale = 0f;
		UIManager.Instance.EnableDeathUI();
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
}
