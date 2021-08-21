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
    private const float move_speed = 8.975f;
    private float acs_speed = 8.9f;
    [SerializeField] private GameObject earth_touch, water_touch, fire_touch;
    PlayerAnim anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = FindObjectOfType<PlayerAnim>();
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
        if (transform.position.y < -2f)
        {
            Death();
        }
        if (transform.position.z<=1.82f || transform.position.z >= 19.22f)
        {
            Death();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "floor")
        {
            on_floor = true;
            GameObject g = Instantiate(earth_touch, transform.position, transform.rotation);
            Destroy(g, 4f);
            anim.StartAnim();

            //Debug.Log(transform.position);
        }
        if (collision.tag == "death")
        {
            //Debug.Log(transform.position);
            Death();
        }
        if (collision.tag == "water")
        {
            on_floor = true;
            FindObjectOfType<WaterLogic>().UpdateBucket();
            GameObject g = Instantiate(water_touch, transform.position, transform.rotation);
            Destroy(g, 4f);
            anim.StartAnim();
            //Debug.Log(transform.position);
        }
        if (collision.tag == "coin")
        {
            FindObjectOfType<CoinLogic>().UpdateCoins();
            PlayerPrefs.SetInt("coins", FindObjectOfType<CoinLogic>().coins);
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
