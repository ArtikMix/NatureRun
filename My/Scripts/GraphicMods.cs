using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicMods : MonoBehaviour
{
    [SerializeField] GameObject rain, dir_light, point_light;
    private void Start()
    {
       
    }

    public void UpdateStates()
    {
        Debug.Log("Updating GX state...");
        if (PlayerPrefs.HasKey("rain") && PlayerPrefs.GetInt("rain") == 1)
        {
            rain.SetActive(true);
        }
        else if (PlayerPrefs.HasKey("rain") && PlayerPrefs.GetInt("rain") == 0)
        {
            rain.SetActive(false);
        }
        if (PlayerPrefs.HasKey("day") && PlayerPrefs.GetInt("day") == 1)
        {
            Debug.Log("day");
            dir_light.SetActive(true);
            point_light.SetActive(false);
        }
        else if (PlayerPrefs.HasKey("day") && PlayerPrefs.GetInt("day") == 0)
        {
            dir_light.SetActive(false);
            point_light.SetActive(true);
        }
        if (PlayerPrefs.HasKey("fog") && PlayerPrefs.GetInt("fog") == 1)
        {
            RenderSettings.fog = true;
        }
        else if (PlayerPrefs.HasKey("fog") && PlayerPrefs.GetInt("fog") == 0)
        {
            RenderSettings.fog = false;
        }
    }
}
