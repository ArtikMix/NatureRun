using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentLogic : MonoBehaviour
{
    [SerializeField] private float posx;
    [SerializeField] private GameObject environment_big;
    private void Start()
    {
        StartCoroutine(EnvironmentSpawn());
    }

    IEnumerator EnvironmentSpawn()
    {
        yield return new WaitForSeconds(6f);
        posx = posx - 99.38589f;
        Vector3 position = new Vector3(posx, -29.61928f, 13.47511f);
        Instantiate(environment_big, position, Quaternion.identity);
        StartCoroutine(EnvironmentSpawn());
    }
}
