using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinLogic : MonoBehaviour
{
    [SerializeField] private Text c_t;
    public int coins;
    private void Start()
    {
        if (PlayerPrefs.HasKey("coins"))
            coins = PlayerPrefs.GetInt("coins");
        else
            coins = 0;
    }
    private void Update()
    {
        c_t.text = "x " + coins.ToString();
    }
}
