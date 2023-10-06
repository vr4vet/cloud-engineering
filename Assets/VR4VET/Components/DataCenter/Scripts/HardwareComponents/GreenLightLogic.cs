using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class GreenLightLogic : MonoBehaviour
{
    public Material lightOff;
    public Material lightOn;
    public GameObject snapPoint;
    private Material emissiveMaterial;

    // Start is called before the first frame update
    void Start()
    {
        if (snapPoint.GetComponent<SnapZone>().HeldItem != null)
        {
            Debug.Log("Held item not null");
            TurnControlLightOn();
        }
    }

    public void TurnControlLightOn()
    {
        Renderer objectRenderer = GetComponent<Renderer>();
        // Turn on the emission when snapped
        objectRenderer.material = lightOn;
        Debug.Log("Changed material");
    }

    public void TurnControlLightOff()
    {
        Renderer objectRenderer = GetComponent<Renderer>();
        // Turn off the emission when not snapped
        objectRenderer.material = lightOff;
    }
}
