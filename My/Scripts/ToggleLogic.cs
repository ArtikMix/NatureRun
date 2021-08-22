using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleLogic : MonoBehaviour
{
    [SerializeField] Toggle d, r, f;
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
    }
}
