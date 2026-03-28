using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    public Checkpoint[] m_checkpoints; // Array to hold references to all checkpoints in the scene

    private Checkpoint m_activeCP; // Reference to the currently active checkpoint, needed for respawning

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_checkpoints = FindObjectsByType<Checkpoint>(FindObjectsSortMode.None); // Find all Checkpoint components in the scene and store them in the array
        foreach (Checkpoint checkpoint in m_checkpoints) // Loop through each checkpoint in the array
        {
            checkpoint.m_checkpointManager = this; // Set the m_checkpointManager reference of each checkpoint to this instance of CheckpointManager
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeactivateAllCheckpoints()
    {
        foreach (Checkpoint checkpoint in m_checkpoints) // Loop through each checkpoint in the array
        {
            checkpoint.DeactivateCheckpoint(); // Call the DeactivateCheckpoint method on each checkpoint to reset them
        }
    }


    public void setActiveCheckpoint(Checkpoint newActiveCP)
    {
        DeactivateAllCheckpoints();
        m_activeCP = newActiveCP; // Set the currently active checkpoint to the new checkpoint passed as a parameter
    }
}
