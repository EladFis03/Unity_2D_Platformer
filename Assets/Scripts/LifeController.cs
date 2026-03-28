using System.Collections;
using UnityEngine;

public class LifeController : MonoBehaviour
{

    public static LifeController instance; // Singleton instance of the LifeController to ensure only one instance exists

    private PlayerManager m_thePlayer; // Reference to the PlayerManager to manage player lives

    public float m_respawnDelay = 2f;

    public int m_lives = 3; // Variable to track the number of lives the player has, initialized to 3

    private void Awake()
    {
        // Implementing the Singleton pattern to ensure only one instance of LifeController exists
        if (instance == null)
        {
            instance = this; // Set the instance to this object if it is the first one
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_thePlayer = FindFirstObjectByType<PlayerManager>(); // Find the PlayerManager in the scene to manage player lives
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        //m_thePlayer.transform.position = FindFirstObjectByType<CheckpointManager>().m_RespawnPos; // Move the player to the respawn position defined in the CheckpointManager

        //PlayerHealthManager.Instnace.IncreaseHealth(PlayerHealthManager.Instnace.GetMaxHealth());

        m_thePlayer.gameObject.SetActive(false);

        m_thePlayer.m_RigidBody.linearVelocity = Vector2.zero; // Reset the player's velocity to zero to prevent any unintended movement after respawning

        m_lives--; // Decrease the player's lives by 1 when they respawn

        if (m_lives > 0) // only if the player still has lives remaining
        {
            StartCoroutine(RespawnCoroutine()); // Start the RespawnCoroutine to handle the respawn process after a delay
        }
        else
        {
            m_lives = 0; // Ensure that lives do not go below 0
        }

        UIManager.instance.UpdateLivesDisplay(m_lives); // Update the UI to reflect the current number of lives after respawning
    }

    public IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(m_respawnDelay);

        m_thePlayer.transform.position = FindFirstObjectByType<CheckpointManager>().m_RespawnPos; // Move the player to the respawn position defined in the CheckpointManager

        PlayerHealthManager.Instnace.IncreaseHealth(PlayerHealthManager.Instnace.GetMaxHealth());

        m_thePlayer.gameObject.SetActive(true);
    }
}
