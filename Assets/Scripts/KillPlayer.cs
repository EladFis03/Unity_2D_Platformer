using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LifeController.instance.Respawn(); // Call the Respawn method on the LifeController singleton instance to respawn the player
        }
    }
}
