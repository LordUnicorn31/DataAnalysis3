using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AnalyticsSceneLogic : MonoBehaviour
{
    private bool zoom = false;
    [SerializeField] Camera camera;
    [SerializeField] float zoomIn = 26;
    [SerializeField] float zoomOut = 91;
    [SerializeField] float camSpeed = 5.0f;
    [SerializeField] Quaternion zoomOutQuat;
    [SerializeField] Quaternion zoomInQuat;
    [SerializeField] float zOffset;
    private const float initialPosX = 22.2f;
    private const float initialPosZ = 5.9f;
    [SerializeField] DataRetriever dataR;

    private void Awake()
    {
        // Camera stuff
        camera.transform.position = new Vector3(initialPosX, zoomOut, initialPosZ);
        camera.transform.rotation = zoomOutQuat;
        zoom = false;
    }
    public void ChangeSceneGameplay()
    {
        SceneManager.LoadScene(0);
    }

    private void FixedUpdate()
    {
        CameraMovement();
    }

    public void Zoom()
    {
        zoom = !zoom;
        if (zoom)
        {
            camera.transform.position = new Vector3(camera.transform.position.x, zoomIn, camera.transform.position.z + zOffset);
            camera.transform.rotation = zoomInQuat;
            dataR.ShowDeaths(true);
        }

        else
        {
            camera.transform.position = new Vector3(initialPosX, zoomOut, initialPosZ);
            camera.transform.rotation = zoomOutQuat;
            dataR.ShowDeaths(false);
        }
    }

    private void CameraMovement()
    {
        if (!zoom) return;
        Vector3 movement = new Vector3();

        if (Input.GetKey(KeyCode.A)) movement.x -= camSpeed;
        if (Input.GetKey(KeyCode.W)) movement.z += camSpeed;
        if (Input.GetKey(KeyCode.D)) movement.x += camSpeed;
        if (Input.GetKey(KeyCode.S)) movement.z -= camSpeed;

        camera.transform.position += movement;
    }
}
