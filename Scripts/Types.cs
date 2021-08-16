using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Types : MonoBehaviour
{
    public GameObject floor, death, water;
    [SerializeField] private GameObject free;
    [SerializeField] private Vector3 pos;
    [SerializeField] private Transform player;

    private void Start()
    {
        StartCoroutine(SpawnBigFreePrefab());
    }

    private void Update()
    {
        pos = new Vector3(player.position.x - 21.75f, 0f, 9.91f);
    }

    IEnumerator SpawnBigFreePrefab()
    {
        Instantiate(free, pos, player.transform.rotation);
        yield return new WaitForSeconds(1f);
    }
}
