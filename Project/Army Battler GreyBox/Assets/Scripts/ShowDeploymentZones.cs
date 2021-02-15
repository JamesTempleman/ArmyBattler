using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDeploymentZones : MonoBehaviour
{
    public Toggle DeploymentToggle;
    public GameObject DeploymentZones;


    // Update is called once per frame
    void Update()
    {
        if (DeploymentToggle.isOn)
        {
            DeploymentZones.SetActive(true);
        }
        else
        {
            DeploymentZones.SetActive(false);
        }

    }
}
