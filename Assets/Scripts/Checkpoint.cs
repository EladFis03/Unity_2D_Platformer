using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private bool m_isActivated; // Flag to track if the checkpoint has been activated
    public Animator m_checkpointAnimator; // Animator component for the checkpoint

    [HideInInspector] // Hide the CheckpointManager reference in the Inspector since it will be set programmatically
    public CheckpointManager m_checkpointManager; // Reference to the CheckpointManager to manage checkpoint states

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !m_isActivated) // Check if the colliding object has the "Player" tag
                                                          // and if the checkpoint is not already activated
        {
            m_checkpointManager.setActiveCheckpoint(this); // Set this checkpoint as the active checkpoint in the CheckpointManager
            m_checkpointAnimator.SetBool("flagActive", true); // Trigger the "Activate" animation
            m_isActivated = true; // Set the checkpoint as activated          
        }
    }


    public void DeactivateCheckpoint()
    {
        m_checkpointAnimator.SetBool("flagActive", false); // Trigger the "Deactivate" animation
        m_isActivated = false; // Set the checkpoint as deactivated
    }
}
