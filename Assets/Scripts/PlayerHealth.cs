using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHearts = 3;                  // cantidad máxima de corazones
    public int currentHealth;
    public Image[] hearts;                     // imágenes en la UI
    public Sprite fullHeart;                   // sprite de corazón lleno
    public Sprite emptyHeart;                  // sprite de corazón vacío

    private void Start()
    {
        currentHealth = maxHearts;

        UpdateHearts();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
            currentHealth = 0;

        UpdateHearts();
    }


    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }
}


