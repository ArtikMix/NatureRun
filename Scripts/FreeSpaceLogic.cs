using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FreeSpaceLogic : MonoBehaviour
{
    [SerializeField] private int[] type = new int[5];
    Types types;
    [SerializeField] private GameObject floor, water, death;
    [SerializeField] private GameObject coin;
    int x = 0;
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
        for (int i = 0; i < 5; i++)
        {
            Debug.Log("for");
            switch (type[i])
            {
                case 1:
                    Debug.Log("1");
                    GameObject g1 = Instantiate(floor, transform.GetChild(i).transform.position, transform.GetChild(i).transform.rotation);
                    Destroy(transform.GetChild(i));
                    g1.transform.SetParent(gameObject.transform);
                    g1.SetActive(Convert.ToBoolean(UnityEngine.Random.Range(0, 2)));
                    if (g1.activeSelf)
                    {
                        int m = UnityEngine.Random.Range(0, 2);
                        if (m == 1)
                        {
                            Vector3 coinPos = new Vector3(g1.transform.position.x, g1.transform.position.y + 2, g1.transform.position.z);
                            Instantiate(coin, coinPos, Quaternion.identity);
                        }
                    }
                    break;
                case 2:
                    Debug.Log("2");
                    GameObject g2 = Instantiate(water, transform.GetChild(i).transform.position, transform.GetChild(i).transform.rotation);
                    Destroy(transform.GetChild(i));
                    g2.transform.SetParent(gameObject.transform);
                    g2.SetActive(Convert.ToBoolean(UnityEngine.Random.Range(0, 2)));
                    break;
                case 3:
                    Debug.Log("3");
                    GameObject g3 = Instantiate(death, transform.GetChild(i).transform.position, transform.GetChild(i).transform.rotation);
                    Destroy(transform.GetChild(i));
                    g3.transform.SetParent(gameObject.transform);
                    g3.SetActive(Convert.ToBoolean(UnityEngine.Random.Range(0, 2)));
                    break;
            }
        }
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player").transform.position.x + 25f < transform.position.x)
            Destroy(gameObject);
    }
}
