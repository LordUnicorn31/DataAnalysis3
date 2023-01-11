using UnityEngine;

public class Zoom : MonoBehaviour
{
    public float zoomSpeed = 5.0f;
    public float minZoom = 5.0f;
    public float maxZoom = 15.0f;

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float zoom = Camera.main.fieldOfView;
        zoom -= scroll * zoomSpeed;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        Camera.main.fieldOfView = zoom;
    }
}
