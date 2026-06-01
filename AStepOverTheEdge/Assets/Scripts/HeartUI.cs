using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    public Image[] hearts;

    public Sprite fullHeart;
    public Sprite emptyHeart;

    public void UpdateHearts(float currentHealth, float maxHealth)
    {
        int heartCount = hearts.Length;

        float healthPerHeart = maxHealth / heartCount;

        for (int i = 0; i < heartCount; i++)
        {
            if (currentHealth > i * healthPerHeart)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}