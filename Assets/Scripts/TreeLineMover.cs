using UnityEngine;

public class TreeLineMover : MonoBehaviour
{

    public float maxDistance = 22f; // How far a object can be from the camera before we move it

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = transform.position.x - Camera.main.transform.position.x;
        // If the object is too far from the camera, move it back to the camera's position
        // This is a simple way to create a looping background effect

        // are we over to the right of the camera?
        if (distance > maxDistance)
        {
            // move the object to the left of the camera
            transform.position -= new Vector3(maxDistance * 2f, 0f, 0f);
        }

        // are we over to the left of the camera?
        if (distance < -maxDistance)
        {
            // move the object to the right of the camera
            transform.position += new Vector3(maxDistance * 2f, 0f, 0f);
        }
    }
}
