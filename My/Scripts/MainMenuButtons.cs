using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject shop, settings, main, day, rain, fog, pause, start, pause_button, guide;
    public bool starting = false;

    private void Start()
    {
        start.SetActive(true);
        pause_button.SetActive(false);
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
        start.SetActive(false);
        starting = true;
        main.SetActive(false);
        pause_button.SetActive(true);
        guide.SetActive(true);
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
            PlayerPrefs.Save();
        }
    }

    public void BuyFog()
    {
        if (PlayerPrefs.HasKey("coins") && PlayerPrefs.GetInt("coins") >= 3000)
        {
            PlayerPrefs.SetInt("fog_buy", 1);
            rain.SetActive(false);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 3000);
            PlayerPrefs.Save();
        }
    }

    public void BuyRain()
    {
        if (PlayerPrefs.HasKey("coins") && PlayerPrefs.GetInt("coins") >= 1000)
        {
            PlayerPrefs.SetInt("rain_buy", 1);
            rain.SetActive(false);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 1000);
            PlayerPrefs.Save();
        }
    }
    #endregion

    #region Settings
    //
    #endregion

    public void Again()
    {
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pause.SetActive(true);
        main.SetActive(true);
    }

    public void Continue()
    {
        pause.SetActive(false);
        Time.timeScale = 1f;
        main.SetActive(false);
    }
}
