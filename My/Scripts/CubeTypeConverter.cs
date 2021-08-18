using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CubeTypeConverter : MonoBehaviour, IPointerClickHandler
{
    WaterLogic water;

    void Start()
    {
        water = FindObjectOfType<WaterLogic>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (water.water >= 1)
        {
            transform.gameObject.tag = "floor";
            water.water--;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
