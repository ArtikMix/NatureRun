using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject shop, settings, main, day, rain, fog;

    private void Start()
    {
        if (PlayerPrefs.GetInt("day_buy") == 1)
        {
            day.SetActive(false);
        }
        if (PlayerPrefs.GetInt("fog_buy") == 1)
        {
            fog.SetActive(false);
        }
        if (PlayerPrefs.GetInt("rain_buy") == 1)
        {
            rain.SetActive(false);
        }
    }

    #region Main
    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ShopButton()
    {
        main.SetActive(false);
        shop.SetActive(true);
    }

    public void SettingsButton()
    {
        main.SetActive(false);
        settings.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
    #endregion

    public void BackButton()
    {
        if (shop.activeSelf)
        {
            shop.SetActive(false);
            main.SetActive(true);
        }
        if (settings.activeSelf)
        {
            settings.SetActive(false);
            main.SetActive(true);
        }
    }

    #region Shop
    public void BuyDay()
    {
        if (PlayerPrefs.HasKey("coins") && PlayerPrefs.GetInt("coins") >= 150)
        {
            PlayerPrefs.SetInt("day_buy", 1);
            day.SetActive(false);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 150);
        }
    }

    public void BuyFog()
    {
        if (PlayerPrefs.HasKey("coins") && PlayerPrefs.GetInt("coins") >= 3000)
        {
            PlayerPrefs.SetInt("fog_buy", 1);
            rain.SetActive(false);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 3000);
        }
    }

    public void BuyRain()
    {
        if (PlayerPrefs.HasKey("coins") && PlayerPrefs.GetInt("coins") >= 1000)
        {
            PlayerPrefs.SetInt("rain_buy", 1);
            rain.SetActive(false);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 1000);
        }
    }
    #endregion

    #region Settings
    //
    #endregion
}
