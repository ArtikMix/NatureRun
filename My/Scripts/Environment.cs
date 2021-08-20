using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.x >= GameObject.FindGameObjectWithTag("Player").transform.position.x + 75f)
            Destroy(gameObject);
    }
}
