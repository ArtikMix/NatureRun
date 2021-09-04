using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CubeTypeConverter : MonoBehaviour, IPointerClickHandler
{
    WaterLogic water;
    [SerializeField] private GameObject guide;
    bool once = false;

    void Start()
    {
        water = FindObjectOfType<WaterLogic>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (water.water >= 1)
        {
            if (once == false)
            {
                guide.SetActive(false);
                once = true;
            }
            transform.gameObject.tag = "floor";
            water.UpdateBucket_();
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
