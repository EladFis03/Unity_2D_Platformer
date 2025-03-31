using System;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{

    public static PlayerHealthManager instance; // Singleton instance of PlayerHealthManager

    
    public int currentHealth;
    [Range(1, 10)]
    public int maxHealth; // Current and maximum health of the player
    public float invincibilityDuration = 2f; // Duration of invincibility after taking damage


    private void Awake()
    {
        instance = this; // Assign the singleton instance to this object
        // Ensure that this object is not destroyed when loading a new scene
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth; // Initialize current health to maximum health

        UIManager.instance.SetMaxHealth(maxHealth); // Set the maximum health in the UI
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetMaxHealth()
    {
        return maxHealth; // Return the maximum health of the player
    }

    public void IncreaseHealth()
    {
        currentHealth++; // Increase current health by the specified amount
        if (currentHealth > maxHealth) // Ensure current health does not exceed maximum health
        {
            currentHealth = maxHealth;
        }

        UIManager.instance.UpdateHealthDisplay(currentHealth, maxHealth); // Update the health display in the UI
    }


    public void DecreaseHealth()
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

        UIManager.instance.UpdateHealthDisplay(currentHealth, maxHealth); // Update the health display in the UI
    }
}
