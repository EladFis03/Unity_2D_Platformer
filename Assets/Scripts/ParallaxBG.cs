using UnityEngine;

public class ParallaxBG : MonoBehaviour
{

    private Transform theCam;
    public Transform sky, treeLine;

    [Range(0f,1f)]
    public float parallaxSpeed = 0.5f; // Speed of the parallax effect
    // the lower the value, the farther the background will be from the camera
    // the higher the value, the closer the background will be to the camera

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find the camera in the scene and assign it to theCam variable
        // This is an alternative way to find the camera if you have multiple cameras in the scene 
        // and you want to ensure you're using the main camera.

        // Uncomment the line below if you want to find the camera by type instead of using Camera.main
        // theCam = FindFirstObjectByType<Camera>().transform;
        theCam = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Update the position of the sky based on the camera's position
        sky.position = new Vector3(theCam.position.x , theCam.position.y , sky.position.z);
        // Update the position of the treeLine based on the camera's position and the parallax speed
        treeLine.position = new Vector3(
            theCam.position.x * parallaxSpeed,
            theCam.position.y,
            treeLine.position.z);
    }
}
