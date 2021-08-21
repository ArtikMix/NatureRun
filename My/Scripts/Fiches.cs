using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fiches : MonoBehaviour
{
    public void BuyDay()
    {
        if (PlayerPrefs.HasKey("coins"))
        {
            if (PlayerPrefs.GetInt("coins") >= 150)
            {
                if (PlayerPrefs.HasKey("day"))
                {
                    PlayerPrefs.SetInt("day", 1);
                }
                else
                {
                    PlayerPrefs.SetInt("day", 1);
                }
            }
            else
            {
                PlayerPrefs.SetInt("day", 0);
            }
        }
        if (PlayerPrefs.HasKey("day") && PlayerPrefs.GetInt("day") == 1)
        {
            if (PlayerPrefs.HasKey("day_active"))
            {
                PlayerPrefs.SetInt("day_active", 1);
            }
            else
            {
                PlayerPrefs.SetInt("day_active", 1);
            }
        }
    }

    public void BuyRain()
    {
    }
}
