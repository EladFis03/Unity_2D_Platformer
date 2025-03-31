using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance; // Singleton instance of UIManager
    public Image[] heartIcons; // Array of heart icons to represent player health
    public Sprite fullHeart; // Sprite for full heart icon
    public Sprite emptyHeart; // Sprite for empty heart icon

    private void Awake()
    {
        instance = this; // Assign the singleton instance to this object
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMaxHealth(int maxHealth)
    {
        // Set the number of heart icons based on the maximum health
        for (int i = 0; i < heartIcons.Length; i++)
        {
            heartIcons[i].enabled = true;

            if (i >= maxHealth)
            {
                heartIcons[i].enabled = false; // Disable heart icons that are not needed
            }
        }
    }

    // This method is called from PlayerHealthManager to update the UI
    // It takes the current player health as a parameter and updates the heart icons accordingly
    public void UpdateHealthDisplay(int currentPlayerHealth, int maxHealth)
    {
        for (int i = 0; i < heartIcons.Length; i++)
        {
            heartIcons[i].enabled = true; // Enable heart icons that are needed

            if (i < currentPlayerHealth)
            {
                heartIcons[i].sprite = fullHeart; // Set the heart icon to full heart
            }
            else
            {
                heartIcons[i].sprite = emptyHeart; // Set the heart icon to empty heart

                if (maxHealth <= i)
                {
                    heartIcons[i].enabled = false; // Disable heart icons that are not needed
                }
            }
        }
    }
}
