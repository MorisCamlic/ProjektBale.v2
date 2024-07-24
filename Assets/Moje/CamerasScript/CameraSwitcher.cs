using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera mainCamera;
    public Camera camera2;
    public Camera camera3;
    public Camera camera4;
    public Camera camera5;
    public Camera camera6;
    public Camera camera6b;
    public Camera camera7;
    public Camera camera8a;
    public Camera camera8b;
    public Camera camera9;
    public Camera camera10;
    public Camera camera11;
    public Camera camera12;
    public Camera camera13;
    public Camera camera14;
    public Camera camera15;
    public Camera camera16;
    public Camera camera17;
    public Camera camera18;
    public Camera camera19;

    private Camera currentCamera;
    private CameraTrigger activeTrigger;

    void Start()
    {
        // Disable all cameras initially
        mainCamera.enabled = false;
        camera2.enabled = false;
        camera3.enabled = false;
        camera4.enabled = false;
        camera5.enabled = false;
        camera6.enabled = false;
        camera6b.enabled = false;
        camera7.enabled = false;
        camera8a.enabled = false;
        camera8b.enabled = false;
        camera9.enabled = false;
        camera10.enabled = false;
        camera11.enabled = false;
        camera12.enabled = false;
        camera13.enabled = false;
        camera14.enabled = false;
        camera15.enabled = false;
        camera16.enabled = false;
        camera17.enabled = false;
        camera18.enabled = false;
        camera19.enabled = false;

        // Enable the main camera
        SwitchToCamera(mainCamera);
    }

    public void SwitchToCamera(Camera camera)
    {
        // Disable the current camera
        if (currentCamera != null)
        {
            currentCamera.enabled = false;
        }

        // Enable the new camera
        currentCamera = camera;
        currentCamera.enabled = true;
    }

    public void SetActiveTrigger(CameraTrigger trigger)
    {
        activeTrigger = trigger;
    }

    public void ClearActiveTrigger(CameraTrigger trigger)
    {
        if (activeTrigger == trigger)
        {
            activeTrigger = null;
            SwitchToCamera(mainCamera);
        }
    }

    public bool IsInActiveTrigger(CameraTrigger trigger)
    {
        return activeTrigger == trigger;
    }
}
