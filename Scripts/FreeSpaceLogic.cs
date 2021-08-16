using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FreeSpaceLogic : MonoBehaviour
{
    [SerializeField] private int[] type = new int[5];
    Types types;
    void Start()
    {
        types = GetComponent<Types>();
        for(int i = 0; i < 5; i++)
        {
            type[i] = UnityEngine.Random.Range(1, 4);
        }
        TypeSpawner();
    }

    private void TypeSpawner()
    {
        for(int i = 0; i < 5; i++)
        {
            Debug.Log("for");
            switch (type[i])
            {
                case 1:
                    Debug.Log("1");
                    GameObject g1 = Instantiate(types.floor, transform.GetChild(i).transform.position, transform.GetChild(i).transform.rotation);
                    Destroy(transform.GetChild(i).gameObject);
                    g1.SetActive(Convert.ToBoolean(UnityEngine.Random.Range(0, 1)));
                    break;
                case 2:
                    Debug.Log("2");
                    GameObject g2 = Instantiate(types.water, transform.GetChild(i).transform.position, transform.GetChild(i).transform.rotation);
                    Destroy(transform.GetChild(i).gameObject);
                    g2.SetActive(Convert.ToBoolean(UnityEngine.Random.Range(0, 1)));
                    break;
                case 3:
                    Debug.Log("3");
                    GameObject g3 = Instantiate(types.death, transform.GetChild(i).transform.position, transform.GetChild(i).transform.rotation);
                    Destroy(transform.GetChild(i).gameObject);
                    g3.SetActive(Convert.ToBoolean(UnityEngine.Random.Range(0, 1)));
                    break;
            }
        }
    }
}
