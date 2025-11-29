using System;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{

    public static PlayerHealthManager Instnace; // Singleton Instnace of PlayerHealthManager
    private PlayerManager m_PlayerManager; // Reference to the PlayerManager script


    // Health variables
    [SerializeField] int currentHealth;
    [Range(1, 10)]
    [SerializeField] int maxHealth; // Current and maximum health of the player
    // Invincibility variables
    public float invincibilityDuration = 1.5f; // Duration of invincibility after taking damage
    private float invincibilityCounter; // Timer to track invincibility duration

    // Visual feedback variables
    public SpriteRenderer playerSprite; // Reference to the player's sprite renderer for visual feedback
    public Color normalColor, fadeColor; // Colors for normal and faded states

    private void Awake()
    {
        Instnace = this; // Assign the singleton Instnace to this object
        // Ensure that this object is not destroyed when loading a new scene
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth; // Initialize current health to maximum health

        UIManager.instance.SetMaxHealth(maxHealth); // Set the maximum health in the UI

        m_PlayerManager = GetComponent<PlayerManager>(); // Get the PlayerManager component attached to the same GameObject
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibilityCounter > 0) // Check if the player is currently invincible
        {
            invincibilityCounter -= Time.deltaTime; // Decrease the invincibility timer

            if(invincibilityCounter <= 0) // Check if invincibility has ended
            {
                playerSprite.color = normalColor; // Reset the player's color to normal
            }
        }

        // Debugging input to test health increase
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.H)) // Debug key to decrease health
        {
            IncreaseHealth(2); // Decrease health when 'H' key is pressed
        }
#endif
    }
    // GetCurrentHealthValue - returns the current health of the player
    public int GetCurrentHealthValue()
    {
        return currentHealth;
    }

    // GetMaxHealth - returns the maximum health of the player
    public int GetMaxHealth()
    {
        return maxHealth; // Return the maximum health of the player
    }
    // IncreaseHealth - increases the player's health by a specified amount
    public void IncreaseHealth(int amountToAdd)
    {
        currentHealth += amountToAdd; // Increase current health by the specified amount
        if (currentHealth > maxHealth) // Ensure current health does not exceed maximum health
        {
            currentHealth = maxHealth;
        }

        UIManager.instance.UpdateHealthDisplay(currentHealth, maxHealth); // Update the health display in the UI
    }

    // DecreaseHealth - decreases the player's health by a specified amount
    public void DecreaseHealth()
    {
        if (invincibilityCounter <= 0) // Check if the player is not currently invincible
        {
            currentHealth--; // Decrease current health by the specified amount

            if (currentHealth <= 0) // Ensure current health does not go below zero
            {
                currentHealth = 0;
                // Handle player death here (e.g., trigger game over, respawn, etc.)
                Debug.Log("Player is dead!");
                gameObject.SetActive(false); // Deactivate the player object
                                             // This is for now, until we add checkpoints and respawn logic
                                             //TODO: Add player death handling logic here
            }
            else
            {
                invincibilityCounter = invincibilityDuration; // Reset the invincibility timer
                playerSprite.color = fadeColor; // Change the player's color to indicate damage taken
                m_PlayerManager.KnockBack(); // Apply knockback effect to the player
            }

            UIManager.instance.UpdateHealthDisplay(currentHealth, maxHealth); // Update the health display in the UI
        }
    }
}
