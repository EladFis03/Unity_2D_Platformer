using UnityEngine;

public class DamagePlayerHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            // turn off the object that entered the trigger
            // other.gameObject.SetActive(false);

            // Call the DecreaseHealth method on the player object
            PlayerHealthManager.Instnace.DecreaseHealth();
        }
    }
}
