using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterLogic : MonoBehaviour
{
    public int water = 1;
    [SerializeField] private Text water_text;

    private void Start()
    {
        water_text.text = "x " + water.ToString();
    }

    public void UpdateBucket()
    {
        water++;
        water_text.text = "x " + water.ToString();
    }

    public void UpdateBucket_()
    {
        water--;
        water_text.text = "x " + water.ToString();
    }
}
