using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToggleLogic : MonoBehaviour
{
    [SerializeField] private GameObject day, rain, fog;
    private bool d, r, f;
    GraphicMods gx;

    private void Start()
    {
        gx = FindObjectOfType<GraphicMods>();
        if (PlayerPrefs.HasKey("day") && PlayerPrefs.GetInt("day") == 1)
        {
            day.transform.GetChild(0).gameObject.SetActive(true);
            d = true;
        }
        else
        {
            day.transform.GetChild(0).gameObject.SetActive(false);
            d = false;
        }
        if (PlayerPrefs.HasKey("fog") && PlayerPrefs.GetInt("fog") == 1)
        {
            fog.transform.GetChild(0).gameObject.SetActive(true);
            f = true;
        }
        else
        {
            fog.transform.GetChild(0).gameObject.SetActive(false);
            f = false;
        }
        if (PlayerPrefs.HasKey("rain") && PlayerPrefs.GetInt("rain") == 1)
        {
            rain.transform.GetChild(0).gameObject.SetActive(true);
            r = true;
        }
        else
        {
            rain.transform.GetChild(0).gameObject.SetActive(true);
            r = false;
        }
        gx.UpdateStates();
    }
    public void DayToggle()
    {
        Debug.Log(PlayerPrefs.GetInt("day_buy"));
        if (PlayerPrefs.HasKey("day_buy") == true && PlayerPrefs.GetInt("day_buy") == 1)
        {
            Debug.Log("haskeyday");
            if (d == false)
            {
                Debug.Log("d false");
                day.transform.GetChild(0).gameObject.SetActive(true);
                PlayerPrefs.SetInt("day", 1);
                PlayerPrefs.Save();
                d = true;
            }
            if (d == true)
            {
                Debug.Log("d true");
                day.transform.GetChild(0).gameObject.SetActive(false);
                PlayerPrefs.SetInt("day", 0);
                PlayerPrefs.Save();
                d = false;
            }
        }
        else
        {
            Debug.Log("hasn'tkeyday");
            day.transform.GetChild(0).gameObject.SetActive(false);
            d = false;
        }
        Debug.Log(PlayerPrefs.GetInt("day"));
        gx.UpdateStates();
    }

    public void FogToggle()
    {
        if (PlayerPrefs.HasKey("fog_buy") && PlayerPrefs.GetInt("fog_buy") == 1)
        {
            if (f == false)
            {
                fog.transform.GetChild(0).gameObject.SetActive(true);
                PlayerPrefs.SetInt("fog", 1);
                PlayerPrefs.Save();
                f = true;
            }
            if (f == true)
            {
                fog.transform.GetChild(0).gameObject.SetActive(false);
                PlayerPrefs.SetInt("fog", 0);
                PlayerPrefs.Save();
                f = false;
            }
        }
        else
        {
            fog.transform.GetChild(0).gameObject.SetActive(false);
            f = false;
        }
        gx.UpdateStates();
    }

    public void RainToggle()
    {
        if (PlayerPrefs.HasKey("rain_buy") && PlayerPrefs.GetInt("rain_buy") == 1)
        {
            if (r == false)
            {
                rain.transform.GetChild(0).gameObject.SetActive(true);
                PlayerPrefs.SetInt("rain", 1);
                PlayerPrefs.Save();
                r = true;
            }
            if (r == true)
            {
                rain.transform.GetChild(0).gameObject.SetActive(false);
                PlayerPrefs.SetInt("rain", 0);
                PlayerPrefs.Save();
                r = false;
            }
        }
        else
        {
            rain.transform.GetChild(0).gameObject.SetActive(false);
            r = false;
        }
        gx.UpdateStates();
    }
}
