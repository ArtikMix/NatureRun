using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Types : MonoBehaviour
{
    [SerializeField] private GameObject free;
    [SerializeField] private Vector3 pos;
    [SerializeField] private float first;

    private void Start()
    {
        StartCoroutine(SpawnBigFreePrefab());
    }

    IEnumerator SpawnBigFreePrefab()
    {
        first = first - 18f;
        pos = new Vector3(first, 0f, 9.9f);
        Instantiate(free, pos, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        StartCoroutine(SpawnBigFreePrefab());
    }
}
