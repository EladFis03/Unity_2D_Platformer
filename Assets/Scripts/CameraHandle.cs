using UnityEngine;

public class CameraHandle : MonoBehaviour
{

    public Transform target;

    public bool freezeVertical, freezeHorizontal;

    public bool doClampPosition;
    public Transform clampMinPos, clampMaxPos;
    private Vector3 positionStore;
    private float halfCamWidth, halfCamHeight;

    public Camera m_cam;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        positionStore = transform.position;

        // Setting it so the clamp positions are disconnected from main camera at the start
        clampMinPos.SetParent(null);
        clampMaxPos.SetParent(null);

        halfCamHeight = m_cam.orthographicSize;
        halfCamWidth = m_cam.orthographicSize * m_cam.aspect;
    }

    // LateUpdate is called once per frame last from all stuff
    void LateUpdate()
    {
        setCameraPosition(target.position.x, target.position.y, -10f);

        handleFreezeCamera();

        stableCameraPosition();

        if(ParallaxBG.instance != null) // Checking if there is a ParallaxBG instance in the scene
            // in levels of a single area (like a special level without parallax background) there might not be one 
            ParallaxBG.instance.MoveBackGround(); // Moving the background according to the camera position
    }

    void setCameraPosition(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /* handleFreezeCamera - freezing the camera horizontaly and/or verticaly */
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void handleFreezeCamera()
    {
        if (freezeVertical)
        {
            setCameraPosition(transform.position.x, positionStore.y, transform.position.z);
        }

        if (freezeHorizontal)
        {
            setCameraPosition(positionStore.x, transform.position.y, transform.position.z);
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /* stableCameraPosition - if the camera reached the lowest/highst points in the game */
    /* the camera will stay there until it is in a higher/lower point */
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void stableCameraPosition()
    {
        if (doClampPosition)
        {
            setCameraPosition(
                Mathf.Clamp(transform.position.x, clampMinPos.position.x + halfCamWidth, clampMaxPos.position.x - halfCamWidth),
                Mathf.Clamp(transform.position.y, clampMinPos.position.y + halfCamHeight, clampMaxPos.position.y - halfCamHeight),
                transform.position.z);
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /* OnDrawGizmos - drawing a box (in debug only) to maintain the clamping better */
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void OnDrawGizmos()
    {
        if (doClampPosition)
        {

            Gizmos.color = Color.cyan;

            // line on left side
            Gizmos.DrawLine(clampMinPos.position, new Vector3(clampMinPos.position.x, clampMaxPos.position.y, 0f));

            // line on right side
            Gizmos.DrawLine(new Vector3(clampMaxPos.position.x, clampMinPos.position.y, 0f), clampMaxPos.position);

            // line on lower side
            Gizmos.DrawLine(clampMinPos.position, new Vector3(clampMaxPos.position.x, clampMinPos.position.y, 0f));

            // line on upper side
            Gizmos.DrawLine(clampMaxPos.position, new Vector3(clampMinPos.position.x, clampMaxPos.position.y, 0f));
        }
    }
}
