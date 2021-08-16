using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerLogic : MonoBehaviour
{
    private bool on_floor = false;//на платформе ли
    private Rigidbody rb;
    [SerializeField] private float jumpPower = 9f;
    private readonly Vector3 jumpDirection = Vector3.up;
    private Vector3 movingDirection;//постоянное движение вперёд и управление акселирометром
    private float move_speed = 9f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (on_floor)
        {
            on_floor = false;
            Jump();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "floor")
        {
            on_floor = true;
            Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), transform.position, Quaternion.identity);
        }
        if (collision.tag == "death")
        {
            Death();
        }
        if (collision.tag == "water")
        {
            on_floor = true;
            Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), transform.position, Quaternion.identity);
            FindObjectOfType<WaterLogic>().water++;
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void FixedUpdate()
    {
        Vector3 acs = Input.acceleration;
        movingDirection = new Vector3(-1, 0, acs.x);
        transform.Translate(movingDirection * move_speed * Time.deltaTime);
    }

    private void Jump()//реализация прыжка, похожего на отскакивание
    {
        rb.AddForce(jumpDirection * jumpPower, ForceMode.Impulse);
    }
}
