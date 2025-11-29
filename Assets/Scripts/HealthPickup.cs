using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int m_healthAmount = 1; // Amount of health to restore

    public GameObject PickupEffect; // Effect to play when the pickup is collected

    public bool isFullHealth = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) // Check if the colliding object has the "Player" tag
        {
            // Access the PlayerHealthManager singleton instance to increase health
            // First, check if the player's health is already at maximum
            if (PlayerHealthManager.Instnace.GetCurrentHealthValue() >= PlayerHealthManager.Instnace.GetMaxHealth())
            {
                return; // If the player's health is already at maximum, do nothing
            }
            // Increase the player's health and destroy the pickup
            if(isFullHealth)
            {
                m_healthAmount = PlayerHealthManager.Instnace.GetMaxHealth(); // Set health amount to max health if isFullHealth is true
            }
            PlayerHealthManager.Instnace.IncreaseHealth(m_healthAmount); // Increase the player's health by the specified amount
            Destroy(gameObject); // Destroy the health pickup object after it has been collected
            Instantiate(PickupEffect, transform.position, transform.rotation); // Instantiate the pickup effect at the pickup's position
        }
    }
}
