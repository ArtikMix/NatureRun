using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToggleLogic : MonoBehaviour
{
    [SerializeField] Toggle d, r, f;

    private void Start()
    {
        if (PlayerPrefs.HasKey("day") && PlayerPrefs.GetInt("day") == 1)
        {
            d.isOn = true;
        }
        else
        {
            d.isOn = false;
        }
        if (PlayerPrefs.HasKey("fog") && PlayerPrefs.GetInt("fog") == 1)
        {
            f.isOn = true;
        }
        else
        {
            f.isOn = false;
        }
        if (PlayerPrefs.HasKey("rain") && PlayerPrefs.GetInt("rain") == 1)
        {
            r.isOn = true;
        }
        else
        {
            r.isOn = false;
        }
    }
    public void DayToggle()
    {
        if (PlayerPrefs.HasKey("day_buy") && PlayerPrefs.GetInt("day_buy") == 1)
        {
            if (d.isOn == false)
            {
                d.isOn = true;
                PlayerPrefs.SetInt("day", 1);
            }
            if (d.isOn == true)
            {
                d.isOn = false;
                PlayerPrefs.SetInt("day", 0);
            }
        }
        else
        {
            d.isOn = false;
        }
    }

    public void FogToggle()
    {
        if (PlayerPrefs.HasKey("fog_buy") && PlayerPrefs.GetInt("fog_buy") == 1)
        {
            if (f.isOn == false)
            {
                f.isOn = true;
                PlayerPrefs.SetInt("fog", 1);
            }
            if (f.isOn == true)
            {
                f.isOn = false;
                PlayerPrefs.SetInt("fog", 0);
            }
        }
        else
        {
            f.isOn = false;
        }
    }

    public void RainToggle()
    {
        if (PlayerPrefs.HasKey("rain_buy") && PlayerPrefs.GetInt("rain_buy") == 1)
        {
            if (r.isOn == false)
            {
                r.isOn = true;
                PlayerPrefs.SetInt("rain", 1);
            }
            if (r.isOn == true)
            {
                r.isOn = false;
                PlayerPrefs.SetInt("rain", 0);
            }
        }
        else
        {
            r.isOn = false;
        }
    }
}
