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
    private readonly float move_speed = 9f;
    private float acs_speed = 9f;
    public int coins;
    [SerializeField] private GameObject earth_touch, water_touch, fire_touch;

    private void Start()
    {
        if (PlayerPrefs.HasKey("coins"))
            coins = PlayerPrefs.GetInt("coins");
        else
            coins = 0;
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (on_floor)
        {
            on_floor = false;
            Jump();
        }
        Vector3 acs = Input.acceleration;
        acs_speed = 9f + Mathf.Abs(acs.x)/100;
        //Debug.Log(acs_speed);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "floor")
        {
            on_floor = true;
            GameObject g = Instantiate(earth_touch, transform.position, transform.rotation);
            Destroy(g, 8f);
            Debug.Log(transform.position);
        }
        if (collision.tag == "death")
        {
            Debug.Log(transform.position);
            Death();
        }
        if (collision.tag == "water")
        {
            on_floor = true;
            FindObjectOfType<WaterLogic>().water++;
            GameObject g = Instantiate(water_touch, transform.position, transform.rotation);
            Destroy(g, 8f);
            Debug.Log(transform.position);
        }
        if (collision.tag == "coin")
        {
            coins++;
            PlayerPrefs.SetInt("coins", coins);
            Destroy(collision.gameObject);
        }
    }

    public void Death()
    {
        Destroy(gameObject);
        GameObject g = Instantiate(fire_touch, transform.position, transform.rotation);
        Destroy(g, 3f);
    }

    public void FixedUpdate()
    {
        Vector3 acs = Input.acceleration;
        movingDirection = new Vector3(-1 * move_speed, 0, acs.x * acs_speed);
        transform.Translate(movingDirection * Time.deltaTime);
    }

    private void Jump()//реализация прыжка, похожего на отскакивание
    {
        rb.AddForce(jumpDirection * jumpPower, ForceMode.Impulse);
    }
}
