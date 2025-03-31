using System;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    
    public static PlayerHealthManager instance; // Singleton instance of PlayerHealthManager


    private void Awake()
    {
        instance = this; // Assign the singleton instance to this object
        // Ensure that this object is not destroyed when loading a new scene
    }



    public int currentHealth, maxHealth; // Current and maximum health of the player
    public float invincibilityDuration = 2f; // Duration of invincibility after taking damage

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth; // Initialize current health to maximum health
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseHealth()
    {
        currentHealth++; // Increase current health by the specified amount
        if (currentHealth > maxHealth) // Ensure current health does not exceed maximum health
        {
            currentHealth = maxHealth;
        }
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
    }
}
