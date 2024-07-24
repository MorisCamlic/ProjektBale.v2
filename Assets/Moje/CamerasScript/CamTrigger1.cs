using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraTrigger : MonoBehaviour
{
    public Camera cameraToSwitchTo;
    private CameraSwitcher cameraSwitcher;

    void Start()
    {
        cameraSwitcher = FindObjectOfType<CameraSwitcher>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraSwitcher.SwitchToCamera(cameraToSwitchTo);
            cameraSwitcher.SetActiveTrigger(this);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cameraSwitcher.ClearActiveTrigger(this);
        }
    }
}
